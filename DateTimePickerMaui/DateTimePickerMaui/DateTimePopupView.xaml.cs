using Mopups.Pages;
using Mopups.Services;

namespace DateTimePickerMaui;

public partial class DateTimePopupView : PopupPage
{
    public static readonly BindableProperty IsTimeVisibleProperty = BindableProperty.Create(
       nameof(IsTimeVisible),
       typeof(bool),
       typeof(DateTimePopupView),
       defaultValue: true);
    public bool IsTimeVisible
    {
        get => (bool)GetValue(IsTimeVisibleProperty);
        set => SetValue(IsTimeVisibleProperty, value);
    }
    private readonly TaskCompletionSource<string> task;
    public DateTimePopupView(TaskCompletionSource<string> taskCompletion)
    {
        InitializeComponent();
        task = taskCompletion;
    }
    /// <summary>
    /// Disable back button: return true means disable back button.
    /// Enable back button: return false means enable back button 
    /// </summary>
    /// <returns>it will return true</returns>
    protected override bool OnBackButtonPressed()
    {
        return true;
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        if (task != null)
            if (!task.Task.IsCanceled && !task.Task.IsFaulted && !task.Task.IsCompleted)
                task.SetCanceled();
    }
    private async void Yes_Tapped(object sender, EventArgs e)
    {
        try
        {
            if (!task.Task.IsCanceled && !task.Task.IsFaulted && !task.Task.IsCompleted)
            {
                string Date = string.Empty;
                if (DateTimePicker.FindByName("MonthPicker") is PickerView MonthPicker)
                {
                    Date = MonthPicker.SelectedValue;
                }
                if (DateTimePicker.FindByName("DayPicker") is PickerView DayPicker)
                {
                    Date = $"{Date} {DayPicker.SelectedValue}";
                }
                if (DateTimePicker.FindByName("YearPicker") is PickerView YearPicker)
                {
                    Date = $"{Date} {YearPicker.SelectedValue}";
                }
                if (DateTimePicker.FindByName("HourPicker") is PickerView HourPicker)
                {
                    Date = $"{Date} {HourPicker.SelectedValue}";
                }
                if (DateTimePicker.FindByName("MinutePicker") is PickerView MinutePicker)
                {
                    Date = $"{Date}:{MinutePicker.SelectedValue}";
                }
                task?.SetResult(Date);
                await MopupService.Instance?.RemovePageAsync(this);
            }
        }
        catch (Exception)
        {
            //
        }
    }
    private async void No_Tapped(object sender, EventArgs e)
    {
        try
        {
            if (!task.Task.IsCanceled && !task.Task.IsFaulted && !task.Task.IsCompleted)
            {
                task.SetCanceled();
                await MopupService.Instance?.RemovePageAsync(this);
            }
        }
        catch (Exception)
        {
            //
        }
    }
}