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
using Microsoft.Office.Interop;
using vkr.Model;
using Word = Microsoft.Office.Interop.Word;

namespace vkr.View
{
    /// <summary>
    /// Логика взаимодействия для Deduct.xaml
    /// </summary>
    public partial class Deduct : Window
    {
        DocumentsContext db;
        List<DeducCount> itemDeduct;
        public Deduct()
        {
            InitializeComponent();
            db = new DocumentsContext();
            itemDeduct = new List<DeducCount>();
        }


        private void btnDeduct_Click(object sender, RoutedEventArgs e)
        {
            CheckDeduct();
            if(itemDeduct.Count == 0) { 
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите списать эти записи?", 
                    "Потверждение", MessageBoxButton.YesNo, 
                    MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    string pathDocument = System.IO.Path.GetFullPath(@"..\..\") + "\\document.docx";
                    Word.Application wordApp = new Word.Application();
                    wordApp.Visible = true;

                    Word.Document document = wordApp.Documents.OpenNoRepairDialog(pathDocument);
                    document.Activate();

                    Word.Table table = document.Tables[1];
                    int k = 3;
                    itemDeduct.ForEach(it => {
                        table.Rows.Add();
                        table.Cell(k, 1).Range.Text = it.GroupName;
                        table.Cell(k, 2).Range.Text = it.Count.ToString();
                        k++;
                    });

                   
                    Object missing = Type.Missing;
                    Word.Find findObject = wordApp.Selection.Find;
                    findObject.ClearFormatting();
                    findObject.Text = "number";
                    findObject.Replacement.ClearFormatting();
                    findObject.Replacement.Text = itemDeduct.Count.ToString();
                    object replaceAll = Word.WdReplace.wdReplaceAll;
                    findObject.Execute(ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing, ref missing, ref missing,
                    ref replaceAll, ref missing, ref missing, ref missing, ref missing);
                    db.SaveChanges();
                    Load();
                    itemDeduct.Clear();


                }
            else MessageBox.Show("Не выбрано не одной записи");
            
            }
            
        }


        private void CheckDeduct(){
            itemDeduct.AddRange(vkrGrid.ItemsSource.OfType<Vkr>()
                .Where(x => x.CheckDeduct)
                .GroupBy(x => x.Group)
                .Select(x => new DeducCount {
                    Count = x.Count(),
                    GroupName = x.Key
                }).ToList());
            itemDeduct.AddRange(thesesGrid.ItemsSource.OfType<Theses>()
                .Where(x => x.CheckDeduct)
                .GroupBy(x => x.Group)
                .Select(x => new DeducCount
                {
                    Count = x.Count(),
                    GroupName = x.Key
                }).ToList());
            itemDeduct.AddRange(practicalGrid.ItemsSource.OfType<Practical>()
                .Where(x => x.CheckDeduct)
                .GroupBy(x => x.Group)
                .Select(x => new DeducCount
                {
                    Count = x.Count(),
                    GroupName = x.Key
                }).ToList());

            itemDeduct.AddRange(otherDocGrid.ItemsSource.OfType<OtherDocumentation>()
                .Where(x => x.CheckDeduct)
                .GroupBy(x => x.TypeDocumentation)
                .Select(x => new DeducCount
                {
                    Count = x.Count(),
                    GroupName = x.Key
                }).ToList());
        }

        private void vkrLoad(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
            vkrGrid.ItemsSource = db.Vkr.Where(x => DateTime.Now.Year - x.Date.Year >= 5 && x.DateDeleted == null).ToList();
            thesesGrid.ItemsSource = db.Theses.Where(x => DateTime.Now.Year - x.Date.Year >= 5 && x.DateDeleted == null).ToList();
            practicalGrid.ItemsSource = db.Practical.Where(x => x.DateDeleted == null).ToList();
            otherDocGrid.ItemsSource = db.OtherDocumentation.Where(x => DateTime.Now.Year - x.DateDeposit.Year >= x.ShelfLife && x.DateDeleted == null).ToList();
        }
    }
}
