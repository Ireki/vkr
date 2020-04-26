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
using System.Net;
using System.Net.Mail;


namespace vkr.View
{
    /// <summary>
    /// Логика взаимодействия для ForgetPassword.xaml
    /// </summary>
    public partial class ForgetPassword : Window
    {
        DocumentsContext db;
        public ForgetPassword()
        {
            InitializeComponent();
            db = new DocumentsContext();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Validate()){
                SendEmailAsync();
                MessageBox.Show("Пароль отправлен вам на почту.");
                BackAutorization();
            }
        }

        private void SendEmailAsync()
        {
            MailAddress from = new MailAddress("adm1nntest@yandex.ru", "Vtik");
            MailAddress to = new MailAddress(GetEmail());
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Восстановление пароля";
            m.Body = "Ваш пароль: " + GetPassword();
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 25);
            smtp.Credentials = new NetworkCredential("adm1nntest@yandex.ru", "freedom8");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }

        private string GetEmail(){
            return db
                .Users
                .FirstOrDefault(x => x.Login.Trim() == textBlockLogin.Text.Trim() || x.Email.Trim() == textBlockLogin.Text.Trim()).Email; 
        }

        private string GetPassword()
        {
            return db
                .Users
                .FirstOrDefault(x => x.Login.Trim() == textBlockLogin.Text.Trim() || x.Email.Trim() == textBlockLogin.Text.Trim()).Password;
        }

        private bool Validate(){
            if (textBlockLogin.Text.Trim() == "")
            {
                textBoxInfo.Text = "Логин не заполнен";
                return false;
            }
            if (!isLogin())
            {
                textBoxInfo.Text = "Не верный логин либо email";
                return false;
            }
            return true;
        }

        private bool isLogin()
        {
            return db
                .Users
                .Any(x => x.Login.Trim() == textBlockLogin.Text.Trim() || x.Email.Trim() == textBlockLogin.Text.Trim());
        }

        private void ButtonBcak_Click(object sender, RoutedEventArgs e)
        {
            BackAutorization();
        }

        private void BackAutorization()
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }
     }
}
