using System;
using System.Collections.Generic;
using System.Data.Objects;
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
            textBlockGroup.ItemsSource = db.Vkr.Where(x => x.DateDeleted == null).Select(x => x.Group).OrderBy(x => x).Distinct().ToList();
            FindCount();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int days = 0;
            if (textBlockRemainedMonth.Text != "") days += int.Parse(textBlockRemainedMonth.Text) * 30;
            if (textBlockRemainedDay.Text != "") days +=  int.Parse(textBlockRemainedDay.Text);
            if (textBlockRemainedYears.Text != "") days += int.Parse(textBlockRemainedYears.Text) * 365;

            vkrGrid.ItemsSource = db.Vkr.Where(
                x => 
                x.DateDeleted == null &&
                x.Group.StartsWith(textBlockGroup.Text) &&
                (x.ProtocolNumber == textBlockProtocolNumber.Text || textBlockProtocolNumber.Text == "") &&
                x.Surname.StartsWith(textBlockSurname.Text) &&
                x.Name.StartsWith(textBlockName.Text) &&
                x.Patronymic.StartsWith(textBlockPatronymic.Text) &&
                x.Theme.StartsWith(textBlockTheme.Text) &&
                x.Director.Contains(textBlockDirector.Text) &&
                x.Location.StartsWith(textBlockLocation.Text) &&
                (x.Date.Year.ToString() == textBlockYears.Text || textBlockYears.Text == "") &&
                (x.Date.Month.ToString() == textBlockMonth.Text || textBlockMonth.Text == "") &&
                (x.Date.Day.ToString() == textBlockDay.Text || textBlockDay.Text == "") &&
                (((DateTime.Now.Year-x.Date.Year)*365 + (DateTime.Now.Month - x.Date.Month) * 30 + (DateTime.Now.Day - x.Date.Day) > 5 * 365 - days &&
                (DateTime.Now.Year - x.Date.Year) * 365 + (DateTime.Now.Month - x.Date.Month) * 30 + (DateTime.Now.Day - x.Date.Day) < 5 * 365)
                || (textBlockRemainedMonth.Text == "" && textBlockRemainedDay.Text == "" && textBlockRemainedYears.Text == ""))

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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            LoadVkr();
        }
    }
}
