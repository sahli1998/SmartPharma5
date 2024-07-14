﻿using Acr.UserDialogs;
using MvvmHelpers.Commands;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPharma5.Model;
using SmartPharma5.View;
using SmartPharma5.Services;
using MySqlConnector;

namespace SmartPharma5.ModelView
{
    public class AllPartnerMV1 : BaseViewModel
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

        private Color stateColor;
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


        private bool testConnection;
        public bool TestConnection { get => testConnection; set => SetProperty(ref testConnection, value); }

        public AllPartnerMV1()
        {


            HomePage = new AsyncCommand(homePage);

            FilterCommand = new AsyncCommand(ChangeToFilter);
            NoFilterCommand = new AsyncCommand(ChangeToNoFilter);
            RefreshCommand = new AsyncCommand(RefreshForPartnerForm);
            TapCommand = new AsyncCommand(TapCommandAsync);
            TapCommand2 = new AsyncCommand(tapFonc);


            try
            {
                Partners = new List<Partner>();
                Task.Run(async () => await RefreshOnAppForPartnerForm());




            }
            catch (Exception ex)
            {
                TestLoad = true;
            }





        }
        static async Task<bool> checkPermissionQuiz(int id_user)
        {
            bool permissoion = false;
            if (await DbConnection.Connecter3())
            {
                string sqlCmd = "SELECT max(quiz_partner) as quiz_partner \r\nFROM atooerp_app_permission_temp inner join\r\natooerp_user_module_group usg on usg.group = atooerp_app_permission_temp.group\r\nwhere user =" + id_user + ";";

                try
                {

                    MySqlCommand cmd = new MySqlCommand(sqlCmd, DbConnection.con);
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (Convert.ToInt32(reader["quiz_partner"]) == 1)
                        {
                            reader.Close();
                            return true;
                        }
                        reader.Close();

                    }

                }

                catch (Exception ex)
                {
                    return false;
                }

            }
            return false;

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
            using (UserDialogs.Instance.Loading("Refreshing, please wait..."))
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

                try
                {
                    if (!await checkPermissionQuiz(iduser))
                    {
                        uint idagent = (uint)Preferences.Get("idagent", Convert.ToUInt32(null));
                        Partners = await Partner.GetPartnerListByAgent(idagent);
                        Category_list = Partners.OrderBy(x => x.Category_Name).Select(x => x.Category_Name.ToLowerInvariant()).Distinct().ToList();
                        State_list = Partners.OrderBy(x => x.State).Select(x => x.State.ToLowerInvariant()).Distinct().ToList();
                    }
                    else
                    {
                        Partners = await Partner.GetPartnerList();
                        Category_list = Partners.OrderBy(x => x.Category_Name).Select(x => x.Category_Name.ToLowerInvariant()).Distinct().ToList();
                        State_list = Partners.OrderBy(x => x.State).Select(x => x.State.ToLowerInvariant()).Distinct().ToList();
                    }
                }
                catch (Exception ex)
                {
                    TestLoad = true;
                    ActPopup = false;
                }

