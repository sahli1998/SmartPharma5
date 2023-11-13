

/* Modification non fusionnée à partir du projet 'SmartPharma5 (net7.0-ios)'
Avant :
using SmartPharma5.Model;
using SmartPharma5.View;
using MvvmHelpers;
Après :
using MvvmHelpers;
using MvvmHelpers.Commands;
using SmartPharma5.Model;
*/
using MvvmHelpers;
using MvvmHelpers.Commands;

/* Modification non fusionnée à partir du projet 'SmartPharma5 (net7.0-ios)'
Avant :
using SmartPharma5.View;
using SmartPharma5.Model;
Après :
using SmartPharma5.Model;
using SmartPharma5.View;
*/
using SmartPharma5.Model;
using SmartPharma5.View;
//using Xamarin.Essentials;
//using static SmartPharma.Model.Payment;

namespace SmartPharma5.ViewModel
{
    public class PaymentViewModel : BaseViewModel
    {
        public AsyncCommand ExitCommand { get; set; }
        public AsyncCommand LogoutCommand { get; set; }

        public int iduser = Preferences.Get("iduser", 0);
        private Payment payment;
        public Payment Payment { get => payment; set => SetProperty(ref payment, value); }
        public Partner Partner { get; set; }
        private decimal total_unpaied;
        public decimal Total_unpaied { get => total_unpaied; set => SetProperty(ref total_unpaied, value); }
        private string type_color;
        public string Type_color { get => type_color; set => SetProperty(ref type_color, value); }
        #region Due Date
        private DateTime due_date = DateTime.Now;
        public DateTime Due_date { get => due_date; set => SetProperty(ref due_date, value); }
        private bool actpopup = false;
        public bool ActPopup { get => actpopup; set => SetProperty(ref actpopup, value); }
        #endregion
        #region Visibility
        private string successpopupmessage;
        public string SuccessPopupMessage { get => successpopupmessage; set => SetProperty(ref successpopupmessage, value); }

        private bool fieldpopup = false;
        public bool FieldPopup { get => fieldpopup; set => SetProperty(ref fieldpopup, value); }
        private bool successpopup = false;
        public bool SuccessPopup { get => successpopup; set => SetProperty(ref successpopup, value); }
        private bool verifPopup = false;
        public bool VerifPopup { get => verifPopup; set => SetProperty(ref verifPopup, value); }
        private bool isVisibleSaveButton = true;
        public bool IsVisibleSaveButton { get => isVisibleSaveButton; set => SetProperty(ref isVisibleSaveButton, value); }
        private bool isVisibleBankAccount = false;
        public bool IsVisibleBankAccount { get => isVisibleBankAccount; set => SetProperty(ref isVisibleBankAccount, value); }
        private bool isVisibledue_date = false;
        public bool IsVisibledue_date { get => isVisibledue_date; set => SetProperty(ref isVisibledue_date, value); }
        private bool isVisiblereference = false;
        public bool IsVisiblereference { get => isVisiblereference; set => SetProperty(ref isVisiblereference, value); }
        private bool isReadOnlyOnValidated = false;
        public bool IsReadOnlyOnValidated { get => isReadOnlyOnValidated; set => SetProperty(ref isReadOnlyOnValidated, value); }
        private bool isVisibleEffectedAmount = false;
        public bool IsVisibleEffectedAmount { get => isVisibleEffectedAmount; set => SetProperty(ref isVisibleEffectedAmount, value); }
        public bool IsReadOnlyPaymentPieceAmount { get => isReadOnlyPaymentPieceAmount; set => SetProperty(ref isReadOnlyPaymentPieceAmount, value); }
        private bool isReadOnlyPaymentPieceAmount = false;
        #endregion
        #region Affected_amount
        private decimal effected_amount;
        public decimal Effected_Amount { get => effected_amount; set => SetProperty(ref effected_amount, value); }
        #endregion
        #region PaymentMethod
        private ObservableRangeCollection<Payment.Payment_method> payment_methodList;
        public ObservableRangeCollection<Payment.Payment_method> Payment_methodList { get => payment_methodList; set => SetProperty(ref payment_methodList, value); }
        private Payment.Payment_method payment_methodlistselecteditem;
        public Payment.Payment_method Payment_methodListSelectedItem { get => payment_methodlistselecteditem; set => SetProperty(ref payment_methodlistselecteditem, value); }
        #endregion
        #region Cash Desk
        private ObservableRangeCollection<Payment.Cash_desk> cash_deskList;
        public ObservableRangeCollection<Payment.Cash_desk> Cash_deskList { get => cash_deskList; set => SetProperty(ref cash_deskList, value); }
        private Payment.Cash_desk cash_deskListselecteditem;
        public Payment.Cash_desk Cash_deskListselecteditem { get => cash_deskListselecteditem; set => SetProperty(ref cash_deskListselecteditem, value); }
        #endregion
        #region UnpaidList
        private ObservableRangeCollection<Payment.Piece> unpaiedList;
        public ObservableRangeCollection<Payment.Piece> UnpaiedList { get => unpaiedList; set => SetProperty(ref unpaiedList, value); }

