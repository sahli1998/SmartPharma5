using Acr.UserDialogs;
using SmartPharma5.Model;
using SmartPharma5.ViewModel;

namespace SmartPharma5.View;

public partial class PartnerFormView : ContentPage
{
    public string type;
    public DataTemplate ItemTemplate { get; set; }
    public PartnerFormView(string type)
    {
        InitializeComponent();
        this.type = type;
        BindingContext = new PartnerFormViewModel(type);

    }

    private async void DXCollectionView_Tap(object sender, DevExpress.Maui.CollectionView.CollectionViewGestureEventArgs e)
    {
        Popup.IsOpen = true;
        await Task.Delay(1000);
        var Item = e.Item as Partner_Form.Collection;
        if (Item != null)
        {
            if (await DbConnection.Connecter3())
            {
                await App.Current.MainPage.Navigation.PushAsync(new QuizQuestionView((Item)));

            }
            else
            {
                PopupConnection.IsOpen = true;


            }

        }
        Popup.IsOpen = false;



    }


    public void Search()
    {


        bool FiltreIsTrue = true;
        var list = BindingContext as PartnerFormViewModel;
        // var listOfForm = list.ListOfForm;
        var formList = list.PartnerFormList.ToList();
        // listOfForm = (ObservableRangeCollection<string>)formList.Select(x => x.Form_name).Distinct();
        if (PartnerSearch.SelectedItem != null)
        {
            int partner_id = int.Parse(PartnerSearch.SelectedValue.ToString());
            formList = formList.Where(i => i.Partner_id == partner_id).ToList();

            PartnerCollection.ItemsSource = formList;
            FiltreIsTrue = false;
        }
        if (FormSearch.SelectedItem != null)
        {
            string formn_name = FormSearch.SelectedItem.ToString();
            formList = formList.Where(i => i.Form_name.ToLowerInvariant().Contains(formn_name.ToLowerInvariant())).ToList();
            PartnerCollection.ItemsSource = formList;

            FiltreIsTrue = false;
        }
        if (StartEstimatedSearch.Date != null)
        {
            DateTime Estimated_date = StartEstimatedSearch.Date.Value;
            formList = formList.Where(i => i.Estimated_date.Date >= Estimated_date.Date).ToList();
            PartnerCollection.ItemsSource = formList;
            FiltreIsTrue = false;
        }
        if (EndEstimatedSearch.Date != null)
        {
            DateTime Estimated_date = EndEstimatedSearch.Date.Value;
            formList = formList.Where(i => i.Estimated_date.Date <= Estimated_date.Date).ToList();
            PartnerCollection.ItemsSource = formList;
            FiltreIsTrue = false;
        }
        if (StartCloseDateSearch.Date != null)
        {
            DateTime Close_date = StartCloseDateSearch.Date.Value;
            formList = formList.Where(i => i.Close_date >= Close_date.Date).ToList();
            PartnerCollection.ItemsSource = formList;
            FiltreIsTrue = false;
        }
        if (EndCloseDateSearch.Date != null)
        {
            DateTime Close_date = EndCloseDateSearch.Date.Value;
            formList = formList.Where(i => i.Close_date <= Close_date.Date).ToList();
            PartnerCollection.ItemsSource = formList;
            FiltreIsTrue = false;
        }

        if (CycleSearch.SelectedItem != null)
        {
            int Cycle_id = int.Parse(CycleSearch.SelectedValue.ToString());
            formList = formList.Where(i => i.Cycle_id == Cycle_id).ToList();
            PartnerCollection.ItemsSource = formList;
            FiltreIsTrue = false;
        }
        if (EmployeSearch.SelectedItem != null)
        {
            int Employe_id = int.Parse(EmployeSearch.SelectedValue.ToString());
            formList = formList.Where(i => i.Agent_id == Employe_id).ToList();
            PartnerCollection.ItemsSource = formList;
            FiltreIsTrue = false;
        }
        if (StateSearch.SelectedItem != null)
        {
            try
            {
                var str = StateSearch.SelectedValue.ToString();
                formList = formList.Where(x => x.state == str).ToList();
                PartnerCollection.ItemsSource = formList;
                FiltreIsTrue = false;
            }
            catch (Exception e)
            {

            }


        }
        if (FiltreIsTrue)
        {
            PartnerCollection.ItemsSource = list.PartnerFormList.ToList();
        }
        //UserDialogs.Instance.HideLoading();
    }


    //private void SimpleButton_Clicked(object sender, EventArgs e)
    //{

    //    PartnerSearch.SelectedItem =  CycleSearch.SelectedItem = StateSearch.SelectedItem = StartEstimatedSearch.Date = EndEstimatedSearch.Date = StartCloseDateSearch.Date = EndCloseDateSearch.Date= null;
    //    FormSearch.SelectedItem = null;
    //    Search();

    //}

    private void Search_Changed(object sender, EventArgs e)
    {
        Search();
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        //UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
        //await Task.Delay(500);
        var vm = BindingContext as PartnerFormViewModel;
        vm.Loading = true;
        await Task.Delay(1000);
        await App.Current.MainPage.Navigation.PushAsync(new QuizPartnerFormCalender(vm.PartnerFormList));
        vm.Loading = false;
       // UserDialogs.Instance.HideLoading();
    }

    protected override void OnAppearing()
    {
        /*PartnerCollection.ItemsSource = null;
        base.OnAppearing();

        var OVM = BindingContext as PartnerFormViewModel;
        if (this.type == "My_Forms")
        {
            Task.Run(() => OVM.LoadMyPartnerForm());

            this.Title = "My Partner Forms";
            PartnerCollection.ItemsSource = OVM.PartnerFormList;
        }

        else
        {
            Task.Run(() => OVM.LoadAllPartnerForm());
            this.Title = "All Partner Forms";
        }*/


    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        Popup.IsOpen = true;
        await Task.Delay(1000);
        if (sender is Frame frame && frame.BindingContext is Partner_Form.Collection tappedItem)
        {
            if (await DbConnection.Connecter3())
            {
                try
                {
                    await App.Current.MainPage.Navigation.PushAsync(new QuizQuestionView((tappedItem)));
                }
                catch(Exception ex) 
                { }
               

            }
            else
            {
                PopupConnection.IsOpen = true;
            }
        }
        Popup.IsOpen = false;
    }

    private void SimpleButton_Clicked(object sender, EventArgs e)
    {
        PartnerSearch.SelectedItem = CycleSearch.SelectedItem = StateSearch.SelectedItem = StartEstimatedSearch.Date = EndEstimatedSearch.Date = StartCloseDateSearch.Date = EndCloseDateSearch.Date = null;
           FormSearch.SelectedItem = null;
        EmployeSearch.SelectedItem = null;
            Search();

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        filtredPannel.IsOpen = true;
    }

    private void SimpleButton_Clicked_1(object sender, EventArgs e)
    {
        filtredPannel.IsOpen = false;
    }
}