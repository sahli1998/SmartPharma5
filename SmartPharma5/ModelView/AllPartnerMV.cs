//using GameplayKit;
using MvvmHelpers;

/* Modification non fusionnée à partir du projet 'SmartPharma5 (net7.0-ios)'
Avant :
using SmartPharma5.Model;
using System;
Après :
using MvvmHelpers.Commands;
using SmartPharma5.Model;
using SmartPharma5.View;
using System;
*/
using MvvmHelpers.Commands;
using SmartPharma5.
/* Modification non fusionnée à partir du projet 'SmartPharma5 (net7.0-ios)'
Avant :
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MvvmHelpers.Commands;
using SmartPharma5.View;
using Color = System.Drawing.Color;
Après :
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = System.Drawing.Color;
*/
Model;
using SmartPharma5.Services;
using SmartPharma5.View;
using Color = System.Drawing.Color;

namespace SmartPharma5.ViewModel
{
    public class AllPartnerMV : BaseViewModel
    {
        private List<Partner> partners;
        public List<Partner> Partners { get => partners; set => SetProperty(ref partners, value); }

        private List<string> category_list;
        public List<string> Category_list { get => category_list; set => SetProperty(ref category_list, value); }

        private List<string> state_list;
        public List<string> State_list { get => state_list; set => SetProperty(ref state_list, value); }

        public bool actpopup = true;
        public bool ActPopup { get => actpopup; set => SetProperty(ref actpopup, value); }


        public bool homeEnabled;
        public bool HomeEnabled { get => homeEnabled; set => SetProperty(ref homeEnabled, value); }



        private bool filtred = false;

        /* Modification non fusionnée à partir du projet 'SmartPharma5 (net7.0-ios)'
        Avant :
                public bool Filtred { get => filtred; set => SetProperty(ref filtred, value); }



                public AsyncCommand TapCommand2 { get; }
        Après :
                public bool Filtred { get => filtred; set => SetProperty(ref filtred, value); }



                public AsyncCommand TapCommand2 { get; }
        */
        public bool Filtred { get => filtred; set => SetProperty(ref filtred, value); }



        public AsyncCommand TapCommand2 { get; }

        private System.Drawing.Color stateColor;
        public Color StateColor { get => stateColor; set => SetProperty(ref stateColor, value); }

        private string stateString;
        public string StateString { get => stateString; set => SetProperty(ref stateString, value); }


        public AsyncCommand HomePage { get; }

        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand FilterCommand { get; }

        public AsyncCommand NoFilterCommand { get; }

        public bool ispulltorefreshenabled = false;
        public bool IsPullToRefreshEnabled { get => ispulltorefreshenabled; set => SetProperty(ref ispulltorefreshenabled, value); }

        public AsyncCommand TapCommand { get; set; }

        private bool testLoad;
        public bool TestLoad { get => testLoad; set => SetProperty(ref testLoad, value); }

        public bool isBtnFilter = true;
        public bool IsBtnFilter { get => isBtnFilter; set => SetProperty(ref isBtnFilter, value); }


        private bool btnfiltred;
        public bool BtnFiltred { get => btnfiltred; set => SetProperty(ref btnfiltred, value); }


