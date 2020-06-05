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

namespace vkr.View
{
    /// <summary>
    /// Логика взаимодействия для OtherDocumentation.xaml
    /// </summary>
    public partial class OtherDocumentationView : Page
    {
        DocumentsContext db;
        public OtherDocumentationView()
        {
            InitializeComponent();
            db = new DocumentsContext();
            LoadOtherDoc();
            FindCount();
        }

        private void FindCount()
        {
            findCount.Text = "Найдено: " + otherDocGrid.Items.Count.ToString();

        }

        private void LoadOtherDoc()
        {
            otherDocGrid.ItemsSource = db.OtherDocumentation.Where(x => x.DateDeleted == null).ToList();
            FindCount();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            int days = 0;
            if (textBlockRemainedMonth.Text != "") days += int.Parse(textBlockRemainedMonth.Text) * 30;
            if (textBlockRemainedDay.Text != "") days += int.Parse(textBlockRemainedDay.Text);
            if (textBlockRemainedYears.Text != "") days += int.Parse(textBlockRemainedYears.Text) * 365;

            otherDocGrid.ItemsSource = db.OtherDocumentation.Where(
                x => x.TypeDocumentation.StartsWith(textBlockTypeDocumentation.Text) &&
                (x.DateDeposit.Year.ToString() == textBlockDepositYears.Text || textBlockDepositYears.Text == "") &&
                (x.DateDeposit.Month.ToString() == textBlockDepositMonth.Text || textBlockDepositMonth.Text == "") &&
                (x.DateDeposit.Day.ToString() == textBlockDepositDay.Text || textBlockDepositDay.Text == "") &&
                x.ShelfLife.ToString().StartsWith(textBlockSheflLife.Text) &&
                x.Location.StartsWith(textBlockLocation.Text) &&
                x.DateDeleted == null &&
                (((DateTime.Now.Year - x.DateDeposit.Year) * 365 + (DateTime.Now.Month - x.DateDeposit.Month) * 30 + (DateTime.Now.Day - x.DateDeposit.Day) > x.ShelfLife * 365 - days &&
                (DateTime.Now.Year - x.DateDeposit.Year) * 365 + (DateTime.Now.Month - x.DateDeposit.Month) * 30 + (DateTime.Now.Day - x.DateDeposit.Day) < x.ShelfLife * 365)
                || (textBlockRemainedMonth.Text == "" && textBlockRemainedDay.Text == "" && textBlockRemainedYears.Text == ""))
            ).ToList();

            FindCount();
        }


        private void changeRowVKR(object sender, RoutedEventArgs e)
        {
            Model.OtherDocumentation itemFocus = otherDocGrid.SelectedValue as Model.OtherDocumentation;
            AddOrChangeOtherDocumentation changePractical = new AddOrChangeOtherDocumentation(itemFocus, db);
            changePractical.ShowDialog();
            LoadOtherDoc();
            FindCount();
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddOrChangeOtherDocumentation changePractical = new AddOrChangeOtherDocumentation(db);
            changePractical.ShowDialog();
            LoadOtherDoc();
            FindCount();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            LoadOtherDoc();
        }
    }
}