        public AsyncCommand AmountChangeCommand { get; }

        public AsyncCommand serr1 { get; }

        private async Task AmountChange()
        {
            decimal restEffectedAmount = Effected_Amount;
            this.Payment.Payment_pieceList.Clear();
            foreach (Payment.Piece piece in UnpaiedList)
            {
                if (restEffectedAmount >= piece.rest_amount)
                {
                    if (piece == UnpaiedList.Last())
                    {
                        this.Payment.Payment_pieceList.Add(new Payment.Payment_piece(0, piece.Id, piece.piece_type, piece.piece_type_name, piece.code, 0, restEffectedAmount, piece.total_amount, piece.paied_amount, piece.rest_amount));
                        restEffectedAmount = restEffectedAmount - piece.rest_amount;
                    }
                    else
                    {
                        this.Payment.Payment_pieceList.Add(new Payment.Payment_piece(0, piece.Id, piece.piece_type, piece.piece_type_name, piece.code, 0, piece.rest_amount, piece.total_amount, piece.paied_amount, piece.rest_amount));
                        restEffectedAmount = restEffectedAmount - piece.rest_amount;
                    }
                }
                else
                {
                    this.Payment.Payment_pieceList.Add(new Payment.Payment_piece(0, piece.Id, piece.piece_type, piece.piece_type_name, piece.code, 0, restEffectedAmount, piece.total_amount, piece.paied_amount, piece.rest_amount));
                    break;
                }
            }
            Payment.SetAmount();
            SuccessPopupMessage = "Amount affected with success";
            SuccessPopup = true;
            await Task.Delay(1000);
            SuccessPopup = false;
        }
        public AsyncCommand<Payment.Piece> SelectedChangedCommand { get; }
        private async Task serr()
        {

        }
        private async Task SelectedChanged(Payment.Piece Piece)
        {
            if (PaymentTypeListSelectedItem.Id == 2 || PaymentTypeListSelectedItem.Id == 3)
            {
                foreach (Payment.Piece p in UnpaiedList)
                {
                    this.Payment.Payment_pieceList.Clear();
                    if (Piece.Id == p.Id)
                    {
                        p.Is_checked = !p.Is_checked;
                        foreach (Payment.Piece piece in UnpaiedList)
                            if (piece.Is_checked == true)
                            {
                                this.Payment.Payment_pieceList.Add(new Payment.Payment_piece(0, piece.Id, piece.piece_type, piece.piece_type_name, piece.code, 0, piece.rest_amount, piece.total_amount, piece.paied_amount, piece.rest_amount));
                            }
                        if (p.Is_checked)
                        {
                            SuccessPopupMessage = "Item has been added with success";
                            SuccessPopup = true;
                            await Task.Delay(1000);
                            SuccessPopup = false;
                        }
                        else
                        {
                            SuccessPopupMessage = "Item has been deleted with success";
                            FieldPopup = true;
                            await Task.Delay(1000);
                            FieldPopup = false;
                        }

                        break;
                    }
                }
                Payment.SetAmount();
            }
        }
        #endregion
        #region PaymentType
        private ObservableRangeCollection<Payment.Type> paymentTypeList;
        public ObservableRangeCollection<Payment.Type> PaymentTypeList { get => paymentTypeList; set => SetProperty(ref paymentTypeList, value); }
        private Payment.Type paymentTypeListSelectedItem;
        public Payment.Type PaymentTypeListSelectedItem { get => paymentTypeListSelectedItem; set => SetProperty(ref paymentTypeListSelectedItem, value); }
        #endregion
        #region Bank
        private ObservableRangeCollection<Payment.Bank> bankList;
        public ObservableRangeCollection<Payment.Bank> BankList { get => bankList; set => SetProperty(ref bankList, value); }
        private Payment.Bank bankListSelectedItem;
        public Payment.Bank BankListSelectedItem { get => bankListSelectedItem; set => SetProperty(ref bankListSelectedItem, value); }
        #endregion
        #region Constructeur
        public PaymentViewModel() { }
        public PaymentViewModel(Payment payment)
        {
            Payment = payment;
            Partner = Partner.GetCommercialPartnerByIdForPayment(payment.IdPartner).Result;
            Title = "Payment[ " + Partner.Name + " ]";
            UnpaiedList = new ObservableRangeCollection<Payment.Piece>();
            Payment_methodList = new ObservableRangeCollection<Payment.Payment_method>();
            Cash_deskList = new ObservableRangeCollection<Payment.Cash_desk>();
            PaymentTypeList = new ObservableRangeCollection<Payment.Type>();
            BankList = new ObservableRangeCollection<Payment.Bank>();
            Payment_methodChangedCommand = new MvvmHelpers.Commands.Command(Payment_methodChanged);
            SelectedChangedCommand = new AsyncCommand<Payment.Piece>(SelectedChanged);
            AmountChangeCommand = new AsyncCommand(AmountChange);
            Payment_typeChangedCommand = new AsyncCommand(Payment_typeChanged);
            SaveCommand = new AsyncCommand(Save);
            ExitCommand = new AsyncCommand(Exit);
            LogoutCommand = new AsyncCommand(Logout);
            serr1 = new AsyncCommand(serr);
            Payment_pieceAmountChangeCommand = new AsyncCommand<object>(Payment_pieceAmountChange);
            setSaveVisibility();
            Task.Run(() => LoadList());

        }
        #endregion
        #region Payment_piece Amount Changed
        public AsyncCommand<object> Payment_pieceAmountChangeCommand { get; set; }
        private Task Payment_pieceAmountChange(object arg)
        {
            Payment.Piece piece1 = new Payment.Piece();
            var payment_piece = (Payment.Payment_piece)arg;
            foreach (Payment.Piece piece in UnpaiedList)
            {
                if (piece.Id == payment_piece.piece)
                    piece1 = piece;
            }
            payment_piece.Piece_restAmount = piece1.rest_amount - payment_piece.amount;
            Payment.SetAmount();
            return Task.CompletedTask;
        }
        #endregion
        #region Payment_method_ChangedCommand
        public MvvmHelpers.Commands.Command Payment_methodChangedCommand { get; }
        private void Payment_methodChanged()
        {
            switch (Payment_methodListSelectedItem.Id)
            {
                case 1:
                    IsVisibleBankAccount = false;
                    IsVisibledue_date = false;
                    IsVisiblereference = false;
                    break;
                case 9:
                case 10:
                case 11:
                    IsVisibleBankAccount = false;
                    IsVisibledue_date = false;
                    IsVisiblereference = false;
                    break;
                case 7:
                    IsVisibleBankAccount = false;
                    IsVisiblereference = true;
                    IsVisibledue_date = false;
                    break;
                case 2:
                case 6:
                    IsVisibleBankAccount = true;
                    IsVisibledue_date = true;
                    IsVisiblereference = true;
                    break;
                case 3:
                case 4:
                case 5:
                    IsVisibleBankAccount = true;
                    IsVisiblereference = true;
                    IsVisibledue_date = false;
                    break;
                case 8:
                    IsVisibleBankAccount = true;
                    IsVisiblereference = false;
                    IsVisibledue_date = false;
                    break;
                default:
                    break;
            }
            Cash_deskListselecteditem = null;
            Cash_deskList.Clear();
            Cash_deskList.AddRange(Payment.getCash_deskListByUserAndPayment_methodAndPayment_type(iduser, Payment_methodListSelectedItem.Id));
            foreach (Payment.Cash_desk cd in Cash_deskList)
                if (cd.principal == true)
                {
                    Cash_deskListselecteditem = cd;
                    break;
                }
        }
        #endregion
        #region Payment_typeChangeCommand
        public AsyncCommand Payment_typeChangedCommand { get; }
        private Task Payment_typeChanged()
        {
            switch (PaymentTypeListSelectedItem.Id)
            {
                case 1:
                    IsVisibleEffectedAmount = true;
                    break;
                case 2:
                case 3:
                    IsVisibleEffectedAmount = false;
                    break;
                default:
                    break;
            }
            return Task.CompletedTask;
        }
        #endregion
        #region Save
        public AsyncCommand SaveCommand { get; }
        private async Task Save()
        {
            SuccessPopupMessage = "data verification...";
            VerifPopup = true;
            await Task.Delay(1000);
            VerifPopup = false;
            if (Payment.Payment_pieceList.Count != 0)
            {
                var Connectivity = DbConnection.CheckConnectivity();
                if (Connectivity)
                {
                    var DbConnectivity = DbConnection.ConnectionIsTrue();
                    if (DbConnectivity)
                    {
                        try
                        {
                            setInformation();
                            if (Payment.IdCash_desk > 0)
                            {
                                if ((Payment.IdPayment_method == 2 || Payment.IdPayment_method == 6 || Payment.IdPayment_method == 3 || Payment.IdPayment_method == 4 || Payment.IdPayment_method == 5) && (Payment.sale_bank > 0 && (Payment.reference != null)))
                                {
                                    if (Payment.CountReference() == 0)
                                    {
                                        SuccessPopupMessage = "Payment has been added...";
                                        Payment.Validate();
                                        //await App.Current.MainPage.DisplayAlert("Success", "Added", "Ok");
                                        //SavingPopup = false;
                                        //SuccessPopupMessage = "Your opportunty has been successfully sent!";
                                        //SuccessPopup = true;
                                        //await Task.Delay(1000);
                                        //SuccessPopup = false;
                                        VerifPopup = false;

                                        await App.Current.MainPage.DisplayAlert("Success", "Payment Saved", "Ok");
                                        await App.Current.MainPage.Navigation.PopToRootAsync();
                                        
                                        await App.Current.MainPage.Navigation.PushAsync(new NavigationPage( new  View.PaymentListView()));
                                    }
                                    else
                                    {
                                        VerifPopup = false;
                                        await App.Current.MainPage.DisplayAlert("Failed", "Try to set an other reference !", "Ok");
                                    }
                                }
                                else if (Payment.IdPayment_method == 1 || Payment.IdPayment_method == 7 || Payment.IdPayment_method == 8 || Payment.IdPayment_method == 9 || Payment.IdPayment_method == 10 || Payment.IdPayment_method == 11)
                                {
                                    Payment.Validate();
                                    //await App.Current.MainPage.DisplayAlert("Success", "Added", "Ok");
                                    //SavingPopup = false;
                                    //SuccessPopupMessage = "Your opportunty has been successfully sent!";
                                    //SuccessPopup = true;
                                    //await Task.Delay(1000);
                                    //SuccessPopup = false;
                                    VerifPopup = false;
                                    await App.Current.MainPage.Navigation.PopAsync();
                                    await App.Current.MainPage.DisplayAlert("Success", "Payment Saved", "Ok");
                                    //-------------------------------------------
                                    await App.Current.MainPage.Navigation.PushAsync(new NavigationPage(new View.PaymentListView()));
                                }
                                else
                                {
                                    VerifPopup = false;
                                    await App.Current.MainPage.DisplayAlert("Failed", "Set bank and reference !", "Ok");
                                }

                            }
                            else
                            {
                                VerifPopup = false;
                                await App.Current.MainPage.DisplayAlert("Failed", "Set Cash Desk", "Ok");
                            }
                        }
                        catch (Exception ex)
                        {
                            await App.Current.MainPage.DisplayAlert("Warning", ex.Message, "Ok");
                        }
                    }
                    else
                    {
                        VerifPopup = false;
                        //SavingPopup = false;
                        //App.Current.MainPage.DisplayAlert("Warning", "There is a problem in connecting to the server. \n Please contact our support for further assistance.", "Ok");
                        //Message = "There is a problem in connecting to the server. \n Please contact our support for further assistance.";
                        await App.Current.MainPage.DisplayAlert("Warning", "There is a problem in connecting to the server. \n Please contact our support for further assistance.", "Ok");
                        //FieldPopup = true;
                    }
                }
                else
                {
                    VerifPopup = false;
                    await App.Current.MainPage.DisplayAlert("Warning", "No network connectivity.", "Ok");
                    //Message = "No network connectivity.";
                    //SavingPopup = false;
                    //await Task.Delay(1000);
                    //FieldPopup = true;
                }
            }
            else
            {
                VerifPopup = false;
                await App.Current.MainPage.DisplayAlert("Warning", "Add Payment before validate", "Ok");
                //SavingPopup = false;
            }
        }