                ActPopup = false;
                IsPullToRefreshEnabled = true;
                BtnFiltred = true;
                HomeEnabled = true;
            }
        }

        public async Task RefreshOnApp()
        {
            using (UserDialogs.Instance.Loading("Loading, please wait..."))
            {

                BtnFiltred = false;
                //IsPullToRefreshEnabled = false;
                //ActPopup = true;

                int CrmGroupe = 0;
                int iduser = Preferences.Get("iduser", 0);
                var UMG = await User_Module_Groupe_Services.GetGroupeCRM(iduser);

                if (UMG != null)
                {
                    CrmGroupe = UMG.IdGroup;
                }
                await Task.Delay(500);

                try
                {
                    if (!await checkPermissionQuiz(iduser))
                    {
                        uint idagent = (uint)Preferences.Get("idagent", Convert.ToUInt32(null));
                        Partners = await Partner.GetPartnerListByAgent(idagent);
                        Category_list = Partners.OrderBy(x => x.Category_Name).Select(x => x.Category_Name.ToLowerInvariant()).Distinct().ToList();
                        State_list = Partners.OrderBy(x => x.State).Select(x => x.State.ToLowerInvariant()).Distinct().ToList();
                    }
                    else
                    {
                        Partners = await Partner.GetPartnerList();
                        Category_list = Partners.OrderBy(x => x.Category_Name).Select(x => x.Category_Name.ToLowerInvariant()).Distinct().ToList();
                        State_list = Partners.OrderBy(x => x.State).Select(x => x.State.ToLowerInvariant()).Distinct().ToList();
                    }
                }
                catch (Exception ex)
                {
                    TestLoad = true;
                }
                finally
                {
                    ActPopup = false;
                    IsPullToRefreshEnabled = true;
                    BtnFiltred = true;
                    HomeEnabled = true;
                }
            }
        }


        public async Task RefreshForPartnerForm()
        {
            using (UserDialogs.Instance.Loading("Refreshing, please wait..."))
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

                try
                {

                    if (!await checkPermissionQuiz(iduser))
                    {
                        uint idagent = (uint)Preferences.Get("idagent", Convert.ToUInt32(null));
                        Partners = await Partner.GetPartnerListByAgent(idagent);
                        Category_list = Partners.OrderBy(x => x.Category_Name).Select(x => x.Category_Name.ToLowerInvariant()).Distinct().ToList();
                        State_list = Partners.OrderBy(x => x.State).Select(x => x.State.ToLowerInvariant()).Distinct().ToList();
                    }
                    else
                    {
                        Partners = await Partner.GetPartnerList();
                        Category_list = Partners.OrderBy(x => x.Category_Name).Select(x => x.Category_Name.ToLowerInvariant()).Distinct().ToList();
                        State_list = Partners.OrderBy(x => x.State).Select(x => x.State.ToLowerInvariant()).Distinct().ToList();
                    }

                }
                catch (Exception ex)
                {
                    TestLoad = true;
                    ActPopup = false;
                }

                ActPopup = false;
                IsPullToRefreshEnabled = true;
                BtnFiltred = true;
                HomeEnabled = true;
            }
        }
        public async Task RefreshOnAppForPartnerForm()
        {
            using (UserDialogs.Instance.Loading("Loading, please wait..."))
            {
                BtnFiltred = false;
                //IsPullToRefreshEnabled = false;
                //ActPopup = true;

                int CrmGroupe = 0;
                int iduser = Preferences.Get("iduser", 0);
                var UMG = await User_Module_Groupe_Services.GetGroupeCRM(iduser);

                if (UMG != null)
                {
                    CrmGroupe = UMG.IdGroup;
                }
                await Task.Delay(500);

                try
                {


                    if (!await checkPermissionQuiz(iduser))
                    {
                        uint idagent = (uint)Preferences.Get("idagent", Convert.ToUInt32(null));
                        Partners = await Partner.GetPartnerListByAgent(idagent);
                        Category_list = Partners.OrderBy(x => x.Category_Name).Select(x => x.Category_Name.ToLowerInvariant()).Distinct().ToList();
                        State_list = Partners.OrderBy(x => x.State).Select(x => x.State.ToLowerInvariant()).Distinct().ToList();
                    }
                    else
                    {
                        Partners = await Partner.GetPartnerList();
                        Category_list = Partners.OrderBy(x => x.Category_Name).Select(x => x.Category_Name.ToLowerInvariant()).Distinct().ToList();
                        State_list = Partners.OrderBy(x => x.State).Select(x => x.State.ToLowerInvariant()).Distinct().ToList();
                    }

                }
                catch (Exception ex)
                {
                    TestLoad = true;
                }
                finally
                {
                    ActPopup = false;
                    IsPullToRefreshEnabled = true;
                    BtnFiltred = true;
                    HomeEnabled = true;
                }
            }
        }
        public async Task ChangeConnexionState()
        {
            bool test = DbConnection.Connecter();
            DbConnection.Deconnecter();

            if (!test)
            {
                StateColor = Colors.Red;
                StateString = "Déconnecté";


            }
            else
            {
                StateColor = Colors.GreenYellow;
                StateString = "Connecté";

            }

            await Task.Delay(2000);





        }
    }
}