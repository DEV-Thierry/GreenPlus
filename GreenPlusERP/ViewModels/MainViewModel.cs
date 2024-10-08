using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GreenPlusERP.Repositorios;
using GreenPlusERP.Models;
using System.Windows.Input;
using System.Windows.Media.Animation;
using GreenPlusERP.Views;
using System.Windows;

namespace GreenPlusERP.ViewModels
{
    public class MainViewModel : viewModelBase
    {
        //Fields
        private UserAccountModel _currentUserAccount;
        private IUserRepository userRepository;
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(nameof(CurrentView)); }
        }

        public UserAccountModel CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand ProdutoCommand { get; set; }

        public ICommand CadastroUsuarioCommand { get; set; }
        public ICommand CadastroFornecedorCommand { get; set; }
        public ICommand PlantioCommand { get; set; }

        //construtor
        public MainViewModel()
        {
            userRepository = new userRepository();
            CurrentUserAccount = new UserAccountModel();
            LoadCurrentUserData();
            CurrentView = new HomeViewModel();

            HomeCommand = new viewModelCommand(Home);
            ProdutoCommand = new viewModelCommand(Product);
            CadastroUsuarioCommand = new viewModelCommand(CadUser);
            CadastroFornecedorCommand = new viewModelCommand(CadFornec);
            PlantioCommand = new viewModelCommand(plantio);
        }




        //metodos
        private void LoadCurrentUserData()
        {
                var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name); 
            
            if (user != null)
            {
                CurrentUserAccount.Username = user.UserName;
                CurrentUserAccount.DisplayName = $"Seja Bem Vindo de volta  Sr.{user.Name.ToUpper()}";
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                CurrentUserAccount.DisplayName = "Invalid user, not logged in";
                //Hide child views.
            }
        }

        private void Home(object obj) => CurrentView = new HomeViewModel();
        private void Product(object obj) => CurrentView = new ProductViewModel();

        private void CadUser(object obj) => CurrentView = new CadastroUsuarioViewModel();
        private void CadFornec(object obj) => CurrentView = new CadastroFornecedorViewModel();
        private void plantio(object obj) => CurrentView = new PlantioViewModel();

    }
}