        private async Task Logout()
        {
            var r = await App.Current.MainPage.DisplayAlert("Warning", "Are you sure you want to lougout!", "Yes", "No");
            if (r)
            {
                Preferences.Set("idagent", Convert.ToUInt32(null));
                await App.Current.MainPage.Navigation.PushAsync(new LoginView());
            }
        }

        private async Task Exit()
        {
            var r = await App.Current.MainPage.DisplayAlert("Warning", "Are you sure you want to exit!", "Yes", "No");
            if (r)
            {
                await App.Current.MainPage.Navigation.PopToRootAsync();

            }
        }
        public void setSaveVisibility()
        {
            if (Payment.Id > 0)
            {
                IsVisibleSaveButton = false;
                IsReadOnlyOnValidated = true;
            }
        }
        #endregion
        public void setInformation()
        {
            try
            {
                Payment.IdPayment_method = payment_methodlistselecteditem.Id;
                Payment.IdPayment_type = (int)paymentTypeListSelectedItem.Id;
                if (bankListSelectedItem != null)
                    Payment.sale_bank = bankListSelectedItem.Id;
                Payment.IdCash_desk = Convert.ToUInt32(cash_deskListselecteditem.Id);
                if (payment_methodlistselecteditem.Id == 2 || payment_methodlistselecteditem.Id == 6)
                {
                    Payment.due_date = Due_date;
                }
                else
                    Payment.due_date = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async void LoadList()
        {
            ActPopup = true;
            UnpaiedList.Clear();
            if (!(Payment.Id > 0))
            {
                var C = Task.Run(() => Payment.GetUnpaiedPiece(Payment.IdPartner));
                UnpaiedList = new ObservableRangeCollection<Payment.Piece>(await C);
                Total_unpaied = UnpaiedList.Sum(p => p.rest_amount);
            }
            if (Payment.Id > 0)
            {
                Payment_methodList.Clear();
                var J = Task.Run(() => Payment.getPaymentMethod());
                Payment_methodList = new ObservableRangeCollection<Payment.Payment_method>(await J);
            }
            else
            {
                Payment_methodList.Clear();
                var J = Task.Run(() => Payment.getPaymentMethodByUser(iduser));
                Payment_methodList = new ObservableRangeCollection<Payment.Payment_method>(await J);
            }


            PaymentTypeList.Clear();
            var A = Task.Run(() => Payment.getTypeList());
            PaymentTypeList = new ObservableRangeCollection<Payment.Type>(await A);

            BankList.Clear();
            var D = Task.Run(() => Payment.getBankList());
            BankList = new ObservableRangeCollection<Payment.Bank>(await D);
            Cash_deskList.Clear();

            if (Payment.Id > 0)
            {
                if (Payment.IdPayment_type > 0)
                {
                    var Y = Task.Run(() => Payment.Type.GetTypeById(Payment.IdPayment_type));
                    PaymentTypeListSelectedItem = await Y;
                }
                if (Payment.IdPayment_method > 0)
                {
                    var X = Task.Run(() => Payment.Payment_method.GetPayment_MethodById(Payment.IdPayment_method));
                    Payment_methodListSelectedItem = await X;
                    switch (Payment.IdPayment_method)
                    {
                        case 1:
                            IsVisibleBankAccount = false;
                            IsVisibledue_date = false;
                            IsVisiblereference = false;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            IsVisibleBankAccount = false;
                            IsVisibledue_date = false;
                            IsVisiblereference = false;
                            break;
                        case 7:
                            IsVisibleBankAccount = false;
                            IsVisiblereference = true;
                            IsVisibledue_date = false;
                            break;
                        case 2:
                        case 6:
                            IsVisibleBankAccount = true;
                            IsVisibledue_date = true;
                            IsVisiblereference = true;
                            try { Due_date = Convert.ToDateTime(Payment.due_date); }
                            catch { }
                            break;
                        case 3:
                        case 4:
                        case 5:
                            IsVisibleBankAccount = true;
                            IsVisiblereference = true;
                            IsVisibledue_date = false;
                            break;
                        case 8:
                            IsVisibleBankAccount = true;
                            IsVisiblereference = false;
                            IsVisibledue_date = false;
                            break;
                        default:
                            break;
                    }
                }
                var E = Task.Run(() => Payment.getCash_deskList());
                Cash_deskList = new ObservableRangeCollection<Payment.Cash_desk>(await E);
                if (Payment.IdCash_desk > 0)
                {
                    var Z = Task.Run(() => Payment.Cash_desk.getCash_deskById((int)Payment.IdCash_desk));
                    Cash_deskListselecteditem = await Z;
                }
                if (Payment.sale_bank > 0)
                {
                    var W = Task.Run(() => Payment.Bank.getBankById(Payment.sale_bank));
                    BankListSelectedItem = await W;
                }
            }
            else
            {
                var Y = Task.Run(() => Payment.Type.GetTypeById(2));
                PaymentTypeListSelectedItem = await Y;
                if (Payment.IdPayment_method > 0)
                {
                    var X = Task.Run(() => Payment.Payment_method.GetPayment_MethodById(Payment.IdPayment_method));
                    Payment_methodListSelectedItem = await X;
                    var E = Task.Run(() => Payment.getCash_deskListByUserAndPayment_methodAndPayment_type(iduser, Payment.IdPayment_method));
                    Cash_deskList = new ObservableRangeCollection<Payment.Cash_desk>(await E);
                    foreach (Payment.Cash_desk cd in Cash_deskList)
                        if (cd.principal == true)
                        {
                            Cash_deskListselecteditem = cd;
                            break;
                        }
                    if (Cash_deskListselecteditem == null && Cash_deskList.Count > 0)
                        Cash_deskListselecteditem = Cash_deskList[0];
                    switch (Payment.IdPayment_method)
                    {
                        case 1:
                            IsVisibleBankAccount = false;
                            IsVisibledue_date = false;
                            IsVisiblereference = false;
                            break;
                        case 9:
                        case 10:
                        case 11:
                            IsVisibleBankAccount = false;
                            IsVisibledue_date = false;
                            IsVisiblereference = false;
                            break;
                        case 7:
                            IsVisibleBankAccount = false;
                            IsVisiblereference = true;
                            IsVisibledue_date = false;
                            break;
                        case 2:
                        case 6:
                            IsVisibleBankAccount = true;
                            IsVisibledue_date = true;
                            IsVisiblereference = true;
                            try { Due_date = Convert.ToDateTime(Payment.due_date); }
                            catch { }
                            break;
                        case 3:
                        case 4:
                        case 5:
                            IsVisibleBankAccount = true;
                            IsVisiblereference = true;
                            IsVisibledue_date = false;
                            break;
                        case 8:
                            IsVisibleBankAccount = true;
                            IsVisiblereference = false;
                            IsVisibledue_date = false;
                            break;
                        default:
                            break;
                    }
                }
            }
            ActPopup = false;
        }
    }
}