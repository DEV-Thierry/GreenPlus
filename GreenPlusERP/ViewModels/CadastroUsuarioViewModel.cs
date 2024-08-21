using GreenPlusERP.Models;
using GreenPlusERP.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GreenPlusERP.ViewModels
{
    public class CadastroUsuarioViewModel : viewModelBase
    {
        //fields
        private userModel _user;
        private IUserRepository userRepository;

        public userModel UserModel
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(nameof(UserModel)); }
        }

        //commands
        public ICommand CadastrarUsuario {  get; set; }
        public ICommand ConsulteCommand {  get; set; }
        

        //Constructor
        public CadastroUsuarioViewModel()
        {
            userRepository = new userRepository();
            UserModel = new userModel();
            CadastrarUsuario = new viewModelCommand(ExecutarCadastro, CanExecuteCadastro);
            ConsulteCommand = new viewModelCommand(ExecutarConsulta, CanExecuteConsulta);
        }

        private bool CanExecuteConsulta(object obj)
        {
            bool CanConsult;
            if (string.IsNullOrWhiteSpace(UserModel.Name))
            {
                CanConsult = false;
            }else
            {
                CanConsult = true;
            }

            return CanConsult;
        }

        private void ExecutarConsulta(object obj)
        {
            UserModel = userRepository.GetByName(UserModel.Name);
        }

        //methods

        private bool CanExecuteCadastro(object obj)
        {
            bool isValidData;

            if(string.IsNullOrWhiteSpace(_user.Name) ||
                string.IsNullOrWhiteSpace(_user.Cargo) ||
                string.IsNullOrWhiteSpace(_user.Grupo) ||
                string.IsNullOrWhiteSpace(_user.Email) ||
                string.IsNullOrWhiteSpace(_user.UserName) ||
                string.IsNullOrWhiteSpace(_user.Password))
            {
                isValidData = false;
            }
            else
                isValidData = true;

            return isValidData;
        }

        private void ExecutarCadastro(object obj)
        {

            if (userRepository.AuthenticateUser(new System.Net.NetworkCredential(UserModel.UserName, UserModel.Password)))
            {
                userRepository.Edit(UserModel);
                MessageBox.Show("Dados Atualizados Com Sucesso");
                UserModel = new userModel();
            }
            else
            {
                try
                {
                    userRepository.Add(_user);
                    UserModel = new userModel();
                    MessageBox.Show("Usuário Cadastrado Com Sucesso");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    UserModel = new userModel();
                }
            }
        }
    }
}
