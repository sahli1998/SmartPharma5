using Acr.UserDialogs;
using SmartPharma5.Model;
using SmartPharma5.ViewModel;

namespace SmartPharma5.View;

public partial class HomeView : ContentPage
{
    public HomeView()
    {
        InitializeComponent();
        var grid = globle_grid as Grid;
        user_contrat.getBtnIInvisibles().GetAwaiter();


        foreach (string btn in user_contrat.ListInVisibleBtn)
        {
            StackLayout st1 = (StackLayout)FindByName(btn);
            st1.IsVisible = false;
        }

        /*   Grid gridInsideFrame = (Grid)frame.Content;
      StackLayout stackLayout1 = (StackLayout)gridInsideFrame.FindByName("achat_all_opportunity");
      if (stackLayout1 != null)
      {

      }*/





        //Popup.IsOpen = false;
        //Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

        var ovm = BindingContext as HomeViewModel;
        TitleValidation.Text = "(" + ovm.LoadNumbers().Result.ToString() + ") Validations";

        var a = Shell.Current;
    }
    protected  override bool OnBackButtonPressed()
    {
        App.Current.MainPage.DisplayAlert("INFO", "Go To Menu", "OK");
        return true;
    }

    private void ImageButton_Clicked(object sender, EventArgs e)
    {
        ScrolView.ScrollToAsync(0, 0, true);
        var OVM = BindingContext as HomeViewModel;
        OVM.MenuVisisble = !OVM.MenuVisisble;
    }


    private async void TapGestureRecognizer_Tapped_sales(object sender, TappedEventArgs e)
    {
        var OVM = BindingContext as HomeViewModel;
        OVM.MenuVisisble = !OVM.MenuVisisble;
        UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
        await Task.Delay(500);
        await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new ShowMenuItemPages()));
        UserDialogs.Instance.HideLoading();
    }
    private async void TapGestureRecognizer_Tapped_payment(object sender, TappedEventArgs e)
    {
        UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
        await Task.Delay(500);
        await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new PaymentModule()));
        UserDialogs.Instance.HideLoading();
    }
    private async void TapGestureRecognizer_Tapped_marketing(object sender, TappedEventArgs e)
    {
        UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
        await Task.Delay(500);
        //await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new module()));
        UserDialogs.Instance.HideLoading();

    }
    private async void TapGestureRecognizer_Tapped_profiling(object sender, TappedEventArgs e)
    {
        UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
        await Task.Delay(500);
        await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new ProfilingModule()));
        UserDialogs.Instance.HideLoading();

    }
    private async void TapGestureRecognizer_Tapped_summary(object sender, TappedEventArgs e)
    {

        
        var OVM = BindingContext as HomeViewModel;
        OVM.MenuVisisble = !OVM.MenuVisisble;
        UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
        await Task.Delay(500);
        if (OVM.AllOppIsVisible)
        {

            await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new SammaryView()));
        }
        else
        {
            await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new SammaryView((uint)Preferences.Get("idagent", Convert.ToUInt32(null)))));
       }
       
        UserDialogs.Instance.HideLoading();
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
        await Task.Delay(500);
        await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new PieDashboard()));
        UserDialogs.Instance.HideLoading();
    }
}