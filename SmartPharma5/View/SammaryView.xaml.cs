using Acr.UserDialogs;
using DevExpress.Maui.Charts;
using SmartPharma5.ViewModel;
using System;
using System.Globalization;

namespace SmartPharma5.View;

public partial class SammaryView : ContentPage
{
    public SammaryView()
    {

        InitializeComponent();

        BindingContext = new SammaryViewModel();
        /*  PieLabel.TextProvider = new LabelTextProvider();
          PieLabel2.TextProvider = new LabelTextProvider();
          PieLabel3.TextProvider = new LabelTextProvider();
          bar_chart2.TextProvider = new LabelTextProvider();
          jauge.FromColor = Color.FromRgb(76, 191, 72);
          jauge.ToColor = Color.FromRgb(76, 191, 72);
          jauge.ViaColor = Color.FromRgb(76, 191, 72);*/

    }
    public SammaryView(uint idAgent)
    {

        InitializeComponent();
        BindingContext = new SammaryViewModel(idAgent);
        /*  PieLabel.TextProvider = new LabelTextProvider();
          PieLabel2.TextProvider = new LabelTextProvider();
          PieLabel3.TextProvider = new LabelTextProvider();
          jauge.FromColor = Color.FromRgb(34, 150, 243);
          jauge.ToColor = Color.FromRgb(255, 83, 83);
          jauge.ViaColor = Color.FromRgb(76, 175, 80);*/

    }
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        UserDialogs.Instance.ShowLoading("Loading Please wait ...");

        // Ajoutez un délai de 1000 ms (1 seconde)
        await Task.Delay(1000);

        await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new HomeView()));

        // Attendez que la page soit poussée et affichée avant de masquer le chargement
        UserDialogs.Instance.HideLoading();
    }
    private void bar_chart2_SelectionChanged(object sender, DevExpress.Maui.Charts.SelectionChangedEventArgs e)
    {

        //var itemselcted = e.SelectedObjects.FirstOrDefault();
        // var item = e.SelectedObjects[0] is DataSourceKey dataSourceKey;
        var OVM = BindingContext as SammaryViewModel;
        if (e.SelectedObjects.Count > 0 && e.SelectedObjects[0] is DataSourceKey dataSourceKey)
        {
            if (dataSourceKey.DataObject is LandAreaItem bubbleDataObject)
            {

                ScrolView.ScrollToAsync(FilterTitle, ScrollToPosition.Center, true);
                OVM.IdJobPosition = 0;
                OVM.IdAgent = (uint)bubbleDataObject.Id; ;
                OVM.LoadByAgent();
                OVM.agentName = bubbleDataObject.Name;

            }
        }
        else
        {

            ScrolView.ScrollToAsync(FilterTitle, ScrollToPosition.Center, true);
            OVM.IdAgent = 0;
            OVM.Load();
            SelectedAgentName.Text = "All Agent";

        }

    }
    private void ChartView_SelectionChanged(object sender, DevExpress.Maui.Charts.SelectionChangedEventArgs e)
    {

        var OVM = BindingContext as SammaryViewModel;
        if (e.SelectedObjects.Count > 0 && e.SelectedObjects[0] is DataSourceKey dataSourceKey)
        {
            if (dataSourceKey.DataObject is LandAreaItem bubbleDataObject)
            {

                ScrolView.ScrollToAsync(FilterTitle, ScrollToPosition.Center, true);
                OVM.IdAgent = 0;
                OVM.IdJobPosition = (int)bubbleDataObject.Id; ;
                OVM.LoadByJobPosition((int)bubbleDataObject.Id);
                OVM.agentName = bubbleDataObject.Name;

            }
        }
        else
        {

            ScrolView.ScrollToAsync(FilterTitle, ScrollToPosition.Center, true);
            OVM.IdAgent = 0;
            OVM.Load();
            SelectedAgentName.Text = "All Agent";

        }

    }
    public class LabelTextProvider : ISeriesLabelTextProvider
    {


        string ISeriesLabelTextProvider.GetText(SeriesLabelValuesBase values)
        {

            if (values is PieSeriesLabelValues seriesValues)
            {
                double v = seriesValues.Value;
                if (v >= 1000000000 || v <= -1000000000)
                    return (v / 1000000000.0).ToString("#.###B", CultureInfo.InvariantCulture);
                else if (v >= 1000000 || v <= -1000000)
                    return (v / 1000000.0).ToString("#.###M", CultureInfo.InvariantCulture);
                else if (v >= 1000 || v <= -1000)
                    return (v / 1000.0).ToString("#.###K", CultureInfo.InvariantCulture);
                else
                    return v.ToString();
            }

            return String.Empty;
        }

    }

   
}