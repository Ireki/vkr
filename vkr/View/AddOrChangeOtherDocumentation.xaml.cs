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
    public partial class AddOrChangeOtherDocumentation : Window
    {
        
        private Model.OtherDocumentation otherDoc { get; set; }
        private DocumentsContext db { get; set; }
        public AddOrChangeOtherDocumentation(DocumentsContext db)
        {
            InitializeComponent();
            this.db = db;
            otherDoc = new Model.OtherDocumentation();
            btnModify.Content = "Добавить";

        }

        public AddOrChangeOtherDocumentation(Model.OtherDocumentation otherDoc, DocumentsContext db)
        {

            InitializeComponent();
            this.otherDoc = otherDoc;
            this.db = db;
            btnModify.Content = "Изменить";
            textBlockLocation.Text = otherDoc.Location;
            textBlockShelfLife.Text = otherDoc.ShelfLife.ToString();
            textBlockTypeDoc.Text = otherDoc.TypeDocumentation;
            textBlockDateDeposit.Text = otherDoc.DateDeposit.ToString();

        }

        bool Validate()
        {
            if(textBlockDateDeposit.Text.Trim() == "")
            {
                MessageBox.Show("Поле группы не должно быть пустым");
                return false;
            }

            else if (textBlockShelfLife.Text.Trim() == "") {
                MessageBox.Show("Поле база практики не должно быть пустым");
                return false;
            }
            else if (textBlockTypeDoc.Text.Trim() == "") {
                MessageBox.Show("Поле населенный пункт не должно быть пустым");
                return false;
            }
            return true;
        }

        void InitPractical()
        {
            otherDoc.Location = textBlockLocation.Text;
            otherDoc.ShelfLife = int.Parse(textBlockShelfLife.Text);
            otherDoc.TypeDocumentation = textBlockTypeDoc.Text;
            otherDoc.DateDeposit = Convert.ToDateTime(textBlockDateDeposit.Text);

        }


        private void AddOtherDoc_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
                if (otherDoc.Id != 0)
                {
                    InitPractical();
                    db.SaveChanges();
                    this.Close();
                }
                else
                {
                    InitPractical();
                    db.OtherDocumentation.Add(otherDoc);
                    db.SaveChanges();
                    this.Close();
                }
        }
    }
}
