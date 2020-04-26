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
using System.Windows.Navigation;
using System.Windows.Shapes;
using vkr.Model;

namespace vkr.View
{
    /// <summary>
    /// Логика взаимодействия для ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        private string login;
        DocumentsContext db;
        public ChangePassword(string login)
        {
            InitializeComponent();
            this.login = login;
            db = new DocumentsContext();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                if (IsPassword()){
                    db.Users.FirstOrDefault(x => x.Login.Trim() == login || x.Email.Trim() == login).Password = textBoxNewPassword.Text.Trim();
                    db.SaveChanges();
                    MessageBox.Show("Пароль был изменен");
                    BackAutorizthion();
                }
                else textBoxInfo.Text = "Старый пароль неверный";
            }
        }

        private bool IsPassword()
        {
            return db
                .Users
                .Any(x => x.Password.Trim() == textBoxOldPassword.Text.Trim() &&
                (x.Login.Trim() == login || x.Email.Trim() == login));
        }

        private bool Validate()
        {
            if(textBoxNewPassword.Text.Trim() == "")
            {
                textBoxInfo.Text = "Старый пароль не заполнен";
                return false;
            }
            if (textBoxOldPassword.Text.Trim() == "")
            {
                textBoxInfo.Text = "Новый пароль не заполнен";
                return false;
            }
            if (textBoxAgainPassword.Text.Trim() == "")
            {
                textBoxInfo.Text = "Повтор пароля не заполнен";
                return false;
            }
            if (textBoxAgainPassword.Text.Trim() != textBoxNewPassword.Text.Trim())
            {
                textBoxInfo.Text = "Пароли не совпадают";
                return false;
            }
            return true;
        }

        private void ButtonBcak_Click(object sender, RoutedEventArgs e) {
            BackAutorizthion();
        }
        private void BackAutorizthion()
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }
    }
}