        public AllPartnerMV()
        {
            HomePage = new AsyncCommand(homePage);

            FilterCommand = new AsyncCommand(ChangeToFilter);
            NoFilterCommand = new AsyncCommand(ChangeToNoFilter);
            RefreshCommand = new AsyncCommand(Refresh);
            TapCommand = new AsyncCommand(TapCommandAsync);
            TapCommand2 = new AsyncCommand(tapFonc);


            try
            {
                Partners = new List<Partner>();
                Task.Run(() => RefreshOnApp());




            }
            catch (Exception ex)
            {
                TestLoad = true;
            }





        }
        private async Task tapFonc()
        {

        }
        private async Task homePage()
        {

            try
            {

                await App.Current.MainPage.Navigation.PushAsync(new HomeView());
            }
            catch (Exception ex)
            {
                await DbConnection.ErrorConnection();
            }

        }
        private async Task TapCommandAsync()
        {
            TestLoad = false;

        }
        private async Task ChangeToFilter()
        {
            Filtred = true;
            IsBtnFilter = false;
        }
        private async Task ChangeToNoFilter()
        {
            Filtred = false;
            IsBtnFilter = true;
        }
        public async Task Refresh()
        {
            Partners = new List<Partner>();
            BtnFiltred = false;
            int CrmGroupe = 0;
            int iduser = Preferences.Get("iduser", 0);
            var UMG = await User_Module_Groupe_Services.GetGroupeCRM(iduser);

            if (UMG != null)
            {
                CrmGroupe = UMG.IdGroup;
            }


            IsPullToRefreshEnabled = false;
           // ActPopup = true;
            /* IsBusy = false;
             HomeEnabled = false;
             BtnFiltred = false;
             bool Testcon = false;

             var O = Task.Run(() => DbConnection.Connecter3());
             Testcon = await O;
             if (Testcon == false)
             {


                 //TestLoad = true;
                 Partners = new List<Partner>();
                 IsBusy = false;

                 return;
             }
             Partners = new List<Partner>();
             TestLoad = false;*/
            try
            {
                if (CrmGroupe == 32)
                {
                    uint idagent = (uint)Preferences.Get("idagent", Convert.ToUInt32(null));
                    var P = Task.Run(() => Partner.GetPartnerListByAgent(idagent));
                    Partners = new List<Partner>(await P);
                    Category_list = Partners.OrderBy(x => x.Category_Name).Select(x => x.Category_Name.ToLowerInvariant()).Distinct().ToList();
                    State_list = Partners.OrderBy(x => x.State).Select(x => x.State.ToLowerInvariant()).Distinct().ToList();

                }
                else
                {
                    var P = Task.Run(() => Partner.GetPartnerList());
                    Partners = new List<Partner>(await P);
                    Category_list = Partners.OrderBy(x => x.Category_Name).Select(x => x.Category_Name.ToLowerInvariant()).Distinct().ToList();
                    State_list = Partners.OrderBy(x => x.State).Select(x => x.State.ToLowerInvariant()).Distinct().ToList();
                }

            }
            catch (Exception ex)
            {
                TestLoad = true;
            }
            ActPopup = false;

            IsPullToRefreshEnabled = true;
            BtnFiltred = true;
            HomeEnabled = true;


            /* IsBusy = false;
             BtnFiltred = true;
             HomeEnabled = true;*/

        }


        public async Task RefreshOnApp()
        {
            BtnFiltred = false;


            IsPullToRefreshEnabled = false;
            ActPopup = true;


            int CrmGroupe = 0;
            int iduser = Preferences.Get("iduser", 0);
            var UMG = await User_Module_Groupe_Services.GetGroupeCRM(iduser);

            if (UMG != null)
            {
                CrmGroupe = UMG.IdGroup;
            }
            await Task.Delay(500);

            /* bool Testcon = false;

             var O = Task.Run(() => DbConnection.Connecter());
             Testcon = await O;


             if (Testcon == false)
             {
                 ActPopup = false;

                 TestLoad = true;
                 IsPullToRefreshEnabled = true;

                 return;
             }*/

            try
            {

                if (CrmGroupe == 32)
                {
                    uint idagent = (uint)Preferences.Get("idagent", Convert.ToUInt32(null));
                    var P = Task.Run(() => Partner.GetPartnerListByAgent(idagent));
                    Partners = new List<Partner>(await P);
                    Category_list = Partners.OrderBy(x => x.Category_Name).Select(x => x.Category_Name.ToLowerInvariant()).Distinct().ToList();
                    State_list = Partners.OrderBy(x => x.State).Select(x => x.State.ToLowerInvariant()).Distinct().ToList();

                }
                else
                {
                    var P = Task.Run(() => Partner.GetPartnerList());
                    Partners = new List<Partner>(await P);
                    Category_list = Partners.OrderBy(x => x.Category_Name).Select(x => x.Category_Name.ToLowerInvariant()).Distinct().ToList();
                    State_list = Partners.OrderBy(x => x.State).Select(x => x.State.ToLowerInvariant()).Distinct().ToList();
                }

               
                   // var C = Task.Run(() => Partner.GetPartnaireByIdAgent(idagent));
                   // Partners = new List<Partner>(await C);
              

               

            }
            catch (Exception ex)
            {
                TestLoad = true;
            }













            ActPopup = false;

            IsPullToRefreshEnabled = true;
            BtnFiltred = true;
            HomeEnabled = true;

            // var O = Task.Run(() => Collection.GetOpportunityByAgent((uint)agentId));
            //OpportunityList = new ObservableRangeCollection<Collection>(await O);





            //  var O = Task.Run(() => Partner.GetPartnerList());
            //  Partners = new List<Partner>(await O);





            // Category_list = Partners.OrderBy(x => x.Category_Name).Select(x => x.Category_Name.ToLowerInvariant()).Distinct().ToList();
            // State_list = Partners.OrderBy(x => x.State).Select(x => x.State.ToLowerInvariant()).Distinct().ToList();





        }
        public async Task ChangeConnexionState()
        {
            bool test = DbConnection.Connecter();
            DbConnection.Deconnecter();

            if (!test)
            {
                StateColor = Color.Red;
                StateString = "Déconnecté";


            }
            else
            {
                StateColor = Color.GreenYellow;
                StateString = "Connecté";

            }

            await Task.Delay(2000);





        }
    }
}
