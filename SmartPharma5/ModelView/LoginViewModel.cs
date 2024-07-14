
/* Modification non fusionnée à partir du projet 'SmartPharma5 (net7.0-ios)'
Avant :
using SmartPharma5.Models;
using SmartPharma5.Services;
//using SmartPharma5.View;
Après :
//using SmartPharma5.View;
using MvvmHelpers;
using SmartPharma5.View;
*/
//using MvvmHelpers.Commands;
//using Xamarin.Essentials;

/* Modification non fusionnée à partir du projet 'SmartPharma5 (net7.0-ios)'
Avant :
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
Après :
using Xamarin.Forms;


using SmartPharma5.Model;
using SmartPharma5.Models;
using SmartPharma5.Services;
*/
//using MvvmHelpers.Commands;

/* Modification non fusionnée à partir du projet 'SmartPharma5 (net7.0-ios)'
Avant :
using SmartPharma5.View;
using System;
using System.Collections.Generic;

/* Modification non fusionnée à partir du projet 'SmartPharma5 (net7.0-ios)'
Après :
using SmartPharma5.Services;
using SmartPharma5.View;
using SmartPharma5.View;
using System;
using System.Collections.Generic;
/* Modification non fusionnée à partir du projet 'SmartPharma5 (net7.0-ios)'
*/
/* Modification non fusionnée à partir du projet 'SmartPharma5 (net7.0-ios)'
Avant :
using SmartPharma5.View;
using MvvmHelpers.Commands;
Après :
using SmartPharma5.Text;
using System.Threading.Tasks;
*/
using Acr.UserDialogs;
using MvvmHelpers;
using MvvmHelpers.Commands;
using SmartPharma5.Model;
using SmartPharma5.Models;
using
/* Modification non fusionnée à partir du projet 'SmartPharma5 (net7.0-ios)'
Avant :
using SmartPharma5.Services;
using SmartPharma5.View;
using Command = MvvmHelpers.Commands.Command;
Après :
using Command = MvvmHelpers.Commands.Command;
*/
SmartPharma5.Services;
using SmartPharma5.View;
using Command = MvvmHelpers.Commands.Command;
//using SmartPharma5.View;
//using SmartPharma53.View;

//using Acr.UserDialogs;

namespace SmartPharma5.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        public User User { get; set; }
        private Server server;
        public Server Server { get => server; set => SetProperty(ref server, value); }
        public AsyncCommand LoginCommand { get; }
        public Command Tryagain { get; }

        private bool actpopup = false;
        private bool successpopup = false;
        private bool fieldpopup = false;
        private string message;
        public string Message { get => message; set => SetProperty(ref message, value); }
        public bool SuccessPopup { get => successpopup; set => SetProperty(ref successpopup, value); }
        public bool FieldPopup { get => fieldpopup; set => SetProperty(ref fieldpopup, value); }
        public bool ActPopup { get => actpopup; set => SetProperty(ref actpopup, value); }

        public LoginViewModel()
        {
            LoginCommand = new AsyncCommand(Login);
            Tryagain = new Command(() => FieldPopup = false);
            User = new User();
            Server = new Server(Preferences.Get("name", "root"), Preferences.Get("password", "1261986"), Preferences.Get("address", "192.168.1.100"), Preferences.Get("database", "atooerp"), Preferences.Get("port", 3306));
        }

        private async Task Login()

        /* Modification non fusionnée à partir du projet 'SmartPharma5 (net7.0-ios)'
        Avant :
                {

                    ActPopup = true;
        Après :
                {

                    ActPopup = true;
        */
        {

            ActPopup = true;
            await Task.Delay(1000);

            var Connectivity = DbConnection.CheckConnectivity();

            if (await Server.ServerConnectionIsTrue())
            {
                Preferences.Set("address", Server.Address);
                Preferences.Set("name", Server.Name);
                Preferences.Set("password", Server.Password);
                Preferences.Set("database", Server.Data_base);
                Preferences.Set("port", Server.Port);

                DbConnection.Update();


                if (Connectivity)
                {
                    var DbConnectivity = DbConnection.ConnectionIsTrue();
                    if (DbConnectivity)
                    {
                        var LoginSuccess = User.LoginTrue();
                        if (LoginSuccess)

                        /* Modification non fusionnée à partir du projet 'SmartPharma5 (net7.0-ios)'
                        Avant :
                                                {


                                                    uint IdAgent = (uint)Preferences.Get("idagent", Convert.ToUInt32(null));
                        Après :
                                                {


                                                    uint IdAgent = (uint)Preferences.Get("idagent", Convert.ToUInt32(null));
                        */
                        {


                            uint IdAgent = (uint)Preferences.Get("idagent", Convert.ToUInt32(null));

                            ActPopup = false;
                            await Task.Delay(1000);
                            SuccessPopup = true;
                            await Task.Delay(2000);
                            //await App.Current.MainPage.DisplayAlert("Success", "Login success! Redirecting...", "OK");
                            var CrmGroupe = Task.Run(async () => await UserCheckModule()).Result;
                            Preferences.Set("UserName", User.Login) ;
                            Preferences.Set("Password", User.Password);
                            switch (CrmGroupe)
                            {

                                case 27:
                                    //  UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
                                    //  Task.Delay(1000).GetAwaiter();

                                    await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new HomeView()));
                                    //   UserDialogs.Instance.HideLoading();

                                    break;
                                case 28:
                                    UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
                                    Task.Delay(1000).GetAwaiter();
                                    await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new HomeView()));
                                    //await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new HomeView()));
                                    // await Shell.Current.GoToAsync("///HomeView");


                                    UserDialogs.Instance.HideLoading();

                                    break;
                                case 32:
                                    //  UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
                                    // Task.Delay(1000).GetAwaiter();
                                    await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new HomeView()));
                                    //  UserDialogs.Instance.HideLoading();

                                    break;
                                case 37:
                                    // UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
                                    // Task.Delay(1000).GetAwaiter();
                                    await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new HomeView()));
                                    // UserDialogs.Instance.HideLoading();

                                    break;
                                default:
                                    //UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
                                    //  Task.Delay(1000).GetAwaiter();
                                    await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new HomeView()));
                                    // UserDialogs.Instance.HideLoading();
                                    break;
                            }
                            SuccessPopup = false;

                        }
                        else
                        {
                            //await App.Current.MainPage.DisplayAlert("Warning", "Please enter a correct username and password", "OK");
                            Message = "Please enter a correct username and password";
                            ActPopup = false;
                            await Task.Delay(1000);
                            FieldPopup = true;
                        }
                    }
                    else
                    {
                        //await App.Current.MainPage.DisplayAlert("Warning", "There is a problem in connecting to the server. \n Please contact our support for further assistance.", "OK");
                        Message = "There is a problem in connecting to the server. \n Please contact our support for further assistance.";
                        ActPopup = false;
                        await Task.Delay(1000);
                        FieldPopup = true;
                    }
                }
                else
                {
                    Message = "No network connectivity.";
                    //ActPopup = false;
                    //await Task.Delay(1000);
                    //FieldPopup = true;
                }
            }
            else
            {
                Message = "Please enter a correct Server Parameter";
                ActPopup = false;
                await Task.Delay(1000);
                FieldPopup = true;
            }

        }
        private async Task<int> UserCheckModule()
        {
            int CrmGroupe = 0;
            int iduser = Preferences.Get("iduser", 0);
            var UMG = await User_Module_Groupe_Services.GetGroupeCRM(iduser);

            if (UMG != null)
            {
                CrmGroupe = UMG.IdGroup;
            }
            return CrmGroupe;

        }
    }
}
