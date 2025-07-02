namespace WeightAndSee.Extensions;
public static class ViewCreatorExtensions
{
    public static void SetViewBar(this IViewCreatorService viewCreator, BaseModel bar)
    {
        viewCreator.Bar = bar;
    }
}
