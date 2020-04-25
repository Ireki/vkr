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
using vkr.View;

namespace vkr
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DeductCount();
        }

        private void clickDeduct(object sender, RoutedEventArgs e)
        {
            Deduct deduct = new Deduct();
            deduct.ShowDialog();
            DeductCount();
        }

        private void DeductCount()
        {
            DocumentsContext db = new DocumentsContext();
            int k = 0;
            k += db.Vkr.Count(x => DateTime.Now.Year - x.Date.Year >= 5 && x.DateDeleted == null);
            k += db.Theses.Count(x => DateTime.Now.Year - x.Date.Year >= 5 && x.DateDeleted == null);
            k += db.Practical.Count(x => DateTime.Now.Year - x.EndOfPractice.Year >= 5 && x.DateDeleted == null);
            k += db.OtherDocumentation.Count(x => DateTime.Now.Year - x.DateDeposit.Year >= x.ShelfLife && x.DateDeleted == null);
            deductCount.Text = "На списание: " + k.ToString();
        }

    }
}
