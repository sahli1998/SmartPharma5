using Acr.UserDialogs;
using DevExpress.Maui.Charts;
using MvvmHelpers;
using MvvmHelpers.Commands;
using MySqlConnector;
using SmartPharma5;
using SmartPharma5.Model;
using SmartPharma5.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ChartView = SmartPharma5.View.ChartView;

namespace SmartPharma5.ModelView
{
    public class DashboardinMV : BaseViewModel
    {
    

     


       




        private List<DashBoardingModel> list_Dashboard;
        public List<DashBoardingModel> List_Dashboard { get => list_Dashboard; set => SetProperty(ref list_Dashboard, value); }



        public AsyncCommand GoToGrid { get; }
        public AsyncCommand MyDashBoardCommand { get; }
        public AsyncCommand AllDashBoardCommand { get; }

        public DashboardinMV()
        {


            List_Dashboard = GetListDashboard().Result;

            GoToGrid = new AsyncCommand(goToGrid);
            MyDashBoardCommand = new AsyncCommand(MyDash);
            AllDashBoardCommand = new AsyncCommand(AllDash);

        }

        public async Task MyDash()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
                await Task.Delay(500);
                await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new SammaryView((uint)Preferences.Get("idagent", Convert.ToUInt32(null)))));
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Warning", "VERIFY YOUR XML FILE IS SUCCEFULY INTEGRATED!", "OK");
            }

        }
        public async Task AllDash()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
                await Task.Delay(500);
                await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new SammaryView()));
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                await DbConnection.ErrorConnection();
            }

        }

        public async Task goToGrid()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
                await Task.Delay(500);
               // await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new ChartView()));
                UserDialogs.Instance.HideLoading();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await DbConnection.ErrorConnection();
            }
            UserDialogs.Instance.HideLoading();


        }
        public async  Task<List<DashBoardingModel>> GetListDashboard()
        {
            List<DashBoardingModel> list = new List<DashBoardingModel>();
           
            try
            {
                string sqlCmd1 = "SELECT Id,name FROM atooerp_app_dashboard";

                DbConnection.Deconnecter();
                DbConnection.Connecter();

                MySqlCommand cmd = new MySqlCommand(sqlCmd1, DbConnection.con);

                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {

                        list.Add(new DashBoardingModel(Convert.ToInt32(reader["Id"]), reader["name"].ToString())) ;
                        



                       



                    }
                    catch (Exception ex)
                    {
                        return null;

                    }

                }
                reader.Close();
                DbConnection.Deconnecter();




            }
            catch (Exception e)
            {
                return null;

            }



            return list;




        }
    }

}
public class DashBoardingModel{
    public int Id { get; set; }
    public string Name { get; set; }
    public DashBoardingModel(int id, string name)
    {
        Id = id;
        Name = name;
    }
    public async Task GoTOMyDashboar()
    {
        try
        {
            UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
            await Task.Delay(500);
            await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new DashBoardCustomazedView(this.Id)));
            UserDialogs.Instance.HideLoading();
        }
        catch (Exception ex)
        {
            UserDialogs.Instance.HideLoading();
            await DbConnection.ErrorConnection();
        }
        UserDialogs.Instance.HideLoading();

    }
}
