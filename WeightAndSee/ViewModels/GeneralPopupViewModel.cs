
namespace WeightAndSee.ViewModels;
public partial class GeneralPopupViewModel : BasePopupViewModel
{
    public GeneralPopupViewModel(IPopupService popupService) : base(popupService)
    {

    }

    //needed for source generator because of [relaycommand] and [observableproperty]
    public GeneralPopupViewModel() : base(null)
    {

    }



   
}
