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
    /// Логика взаимодействия для RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : Window
    {
        DocumentsContext db;
        public RegistrationView()
        {
            InitializeComponent();
            db = new DocumentsContext();
        }
       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                Registration();
                MessageBox.Show("Регистрация прошла успешно");
                BackAutorization();
            }
        }

        private void BackAutorization()
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }

        private void Registration()
        {
            db.Add(new User (){ 
                Surname = textBoxSurname.Text,
                Login = textBoxLogin.Text,
                Email = textBoxEmail.Text,
                Password = textBoxPassword.Password,
                Name = textBoxName.Text, 
                Role = 1
            });
            db.SaveChanges();

        }

        private bool isLogin()
        {
            return db
                .Users
                .Any(x => x.Login.Trim() == textBoxLogin.Text.Trim() || x.Email.Trim() == textBoxLogin.Text.Trim());
        }

        private bool Validate()
        {
            if (textBoxName.Text == "")
            {
                textBlockInfo.Text = "Не введен имя.";
                return false;
            }
            if (textBoxEmail.Text == "")
            {
                textBlockInfo.Text = "Не введена почта.";
                return false;
            }
            if (textBoxSurname.Text == "")
            {
                textBlockInfo.Text = "Не введена фамилия.";
                return false;
            }
            if (textBoxLogin.Text == "")
            {
                textBlockInfo.Text = "Не введен логин.";
                return false;
            }
            if (textBoxPassword.Password == "")
            {
                textBlockInfo.Text = "Не введен пароль.";
                return false;
            }
            if (textBoxPassword2.Password == "")
            {
                textBlockInfo.Text = "Не введен повтор пароль.";
                return false;
            }

            if (textBoxPassword2.Password != textBoxPassword.Password)
            {
                textBlockInfo.Text = "Пароли не совпадают.";
                return false;
            }

            if (isLogin())
            {
                textBlockInfo.Text = "Такой логин уже существует.";
                return false;
            }
            return true;
        }

        private void ButtonBcak_Click(object sender, RoutedEventArgs e)
        {
            BackAutorization();
        }
    }
}
