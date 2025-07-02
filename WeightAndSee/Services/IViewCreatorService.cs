namespace WeightAndSee.Services;

public interface IViewCreatorService
{
    //public View CreateDisplayContent(ObservableCollection<KiloPlateModel> leftPlates, ObservableCollection<KiloPlateModel> rightPlates,
    //                                    int leftOffset, int rightOffset, int plateWidth, int plateViewTranslationY, int plateSpacing);
    BaseModel Bar { get; set; }
    
    View CreateDisplayContent();

    View CreatePlateView(KiloPlateModel plate);
     ContentView DisplayView {get; set;}

    ContentView DisplayItem { get; }

    ContentView CreateItemForDisplay();
    void ResetBarDisplay();

    void RefreshDisplay();

    void Plates_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e);

    //public Line BarLine { get; }

}
