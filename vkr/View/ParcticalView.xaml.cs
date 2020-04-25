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
    public partial class PracticalView : Page
    {

        DocumentsContext db;
        public PracticalView()
        {
            InitializeComponent();
            db = new DocumentsContext();
            LoadPractical();
        }

        private void FindCount()
        {
            findCount.Text = "Найдено: " + practicalGrid.Items.Count.ToString();
        }

        private void LoadPractical()
        {
            practicalGrid.ItemsSource = db.Practical.Where(x => x.DateDeleted == null).ToList();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            practicalGrid.ItemsSource = db.Practical.Where(
                x => x.Group.StartsWith(textBlockGroup.Text) &&
                x.Fio.StartsWith(textBlockFio.Text) &&
                x.HumanSettlement.StartsWith(textBlockHumanSettlement.Text) &&
                x.PracticeBase.StartsWith(textBlockPracticeBase.Text) &&
                (x.StartOfPractice.Year.ToString() == textBlockStartYears.Text || textBlockStartYears.Text == "") &&
                (x.StartOfPractice.Month.ToString() == textBlockStartMonth.Text || textBlockStartMonth.Text == "") &&
                (x.StartOfPractice.Day.ToString() == textBlockStartDay.Text || textBlockStartDay.Text == "") &&
                (x.EndOfPractice.Year.ToString() == textBlockEndYears.Text || textBlockEndYears.Text == "") &&
                (x.EndOfPractice.Month.ToString() == textBlockEndMonth.Text || textBlockEndMonth.Text == "") &&
                (x.EndOfPractice.Day.ToString() == textBlockEndDay.Text || textBlockEndDay.Text == "") &&
                x.FormOfPractice.StartsWith(textBlockDirector.Text) &&
                x.Payment.StartsWith(textBlockPayment.Text) &&
                x.Director.Contains(textBlockDirector.Text)
            ).ToList();
            FindCount();
        }


        private void changeRowVKR(object sender, RoutedEventArgs e)
        {
            Model.Practical itemFocus = practicalGrid.SelectedValue as Model.Practical;
            AddOrChangePractical changePractical = new AddOrChangePractical(itemFocus, db);
            changePractical.ShowDialog();
            LoadPractical();
            FindCount();
        }

        private void deleteRowVKR(object sender, RoutedEventArgs e)
        {
            Model.Practical itemFocus = practicalGrid.SelectedValue as Model.Practical;
            itemFocus.DateDeleted = DateTime.Now;
            db.SaveChanges();
            LoadPractical();
            FindCount();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrChangePractical changePractical = new AddOrChangePractical(db);
            changePractical.ShowDialog();
            LoadPractical();
            FindCount();
        }

    }
}
