﻿using System;
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
        }

        private void FindCount()
        {
            findCount.Text = "Найдено: " + otherDocGrid.Items.Count.ToString();
        }

        private void LoadOtherDoc()
        {
            otherDocGrid.ItemsSource = db.OtherDocumentation.Where(x => x.DateDeleted == null).ToList();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            otherDocGrid.ItemsSource = db.OtherDocumentation.Where(
                x => x.TypeDocumentation.StartsWith(textBlockTypeDocumentation.Text) &&
                (x.DateDeposit.Year.ToString() == textBlockDepositYears.Text || textBlockDepositYears.Text == "") &&
                (x.DateDeposit.Month.ToString() == textBlockDepositMonth.Text || textBlockDepositMonth.Text == "") &&
                (x.DateDeposit.Day.ToString() == textBlockDepositDay.Text || textBlockDepositDay.Text == "") &&
                x.ShelfLife.ToString().StartsWith(textBlockSheflLife.Text) &&
                x.Location.StartsWith(textBlockLocation.Text) &&
                x.DateDeleted != null
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

    }
}
