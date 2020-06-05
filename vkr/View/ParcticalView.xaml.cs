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
            FindCount();
        }

        private void FindCount()
        {
            findCount.Text = "Найдено: " + practicalGrid.Items.Count.ToString();
        }

        private void LoadPractical()
        {
            practicalGrid.ItemsSource = db.Practical.Where(x => x.DateDeleted == null).ToList();
            textBlockGroup.ItemsSource = db.Practical.Where(x => x.DateDeleted == null).Select(x => x.Group).OrderBy(x => x).Distinct().ToList();
            FindCount();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int days = 0;
            if (textBlockRemainedMonth.Text != "") days += int.Parse(textBlockRemainedMonth.Text) * 30;
            if (textBlockRemainedDay.Text != "") days += int.Parse(textBlockRemainedDay.Text);
            if (textBlockRemainedYears.Text != "") days += int.Parse(textBlockRemainedYears.Text) * 365;

            practicalGrid.ItemsSource = db.Practical.Where(
                x => x.Group.StartsWith(textBlockGroup.Text) &&
                x.Name.StartsWith(textBlockName.Text) &&
                x.Surname.StartsWith(textBlockSurname.Text) &&
                x.Patronymic.StartsWith(textBlockPatronymic.Text) &&
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
                x.Director.Contains(textBlockDirector.Text) &&
                x.DateDeleted == null &&
                (((DateTime.Now.Year - x.EndOfPractice.Year) * 365 + (DateTime.Now.Month - x.EndOfPractice.Month) * 30 + (DateTime.Now.Day - x.EndOfPractice.Day) > 5 * 365 - days &&
                (DateTime.Now.Year - x.EndOfPractice.Year) * 365 + (DateTime.Now.Month - x.EndOfPractice.Month) * 30 + (DateTime.Now.Day - x.EndOfPractice.Day) < 5 * 365)
                || (textBlockRemainedMonth.Text == "" && textBlockRemainedDay.Text == "" && textBlockRemainedYears.Text == ""))
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            LoadPractical();
        }
    }
}
