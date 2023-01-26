using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;


namespace DateTimePickerMaui
{
    public class ObservableRangeCollection<T> : ObservableCollection<T>
    {
        /// <summary>
        /// Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection(Of T) class.
        /// </summary>  
        public ObservableRangeCollection()
        {
        }
        /// <summary>
        /// Initializes a new instance of the System.Collections.ObjectModel.ObservableCollection(Of
        ///     T) class that contains elements copied from the specified collection.
        ///     Exceptions:
        ///   T:System.ArgumentNullException:
        ///    The collection parameter cannot be null.
        /// </summary>
        /// <param name="collection">The collection from which the elements are copied.</param>
        public ObservableRangeCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }
        /// <summary>
        ///  Adds the elements of the specified collection to the end of the ObservableCollection(Of T)
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="notificationMode"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddRange(IEnumerable<T> collection, NotifyCollectionChangedAction notificationMode = NotifyCollectionChangedAction.Add)
        {
            if (notificationMode != 0 && notificationMode != NotifyCollectionChangedAction.Reset)
            {
                throw new ArgumentException("Mode must be either Add or Reset for AddRange.", nameof(notificationMode));
            }

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            CheckReentrancy();
            int count = base.Count;
            if (AddArrangeCore(collection))
            {
                if (notificationMode == NotifyCollectionChangedAction.Reset)
                {
                    RaiseChangeNotificationEvents(NotifyCollectionChangedAction.Reset);
                    return;
                }

                List<T> changedItems = (collection is List<T> list) ? list : new List<T>(collection);
                RaiseChangeNotificationEvents(NotifyCollectionChangedAction.Add, changedItems, count);
            }
        }
        /// <summary>
        ///  Removes the first occurence of each item in the specified collection from ObservableCollection(Of
        ///     T). NOTE: with notificationMode = Remove, removed items starting index is not
        ///     set because items are not guaranteed to be consecutive.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="notificationMode"></param>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public void RemoveRange(IEnumerable<T> collection, NotifyCollectionChangedAction notificationMode = NotifyCollectionChangedAction.Reset)
        {
            if (notificationMode != NotifyCollectionChangedAction.Remove && notificationMode != NotifyCollectionChangedAction.Reset)
            {
                throw new ArgumentException("Mode must be either Remove or Reset for RemoveRange.", nameof(notificationMode));
            }

            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            CheckReentrancy();
            if (notificationMode == NotifyCollectionChangedAction.Reset)
            {
                bool flag = false;
                foreach (T item in collection)
                {
                    base.Items.Remove(item);
                    flag = true;
                }

                if (flag)
                {
                    RaiseChangeNotificationEvents(NotifyCollectionChangedAction.Reset);
                }

                return;
            }

            List<T> list = new(collection);
            for (int i = 0; i < list.Count; i++)
            {
                if (!base.Items.Remove(list[i]))
                {
                    list.RemoveAt(i);
                    i--;
                }
            }

            if (list.Count != 0)
            {
                RaiseChangeNotificationEvents(NotifyCollectionChangedAction.Remove, list);
            }
        }
        /// <summary>
        ///  Clears the current collection and replaces it with the specified item.
        /// </summary>
        /// <param name="item"></param> 
        public void Replace(T item)
        {
            ReplaceRange(new T[1] { item });
        }
        /// <summary>
        /// Clears the current collection and replaces it with the specified collection.
        /// </summary>
        /// <param name="collection"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public void ReplaceRange(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            CheckReentrancy();
            bool num = base.Items.Count == 0;
            base.Items.Clear();
            AddArrangeCore(collection);
            bool flag = base.Items.Count == 0;
            if (!(num && flag))
            {
                RaiseChangeNotificationEvents(NotifyCollectionChangedAction.Reset);
            }
        }
        /// <summary>
        /// To Add Arrange Core
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        private bool AddArrangeCore(IEnumerable<T> collection)
        {
            bool result = false;
            foreach (T item in collection)
            {
                base.Items.Add(item);
                result = true;
            }

            return result;
        }
        /// <summary>
        /// when Change of property it will raise NotificationEvents
        /// </summary>
        /// <param name="action"></param>
        /// <param name="changedItems"></param>
        /// <param name="startingIndex"></param>
#nullable enable
        private void RaiseChangeNotificationEvents(NotifyCollectionChangedAction action, List<T>? changedItems = null, int startingIndex = -1)
        {
            OnPropertyChanged(new PropertyChangedEventArgs("Count"));
            OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
            if (changedItems == null)
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(action));
            }
            else
            {
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, changedItems, startingIndex));
            }
        }
    }
}
