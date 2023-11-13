using SmartPharma5.ViewModel;

namespace SmartPharma5.View;

public partial class ProfileUpdate : ContentPage
{
    public ProfileUpdate(uint a)
    {
        InitializeComponent();
        BindingContext = new UpdateProfileMV(a);
    }

}