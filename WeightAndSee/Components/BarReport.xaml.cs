namespace WeightAndSee.Components;

public partial class BarReport : ContentView
{
	public BarReport()
	{
		InitializeComponent();
	}

	//BarrReportViewProperty
	public static readonly BindableProperty BarReportViewProperty =
		BindableProperty.Create(
			propertyName:nameof(BarReportView),
			returnType:typeof(ContentView),
			declaringType:typeof(BarReport));

	public ContentView BarReportView
	{
		get => (ContentView)GetValue(BarReportViewProperty);
		set => SetValue(BarReportViewProperty, value);
	}

    //BarViewProperty
	public static readonly BindableProperty BarViewProperty =
		BindableProperty.Create(
			propertyName:nameof(BarView),
			returnType:typeof(ContentView),
			declaringType:typeof(BarReport));

	public ContentView BarView
	{
		get => (ContentView)GetValue(BarViewProperty);
		set => SetValue(BarViewProperty, value);
    }
}