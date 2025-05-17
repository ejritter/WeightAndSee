#if DEBUG
[assembly:System.Reflection.Metadata.MetadataUpdateHandlerAttribute(typeof(WeightAndSee.Services.HotReloadService))]
namespace WeightAndSee.Services;
public static class HotReloadService
{
    public static event Action<Type[]?>? UpdateApplicationEvent;

    internal static void ClearCache(Type[]? types) { }

    internal static void UpdateApplication(Type[]? types)
    {
        UpdateApplicationEvent?.Invoke(types);
    }
}
#endif