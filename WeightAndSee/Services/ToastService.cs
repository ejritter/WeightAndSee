namespace WeightAndSee.Services;
public class ToastService : IToastService
{
    public async Task ShowToastAsync(string message, ToastDuration toastDuration = ToastDuration.Short, double fontSize = 14)
    {
        var toast = Toast.Make(message, toastDuration, fontSize);
        await toast.Show();
    }
}
