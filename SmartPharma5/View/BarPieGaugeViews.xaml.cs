using DevExpress.Maui.Charts;
using SmartPharma5.Model;
using SmartPharma5.ModelView;

namespace SmartPharma5.View;

public partial class BarPieGaugeViews : ContentPage
{
    public string XML_FILE { get; set; }
	public BarPieGaugeViews(string xml_file)
	{
        this.XML_FILE= xml_file;
		InitializeComponent();

		BindingContext = new BarSeriesMV(this.XML_FILE);
}

    private void ChartView_SelectionChanged(object sender, DevExpress.Maui.Charts.SelectionChangedEventArgs e)
    {
        try
        {
            var b = e.SelectedObjects;
            DevExpress.Maui.Charts.DataSourceKey a = e.SelectedObjects[0] as DataSourceKey;
            LandBarItem barSelected = a.DataObject as LandBarItem;
            var ovm = BindingContext as BarSeriesMV;
            ovm.List_BarSeries.FirstOrDefault(p => p.Id == barSelected.Id_BarSerie).Selected_Value1 = Convert.ToString((int)Math.Round(barSelected.Number1));
            ovm.List_BarSeries.FirstOrDefault(p => p.Id == barSelected.Id_BarSerie).Selected_Arguemnt = barSelected.Name;

        }
        catch(Exception ex)
        {

        }
     

      
       
        //ovm.List_BarSeries[barSelected.Id_BarSerie].Selected_Value = Convert.ToString(barSelected.Number1);
        // ovm.List_BarSeries[barSelected.Id_BarSerie].Selected_Arguemnt = barSelected.Name;


        //BarSeriesModel.se = barSelected.Name;
        //BarSeriesModel.Selected_Value =Convert.ToString( barSelected.Number1);





    }

    private void ChartView_SelectionChanged_1(object sender, DevExpress.Maui.Charts.SelectionChangedEventArgs e)
    {
        try
        {
            var b = e.SelectedObjects;
            DevExpress.Maui.Charts.DataSourceKey a = e.SelectedObjects[0] as DataSourceKey;
            LandBarItem barSelected = a.DataObject as LandBarItem;
            var ovm = BindingContext as BarSeriesMV;
            ovm.List_BarSeries.FirstOrDefault(p => p.Id == barSelected.Id_BarSerie).Selected_Value1 = Convert.ToString((int)Math.Round(barSelected.Number1));
            ovm.List_BarSeries.FirstOrDefault(p => p.Id == barSelected.Id_BarSerie).Selected_Arguemnt = barSelected.Name;

            ovm.List_BarSeries.FirstOrDefault(p => p.Id == barSelected.Id_BarSerie).Selected_Value2 = Convert.ToString((int)Math.Round(barSelected.Number2));

        }
        catch(Exception ex)
        {

        }
       



       

    }

    private void bar_chart_SelectionChanged(object sender, DevExpress.Maui.Charts.SelectionChangedEventArgs e)
    {
        try
        {
            var b = e.SelectedObjects;
            DevExpress.Maui.Charts.DataSourceKey a = e.SelectedObjects[0] as DataSourceKey;
            LandBarItem barSelected = a.DataObject as LandBarItem;
            var ovm = BindingContext as BarSeriesMV;
            ovm.List_BarSeries.FirstOrDefault(p => p.Id == barSelected.Id_BarSerie).Selected_Value1 = Convert.ToString((int)Math.Round(barSelected.Number1));
            ovm.List_BarSeries.FirstOrDefault(p => p.Id == barSelected.Id_BarSerie).Selected_Arguemnt = barSelected.Name;

            ovm.List_BarSeries.FirstOrDefault(p => p.Id == barSelected.Id_BarSerie).Selected_Value2 = Convert.ToString((int)Math.Round(barSelected.Number2));



            ovm.List_BarSeries.FirstOrDefault(p => p.Id == barSelected.Id_BarSerie).Selected_Value3 = Convert.ToString((int)Math.Round(barSelected.Number3));
        }
        catch(Exception ex)
        {

        }
       

    }
}