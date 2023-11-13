//using Acr.UserDialogs;
using Acr.UserDialogs;
using MvvmHelpers;
using MvvmHelpers.Commands;
using SmartPharma5.Model;

namespace SmartPharma5.ViewModel
{
    class ValidateUpdateProfileMV : BaseViewModel
    {

        private List<ValidateProfileChanges> listOfRequests;
        public List<ValidateProfileChanges> ListOfRequests { get => listOfRequests; set => SetProperty(ref listOfRequests, value); }

        

        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand HistoriqueCommand { get; set; }

        public AsyncCommand CurrentCommand { get; set; }

        private bool btnCurrentNotif;
        public bool BtnCurrentNotif { get => btnCurrentNotif; set => SetProperty(ref btnCurrentNotif, value); }


        private bool btnHitstoricNotif;
        public bool BtnHitstoricNotif { get => btnHitstoricNotif; set => SetProperty(ref btnHitstoricNotif, value); }


        public ValidateUpdateProfileMV()
        {
            try
            {
                RefreshCommand = new AsyncCommand(refresh);
                BtnHitstoricNotif = true;
                btnCurrentNotif = false;

                HistoriqueCommand = new AsyncCommand(history);
                CurrentCommand = new AsyncCommand(current);

                /* Modification non fusionnée à partir du projet 'SmartPharma5 (net7.0-ios)'
                Avant :
                                ListOfRequests = new List<ValidateProfileChanges>(ValidateProfileChanges.GetAllChangesProfile().Result);

                            }catch(Exception ex)
                Après :
                                ListOfRequests = new List<ValidateProfileChanges>(ValidateProfileChanges.GetAllChangesProfile().Result);

                            }
                            catch(Exception ex)
                */
                ListOfRequests = new List<ValidateProfileChanges>(ValidateProfileChanges.GetAllChangesProfile().Result);

            }
            catch (Exception ex)
            {

            }
        }
        public async Task refresh()
        {
            ListOfRequests = new List<ValidateProfileChanges>();
            try
            {
                ListOfRequests = new List<ValidateProfileChanges>(ValidateProfileChanges.GetAllChangesProfile().Result);
                IsBusy = false;
            }
            catch(Exception ex)
            {

            }
            IsBusy = false;
        }

        public async Task current()
        {
            UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
             await Task.Delay(500);
            BtnCurrentNotif = false;
            BtnHitstoricNotif = true;
            ListOfRequests = ValidateProfileChanges.GetAllChangesProfile().Result;
             UserDialogs.Instance.HideLoading();
        }
        public async Task history()
        {
            UserDialogs.Instance.ShowLoading("Loading Pleae wait ...");
            await Task.Delay(500);
            BtnCurrentNotif = true;
            BtnHitstoricNotif = false;
            ListOfRequests = ValidateProfileChanges.GetAllChangesHistoryProfile().Result;
            UserDialogs.Instance.HideLoading();

        }

    }
}
