using System.Collections;

namespace DateTimePickerMaui
{
    public static class ObservableCollectionExtensions
    {
        /// <summary>
        /// Api will always retuns us List. so we will convert List to ObservableRangeCollection here.
        /// </summary>
        /// <typeparam name="T">pass the model</typeparam>
        /// <param name="sourceCollection">pass the new data of collection which you want to add</param>
        /// <returns>return a collection of <typeparamref name="T"/></returns>
        public static ObservableRangeCollection<T> ToCollection<T>(this IEnumerable<T> sourceCollection)
        {
            if (sourceCollection == null || !sourceCollection.Any()) return new ObservableRangeCollection<T>();
            return new ObservableRangeCollection<T>(sourceCollection);
        }
        /// <summary>
        /// ReplaceRangeCollection :This Extension will help us To replace All Old Items to New Item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="targetCollection">pass the existing collection</param>
        /// <param name="sourceCollection">pass the new data of collection which you want to Replace</param>
        public static void ReplaceRangeCollection<T>(this ObservableRangeCollection<T> targetCollection, IEnumerable<T> sourceCollection)
        {
            if (sourceCollection == null || !sourceCollection.Any()) return;
            if (targetCollection == null || !targetCollection.Any()) return;
            var sourceList = sourceCollection.ToList();
            targetCollection.ReplaceRange(sourceList);
        }
        /// <summary>
        /// ReloadCollection : It will clear previous collection and add full new list
        /// </summary>
        /// <typeparam name="T">pass the Data type</typeparam>
        /// <param name="targetCollection">pass the existing collection</param>
        /// <param name="sourceCollection">pass the new data of collection which you want to add</param>
        public static void ReloadCollection<T>(this ObservableRangeCollection<T> targetCollection, IEnumerable<T> sourceCollection)
        {
            if (sourceCollection == null || !sourceCollection.Any()) return;
            targetCollection?.Clear();
            targetCollection.AddRange(sourceCollection);
        }
        /// <summary>
        /// EzyCount : Get The count of IEnumerable
        /// </summary>
        /// <param name="source">pass the source</param>
        /// <returns>It will return number of items</returns>
        public static int EzyCount(this IEnumerable source)
        {
            if (source is ICollection col)
                return col.Count;

            int c = 0;
            var e = source.GetEnumerator();
            DynamicUsing(e, () =>
            {
                while (e.MoveNext())
                    c++;
            });

            return c;
        }
        public static void DynamicUsing(object resource, Action action)
        {
            try
            {
                action();
            }
            finally
            {
                IDisposable d = resource as IDisposable;
                d?.Dispose();
            }
        }
    }
}
