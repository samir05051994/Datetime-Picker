namespace DateTimePickerMaui;

public partial class DateTimePickerView : Grid
{
    public static readonly BindableProperty MonthPickerListProperty = BindableProperty.Create(
        nameof(MonthPickerList),
        typeof(ObservableRangeCollection<string>),
        typeof(DateTimePickerView),
        defaultValue: DateTimeUtil.GetMonthList());
    public ObservableRangeCollection<string> MonthPickerList
    {
        get => (ObservableRangeCollection<string>)GetValue(MonthPickerListProperty);
        set => SetValue(MonthPickerListProperty, value);
    }

    public static readonly BindableProperty DayPickerListProperty = BindableProperty.Create(
        nameof(DayPickerList),
        typeof(ObservableRangeCollection<string>),
        typeof(DateTimePickerView),
        defaultValue: DateTimeUtil.GetDayList(DateTime.Now.Year, DateTime.Now.Month));
    public ObservableRangeCollection<string> DayPickerList
    {
        get => (ObservableRangeCollection<string>)GetValue(DayPickerListProperty);
        set => SetValue(DayPickerListProperty, value);
    }
    public static readonly BindableProperty YearPickerListProperty = BindableProperty.Create(
        nameof(YearPickerList),
        typeof(ObservableRangeCollection<string>),
        typeof(DateTimePickerView),
        defaultValue: DateTimeUtil.GetYearList());
    public ObservableRangeCollection<string> YearPickerList
    {
        get => (ObservableRangeCollection<string>)GetValue(YearPickerListProperty);
        set => SetValue(YearPickerListProperty, value);
    }
    public static readonly BindableProperty HourPickerListProperty = BindableProperty.Create(
        nameof(HourPickerList),
        typeof(ObservableRangeCollection<string>),
        typeof(DateTimePickerView),
        defaultValue: DateTimeUtil.GetHourList());
    public ObservableRangeCollection<string> HourPickerList
    {
        get => (ObservableRangeCollection<string>)GetValue(HourPickerListProperty);
        set => SetValue(HourPickerListProperty, value);
    }
    public static readonly BindableProperty MinPickerListProperty = BindableProperty.Create(
        nameof(MinPickerList),
        typeof(ObservableRangeCollection<string>),
        typeof(DateTimePickerView),
        defaultValue: DateTimeUtil.GetMinList());
    public ObservableRangeCollection<string> MinPickerList
    {
        get => (ObservableRangeCollection<string>)GetValue(MinPickerListProperty);
        set => SetValue(MinPickerListProperty, value);
    }

    public static readonly BindableProperty SelectedMonthProperty = BindableProperty.Create(
        nameof(SelectedMonth),
        typeof(string),
        typeof(DateTimePickerView),
        defaultValue: DateTime.Now.ToString("MMM"),
        propertyChanged: (source, oldValue, newValue) =>
        {
            if (oldValue == null || newValue == null) return;
            if (string.IsNullOrEmpty(oldValue?.ToString()) || string.IsNullOrEmpty(newValue?.ToString())) return;
            var view = source as DateTimePickerView;
            var m = DateTimeUtil.GetMonthInt(newValue.ToString());
            view.DayPickerList = DateTimeUtil.GetDayList(int.Parse(view.SelectedYear), m);
        });
    public string SelectedMonth
    {
        get => (string)GetValue(SelectedMonthProperty);
        set => SetValue(SelectedMonthProperty, value);
    }
    public static readonly BindableProperty SelectedYearProperty = BindableProperty.Create(
        nameof(SelectedYear),
        typeof(string),
        typeof(DateTimePickerView),
        defaultValue: DateTime.Now.ToString("yyyy"),
        propertyChanged: (source, oldValue, newValue) =>
        {
            if (oldValue == null || newValue == null) return;
            if (string.IsNullOrEmpty(oldValue?.ToString()) || string.IsNullOrEmpty(newValue?.ToString())) return;
            var view = source as DateTimePickerView;
            var m = DateTimeUtil.GetMonthInt(view.SelectedMonth);
            view.DayPickerList = DateTimeUtil.GetDayList(int.Parse(newValue.ToString()), m);
        });
    public string SelectedYear
    {
        get => (string)GetValue(SelectedYearProperty);
        set => SetValue(SelectedYearProperty, value);
    }
    public static readonly BindableProperty IsTimeVisibleProperty = BindableProperty.Create(
        nameof(IsTimeVisible),
        typeof(bool),
        typeof(DateTimePickerView),
        defaultValue: true);
    public bool IsTimeVisible
    {
        get => (bool)GetValue(IsTimeVisibleProperty);
        set => SetValue(IsTimeVisibleProperty, value);
    }

    public DateTimePickerView()
	{
		InitializeComponent();
	}
}