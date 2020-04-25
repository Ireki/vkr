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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace vkr.View
{
    /// <summary>
    /// Логика взаимодействия для AddPractical.xaml
    /// </summary>
    public partial class AddOrChangePractical : Window
    {
        
        private Model.Practical practical { get; set; }
        private DocumentsContext db { get; set; }
        public AddOrChangePractical(DocumentsContext db)
        {
            InitializeComponent();
            this.db = db;
            practical = new Model.Practical();
        }

        public AddOrChangePractical(Model.Practical practical, DocumentsContext db)
        {

            InitializeComponent();
            this.practical = practical;
            this.db = db;
            btnModify.Content = "Изменить";
            textBlockGroup.Text = practical.Group;
            textBlockName.Text = practical.Name;
            textBlockSurname.Text = practical.Surname;
            textBlockPatronymic.Text = practical.Patronymic;
            textBlockPracticeBase.Text = practical.PracticeBase;
            textBlockHumanSettlement.Text = practical.HumanSettlement;
            textBlockStartOfPractice.Text = practical.StartOfPractice.ToString();
            textBlockEndOfPractice.Text = practical.EndOfPractice.ToString();
            textBlockFormOfPractice.Text = practical.FormOfPractice;
            textBlockFormOfPractice.Text = practical.Payment;
            textBlockDirector.Text = practical.Director;
        }

        bool Validate()
        {
            if(textBlockGroup.Text.Trim() == "")
            {
                MessageBox.Show("Поле группы не должно быть пустым");
                return false;
            }
            else if (textBlockName.Text.Trim() == "")
            {
                MessageBox.Show("Поле имя не должно быть пустым");
                return false;
            }
            else if (textBlockSurname.Text.Trim() == "")
            {
                MessageBox.Show("Поле фамилия не должно быть пустым");
                return false;
            }
            else if (textBlockPatronymic.Text.Trim() == "")
            {
                MessageBox.Show("Поле отчетство не должно быть пустым");
                return false;
            }
            else if (textBlockPracticeBase.Text.Trim() == "") {
                MessageBox.Show("Поле база практики не должно быть пустым");
                return false;
            }
            else if (textBlockHumanSettlement.Text.Trim() == "") {
                MessageBox.Show("Поле населенный пункт не должно быть пустым");
                return false;
            }
            else if (textBlockStartOfPractice.Text.Trim() == "") {
                MessageBox.Show("Поле начало практики не должно быть пустым");
                return false;
            }
            else if (textBlockEndOfPractice.Text.Trim() == "") {
                MessageBox.Show("Поле конец практики не должно быть пустым");
                return false;
            }
            else if (textBlockDirector.Text.Trim() == "") {
                MessageBox.Show("Поле директора не должно быть пустым");
                return false;
            }
            else if (textBlockFormOfPractice.Text.Trim() == "") {
                MessageBox.Show("Поле формы практики не должно быть пустым");
                return false;
            }
            else if (textBlockDirector.Text.Trim() == "") {
                MessageBox.Show("Поле директора не должно быть пустым");
                return false;
            }
            
            return true;
        }

        void InitPractical()
        {
            practical.Group = textBlockGroup.Text;
            practical.Name = textBlockName.Text;
            practical.Surname = textBlockSurname.Text;
            practical.Patronymic = textBlockPatronymic.Text;
            practical.PracticeBase = textBlockPracticeBase.Text;
            practical.HumanSettlement = textBlockHumanSettlement.Text;
            practical.StartOfPractice = Convert.ToDateTime(textBlockStartOfPractice.Text);
            practical.EndOfPractice = Convert.ToDateTime(textBlockEndOfPractice.Text);
            practical.FormOfPractice = textBlockFormOfPractice.Text;
            practical.Payment = textBlockFormOfPractice.Text;
            practical.Director = textBlockDirector.Text;
        }


        private void AddPractical_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
                if (practical.Id != 0)
                {
                    InitPractical();
                    db.SaveChanges();
                    this.Close();
                }
                else
                {
                    InitPractical();
                    db.Practical.Add(practical);
                    db.SaveChanges();
                    this.Close();
                }
        }
    }
}
