using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Логика взаимодействия для VkrView.xaml
    /// </summary>
    public partial class VkrView : Page
    {

        DocumentsContext db;
        public VkrView()
        {
            InitializeComponent();
            db = new DocumentsContext();
            LoadVkr();
        }

        private void FindCount()
        {
            findCount.Text = "Найдено: " + vkrGrid.Items.Count.ToString();
        }
        private void LoadVkr()
        {
            vkrGrid.ItemsSource = db.Vkr.Where(x => x.DateDeleted == null).ToList();
            FindCount();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            vkrGrid.ItemsSource = db.Vkr.Where(
                    x => x.Group.StartsWith(textBlockGroup.Text) &&
                    (x.ProtocolNumber == textBlockProtocolNumber.Text || textBlockProtocolNumber.Text == "") &&
                    x.Surname.StartsWith(textBlockSurname.Text) &&
                    x.Name.StartsWith(textBlockName.Text) &&
                    x.Patronymic.StartsWith(textBlockPatronymic.Text) &&
                    x.Theme.StartsWith(textBlockTheme.Text) &&
                    x.Director.Contains(textBlockDirector.Text) &&
                    x.Location.StartsWith(textBlockLocation.Text) &&
                    (x.Date.Year.ToString() == textBlockYears.Text || textBlockYears.Text == "") &&
                    (x.Date.Month.ToString() == textBlockMonth.Text || textBlockMonth.Text == "") &&
                    (x.Date.Day.ToString() == textBlockDay.Text || textBlockDay.Text == "")
               ).ToList();
                
            FindCount();

        }

        private void changeRowVkrView(object sender, RoutedEventArgs e)
        {
            Vkr itemFocus = vkrGrid.SelectedValue as Vkr;
            AddOrChange changeVkr = new AddOrChange(itemFocus, db);
            changeVkr.ShowDialog();
            LoadVkr();
            FindCount();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrChange changeVkr = new AddOrChange(db);
            changeVkr.ShowDialog();
            LoadVkr();
            FindCount();
        }
    }
}
