using SmartPharma5.ViewModel;

namespace SmartPharma5.View;

public partial class PartnerTempAttributesView : ContentPage
{
    public PartnerTempAttributesView(int partner_temp, int id_profile)
    {
        InitializeComponent();
        BindingContext = new PartnerTempAttributesMV(partner_temp, id_profile);
    }
}