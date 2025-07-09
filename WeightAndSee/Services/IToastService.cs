namespace WeightAndSee.Services;
public interface IToastService
{
    Task ShowToastAsync(string message, ToastDuration duration = ToastDuration.Short, double fontSize = 14);
}
