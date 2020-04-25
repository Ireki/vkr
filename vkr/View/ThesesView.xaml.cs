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
    /// Логика взаимодействия для VKR.xaml
    /// </summary>
    public partial class ThesesView : Page
    {

        DocumentsContext db;
        public ThesesView()
        {
            InitializeComponent();
            db = new DocumentsContext();
            LoadTheses();
            FindCount();
        }

        private void FindCount()
        {
            findCount.Text = "Найдено: " + thesesGrid.Items.Count.ToString();
        }

        private void LoadTheses()
        {
            thesesGrid.ItemsSource = db.Theses.Where(x => x.DateDeleted == null).ToList();
            FindCount();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            thesesGrid.ItemsSource = db.Theses.Where(
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


        private void changeRowVKR(object sender, RoutedEventArgs e)
        {
            Model.Theses itemFocus = thesesGrid.SelectedValue as Model.Theses;
            AddOrChange changeTheses = new AddOrChange(itemFocus, db);
            changeTheses.ShowDialog();
            LoadTheses();
            FindCount();
        }

        private void deleteRowVKR(object sender, RoutedEventArgs e)
        {
            Model.Theses itemFocus = thesesGrid.SelectedValue as Model.Theses;
            itemFocus.DateDeleted = DateTime.Now;
            db.SaveChanges();
            LoadTheses();
            FindCount();
        }

        private void clickDeduct(object sender, RoutedEventArgs e)
        {
            Deduct deduct = new Deduct();
            deduct.ShowDialog();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrChange changeTheses = new AddOrChange(db);
            changeTheses.ShowDialog();
            LoadTheses();
            FindCount();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            LoadTheses();
        }
    }
}
