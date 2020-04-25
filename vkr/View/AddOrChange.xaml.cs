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
    /// Логика взаимодействия для AddVkr.xaml
    /// </summary>
    public partial class AddOrChange : Window
    {
        
        private Vkr vkr { get; set; }
        private Model.Theses theses { get; set; }
        DocumentsContext db;
        public AddOrChange(DocumentsContext db)
        {
            InitializeComponent();
            this.db = db;
            vkr = new Vkr();
        }

        public AddOrChange(Vkr vkr, DocumentsContext db)
        {
            InitializeComponent();
            this.vkr = vkr;
            this.db = db;
            textBlockGroup.Text = vkr.Group;
            textBlockProtocolNumber.Text = vkr.ProtocolNumber;
            textBlockSurname.Text = vkr.Surname;
            textBlockName.Text = vkr.Name;
            textBlockPatronymic.Text = vkr.Patronymic;
            textBlockTheme.Text = vkr.Theme;
            textBlockDirector.Text = vkr.Director;
            textBlockDate.Text = vkr.Date.ToString();
            textBlockLocation.Text = vkr.Location;
        }

        public AddOrChange(Model.Theses vkr, DocumentsContext db)
        {
            InitializeComponent();
            this.theses = theses;
            this.db = db;
            textBlockGroup.Text = theses.Group;
            textBlockProtocolNumber.Text = theses.ProtocolNumber;
            textBlockSurname.Text = theses.Surname;
            textBlockName.Text = theses.Name;
            textBlockPatronymic.Text = theses.Patronymic;
            textBlockTheme.Text = theses.Theme;
            textBlockDirector.Text = theses.Director;
            textBlockDate.Text = theses.Date.ToString();
            textBlockLocation.Text = theses.Location;
        }


        bool Validate()
        {
            if(textBlockGroup.Text.Trim() == "")
            {
                MessageBox.Show("Поле группы не должно быть пустым");
                return false;
            }
            else if (textBlockProtocolNumber.Text.Trim() == "")
            {
                MessageBox.Show("Поле номера протокола не должно быть пустым");
                return false;
            }
            else if (textBlockSurname.Text.Trim() == "") {
                MessageBox.Show("Поле фамилия не должно быть пустым");
                return false;
            }
            else if (textBlockName.Text.Trim() == "") {
                MessageBox.Show("Поле имени не должно быть пустым");
                return false;
            }
            else if (textBlockPatronymic.Text.Trim() == "") {
                MessageBox.Show("Поле отчества не должно быть пустым");
                return false;
            }
            else if (textBlockTheme.Text.Trim() == "") {
                MessageBox.Show("Поле темы не должно быть пустым");
                return false;
            }
            else if (textBlockDirector.Text.Trim() == "") {
                MessageBox.Show("Поле руководителя не должно быть пустым");
                return false;
            }
            else if (textBlockDate.Text.Trim() == "") {
                MessageBox.Show("Поле даты не должно быть пустым");
                return false;
            }
            /*else if (textBlockLocation.Text.Trim() == "") {
                MessageBox.Show("Поле места не должно быть пустым");
                return false;
            }*/
            
            return true;
        }

        void InitVkr()
        {
            vkr.Group = textBlockGroup.Text;
            vkr.ProtocolNumber = textBlockProtocolNumber.Text;
            vkr.Surname = textBlockSurname.Text;
            vkr.Name = textBlockName.Text;
            vkr.Patronymic = textBlockPatronymic.Text;
            vkr.Theme = textBlockTheme.Text;
            vkr.Director = textBlockDirector.Text;
            vkr.Date = Convert.ToDateTime(textBlockDate.Text);
            vkr.Location = textBlockLocation.Text;
        }

        void InitTheses()
        {
            theses.Group = textBlockGroup.Text;
            theses.ProtocolNumber = textBlockProtocolNumber.Text;
            theses.Surname = textBlockSurname.Text;
            theses.Name = textBlockName.Text;
            theses.Patronymic = textBlockPatronymic.Text;
            theses.Theme = textBlockTheme.Text;
            theses.Director = textBlockDirector.Text;
            theses.Date = Convert.ToDateTime(textBlockDate.Text);
            theses.Location = textBlockLocation.Text;
        }

        private void AddVkr_Click(object sender, RoutedEventArgs e)
        {
            if(Validate())
                if (vkr != null)
                    if (vkr.Id != 0)
                    {
                        InitVkr();
                        db.SaveChanges();
                        this.Close();
                    }
                    else {
                        InitVkr();
                        db.Vkr.Add(vkr);
                        db.SaveChanges();
                        this.Close();
                    }
                if (theses != null)
                    if (theses.Id != 0) { 
                        InitTheses();
                        db.SaveChanges();
                        this.Close();
                    }
                    else
                    {
                        InitTheses();
                        db.Theses.Add(theses);
                        db.SaveChanges();
                        this.Close();
                    }
        }


    }
}
