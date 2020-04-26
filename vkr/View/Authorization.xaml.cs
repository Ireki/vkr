using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using vkr.Model;

namespace vkr.View
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Validate()){
                if (IsPassword()){
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();
                }
                else textBlockInfo.Text = "Не правильный логин либо пароль.";

            }
        }

        private bool IsPassword(){
            return (new DocumentsContext())
                .Users
                .Any(x => x.Password.Trim() == textBoxPassword.Password.Trim() &&
                (x.Login.Trim() == textBoxLogin.Text.Trim() || x.Email.Trim() == textBoxLogin.Text.Trim()));

        }

        private bool Validate(){
            if(textBoxLogin.Text == ""){
                textBlockInfo.Text = "Не введен логин.";
                return false;
            }
            if (textBoxPassword.Password == ""){
                textBlockInfo.Text = "Не введен пароль.";
                return false;
            }
            return true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ForgetPassword forgetPassword = new ForgetPassword();
            forgetPassword.Show();
            this.Close();
            
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (textBoxLogin.Text.Trim() == "" ||
                !(new DocumentsContext())
                .Users
                .Any(x => x.Login.Trim() == textBoxLogin.Text.Trim() || x.Email.Trim() == textBoxLogin.Text.Trim())) 
            {
                textBlockInfo.Text = "Не верный логин";
            }
            else
            {
                ChangePassword changePas = new ChangePassword(textBoxLogin.Text.Trim());
                changePas.Show();
                this.Close();
            }
        }
    }
}
