namespace DateTimePickerMaui;

public partial class PickerView : Grid
{
    static double AnimateY = 0;
    static readonly uint AnimateLength = 50;
    public static readonly BindableProperty PickerListProperty = BindableProperty.Create(
       nameof(PickerList),
       typeof(ObservableRangeCollection<string>),
       typeof(PickerView),
       default(ObservableRangeCollection<string>),
       propertyChanged: (source, oldValue, newValue) =>
       {
           IsUpdatePickerChange(source, newValue);
       });
    public ObservableRangeCollection<string> PickerList
    {
        get => (ObservableRangeCollection<string>)GetValue(PickerListProperty);
        set => SetValue(PickerListProperty, value);
    }
    public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(
        nameof(FontSize),
        typeof(double),
        typeof(PickerView),
        defaultValue: 14.0);
    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }
    public static readonly BindableProperty SelectedValueProperty = BindableProperty.Create(
       nameof(SelectedValue),
       typeof(string),
       typeof(PickerView),
       defaultValue: string.Empty);
    public string SelectedValue
    {
        get => (string)GetValue(SelectedValueProperty);
        set => SetValue(SelectedValueProperty, value);
    }
    public PickerView()
	{
		InitializeComponent();
	}
    public static void IsUpdatePickerChange(object source, object newValue)
    {
        try
        {
            if (newValue == null || source == null) return;

            var view = source as PickerView;
            if (view.PickerList == null) return;
            if (!view.PickerList.Any()) return;
            if (view.PickerList?.Count == 0) return;

            if (string.IsNullOrEmpty(view.SelectedValue)) return;

            var ItemList = view.PickerList.Where(x => x == view.SelectedValue);
            if (ItemList.Any())
            {
                view.SelectedValue = ItemList.FirstOrDefault();
            }
            else
            {
                view.SelectedValue = view.PickerList.FirstOrDefault();
            }

            var selectedIndex = view.PickerList.IndexOf(view.SelectedValue);

            if (selectedIndex - 1 < 0)
            {
                view.lbl1.Text = view.PickerList.LastOrDefault();
            }
            else
            {
                view.lbl1.Text = view.PickerList.ElementAt(selectedIndex - 1);
            }

            view.lbl2.Text = view.PickerList.ElementAt(selectedIndex);

            if (selectedIndex == view.PickerList.Count - 1)
            {
                view.lbl3.Text = view.PickerList.FirstOrDefault();
            }
            else
            {
                view.lbl3.Text = view.PickerList.ElementAt(selectedIndex + 1);
            }
        }
        catch (Exception)
        {
            //
        }
    }
    private async void SwipeUp(object sender, SwipedEventArgs e)
    {
        try
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                if (AnimateY == 0)
                {
                    AnimateY = lbl2.Height + lbl2.Padding.VerticalThickness + pickerview.RowSpacing;
                }

                await Task.WhenAll(
                    lbl1.TranslateTo(0, -AnimateY, AnimateLength, Easing.Linear),
                    lbl2.TranslateTo(0, -AnimateY, AnimateLength, Easing.Linear),
                    lbl3.TranslateTo(0, -AnimateY, AnimateLength, Easing.Linear));

                //lbl1.TranslationY = 0;
                //lbl2.TranslationY = 0;
                //lbl3.TranslationY = 0;

                await Task.WhenAll(
                    lbl1.TranslateTo(0, 0, AnimateLength, Easing.Linear),
                    lbl2.TranslateTo(0, 0, AnimateLength, Easing.Linear),
                    lbl3.TranslateTo(0, 0, AnimateLength, Easing.Linear));

                lbl1.Text = lbl2.Text;
                lbl2.Text = lbl3.Text;

                var selectedIndex = PickerList.IndexOf(PickerList.Where(x => x == lbl2.Text).FirstOrDefault());
                if (selectedIndex == PickerList.Count - 1)
                {
                    lbl3.Text = PickerList.FirstOrDefault();
                }
                else
                {
                    lbl3.Text = PickerList.ElementAt(selectedIndex + 1);
                }
            });
        }

        catch (Exception)
        {
           // Log.Error($"SwipeUp Failed : {ex}");
        }
    }

    private async void SwipeDown(object sender, SwipedEventArgs e)
    {
        try
        {
            await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                if (AnimateY == 0)
                {
                    AnimateY = lbl2.Height + lbl2.Padding.VerticalThickness + pickerview.RowSpacing;
                }

                //lbl1.TranslationY = -AnimateY;
                //lbl2.TranslationY = -AnimateY;
                //lbl3.TranslationY = -AnimateY;

                await Task.WhenAll(
                  lbl1.TranslateTo(0, AnimateY, AnimateLength, Easing.Linear),
                  lbl2.TranslateTo(0, AnimateY, AnimateLength, Easing.Linear),
                  lbl3.TranslateTo(0, AnimateY, AnimateLength, Easing.Linear));

                await Task.WhenAll(
                    lbl1.TranslateTo(0, 0, AnimateLength, Easing.Linear),
                    lbl2.TranslateTo(0, 0, AnimateLength, Easing.Linear),
                    lbl3.TranslateTo(0, 0, AnimateLength, Easing.Linear));

                lbl3.Text = lbl2.Text;
                lbl2.Text = lbl1.Text;

                var selectedIndex = PickerList.IndexOf(PickerList.Where(x => x == lbl2.Text).FirstOrDefault());
                if (selectedIndex - 1 < 0)
                {
                    lbl1.Text = PickerList.LastOrDefault();
                }
                else
                {
                    lbl1.Text = PickerList.ElementAt(selectedIndex - 1);
                }
            });
        }
        catch (Exception)
        {
            //
        }
    }
}