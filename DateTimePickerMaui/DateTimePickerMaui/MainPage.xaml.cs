using Mopups.Services;

namespace DateTimePickerMaui;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnClicked(object sender, EventArgs e)
	{
        var taskResult = new TaskCompletionSource<string>();
        DateTimePopupView dateTimePopupView = new DateTimePopupView(taskResult)
        {
            IsTimeVisible = false
        };
        await MopupService.Instance?.PushAsync(dateTimePopupView);
        await taskResult.Task.ContinueWith(result =>
        {
            if (result.IsCompleted && !result.IsCanceled)
            {
                bool isSuccess = DateTime.TryParse(result?.Result, out DateTime NewDate);
                if (!isSuccess) return;
                myBirthdate.Text = NewDate.ToString("MMM dd yyyy");
            }
        });
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var taskResult = new TaskCompletionSource<string>();
        DateTimePopupView dateTimePopupView = new DateTimePopupView(taskResult)
        {
            IsTimeVisible = true
        };
        await MopupService.Instance?.PushAsync(dateTimePopupView);
        await taskResult.Task.ContinueWith(result =>
        {
            if (result.IsCompleted && !result.IsCanceled)
            {
                bool isSuccess = DateTime.TryParse(result?.Result, out DateTime NewDate);
                if (!isSuccess) return;
                mydatetime.Text = NewDate.ToString("MMM dd yyyy HH:mm");
            }
        });
    }
}

