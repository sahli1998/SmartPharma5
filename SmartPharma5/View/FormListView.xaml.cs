using SmartPharma5.Model;
using SmartPharma5.ViewModel;

namespace SmartPharma5.View;

public partial class FormListView : ContentPage
{
    public Partner Partner { get; set; }
    public FormListView()
    {
        InitializeComponent();
        BindingContext = new FormListViewModel();
        var OVM = BindingContext as FormListViewModel;
        //Task.Run(()=> OVM.Reload());
    }
    public FormListView(Partner partner)
    {
        Partner = partner;
        InitializeComponent();
        BindingContext = new FormListViewModel(Partner);
        var OVM = BindingContext as FormListViewModel;
        //Task.Run(()=> OVM.Reload());
    }
    public FormListView(int category)
    {
        //Partner = partner;
        InitializeComponent();
        BindingContext = new FormListViewModel(category);
        var OVM = BindingContext as FormListViewModel;
        //Task.Run(()=> OVM.Reload());
    }

    public async void DXCollectionView_Tap(object sender, DevExpress.Maui.CollectionView.CollectionViewGestureEventArgs e)
    {

        var Item = e.Item as Form;
        if (Item != null)
        {
            try
            {
                //SavingPopup.IsOpen=true;
                //await Task.Delay(1000);

                var r = Partner_Form.InsertPartnerFormAndGetLastId(Partner, Item);
                int? idpartenerform = await r;
                if (idpartenerform != null)
                {
                    var p = Partner_Form.GetPartnerFormById(idpartenerform);
                    SavingPopup.IsOpen = false;
                    SuccessPopup.IsOpen = true;
                    await Task.Delay(1000);
                    SuccessPopup.IsOpen = false;
                    await App.Current.MainPage.Navigation.PushAsync(new QuizQuestionView(p));

                }
                else
                {
                    Popup.IsOpen = true;
                    //SuccessPopup.IsOpen = false;


                }
            }
            catch
            {
                Popup.IsOpen = true;
                // SuccessPopup.IsOpen = false;
                // await DisplayAlert("Warning", "There is a problem in the server", "Ok");

            }

        }
        else
        {
            Popup.IsOpen = true;
            //await DisplayAlert("Warrning", "Select a Cycle", "Ok");
        }
        //Partner_Form partner_form = new Partner_Form()
        //await App.Current.MainPage.Navigation.PushAsync(new FormListView(Item));



    }

    private async void OnItemTapped(object sender, EventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is Form tappedItem)
        {
            try
            {
                //SavingPopup.IsOpen=true;
                //await Task.Delay(1000);

                var r = Partner_Form.InsertPartnerFormAndGetLastId(Partner, tappedItem);
                int? idpartenerform = await r;
                if (idpartenerform != null)
                {
                    var p = Partner_Form.GetPartnerFormById(idpartenerform);
                    SavingPopup.IsOpen = false;
                    SuccessPopup.IsOpen = true;
                    await Task.Delay(1000);
                    SuccessPopup.IsOpen = false;
                    await App.Current.MainPage.Navigation.PushAsync(new QuizQuestionView(p));

                }
                else
                {
                    Popup.IsOpen = true;
                    //SuccessPopup.IsOpen = false;


                }
            }
            catch
            {
                Popup.IsOpen = true;
                // SuccessPopup.IsOpen = false;
                // await DisplayAlert("Warning", "There is a problem in the server", "Ok");

            }
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();


    }
}