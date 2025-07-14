namespace WeightAndSee.Services;

public interface IViewCreatorService
{
    //public View CreateDisplayContent(ObservableCollection<KiloPlateModel> leftPlates, ObservableCollection<KiloPlateModel> rightPlates,
    //                                    int leftOffset, int rightOffset, int plateWidth, int plateViewTranslationY, int plateSpacing);
    BaseModel Bar { get; set; }
    
    View CreateDisplayContent();

    View CreatePlateOnBarView(KiloPlateModel plate);
     //ContentView _displayView {get; set;}

    ContentView DisplayItem { get; }

    ContentView CreateItemForDisplay();
    void ResetBarDisplay();
    
    void SetViewBar(BaseModel bar);

    void RefreshDisplay();

    void Plates_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e);

    ContentView BarReportView(BaseModel bar);

    Image CreateSinglePlateView(KiloPlateModel plate);

    //public Line BarLine { get; }

}
