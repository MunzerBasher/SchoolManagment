using Microsoft.IdentityModel.Tokens;
using SchoolDLL;
using SchoolDLL.Entities;
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

namespace School.views.Pages
{
    /// <summary>
    /// Interaction logic for ClassesPage.xaml
    /// </summary>
    public partial class ClassesPage : UserControl
    {
        public ClassesPage(IYearServices yearServices)
        {
            _yearServices = yearServices;
            InitializeComponent();
        }
        private readonly IYearServices _yearServices;
        private bool status;
        

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            YearNameTextBox.Text = string.Empty;
            YearNumberTextBox.Text = string.Empty;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (YearNameTextBox.Text == string.Empty)
            {
                MessageBox.Show("أدخل اسم العام الدراسي ", "تنبيه !", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            
            var Id = (YearNumberTextBox.Text == string.Empty) ? -1 : (int.TryParse(YearNumberTextBox.Text, out int id) ? int.Parse(YearNumberTextBox.Text): -1);

            var value = await _yearServices.Save(new Year
            {
                Id = Id,
                isActive = true,
                Name = YearNameTextBox.Text,
            });
            await LoadData();
        }


        private void YearNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private async void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            await LoadData(SearchTextBox.Text);
        }


        private async Task LoadData(string? str = "")
        {
            var data = await _yearServices.GetAllYear();

            
            AcademicYearsDataGrid.AutoGenerateColumns = true;
            if (!str.IsNullOrEmpty())
            {
                AcademicYearsDataGrid.ItemsSource = data.Select(a => a.Name.Contains(str)).ToList();
            }
            else
            {
                AcademicYearsDataGrid.ItemsSource = data;
            }

                await Dispatcher.InvokeAsync(() =>
                {
                    if (AcademicYearsDataGrid.Columns.Count >= 3)
                    {
                        AcademicYearsDataGrid.Columns[0].Header = "رقم العام الدراسي";
                        AcademicYearsDataGrid.Columns[1].Header = "السنة الدراسية";
                        AcademicYearsDataGrid.Columns[2].Header = "الحالة";
                        AcademicYearsDataGrid.Columns[0].Width = 150;
                        AcademicYearsDataGrid.Columns[2].Width = 100;
                        AcademicYearsDataGrid.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                    }
                });
            }
        
            private async void UserControl_Loaded(object sender, RoutedEventArgs e)
            {
                await LoadData();
            }

            private void AcademicYearsDataGrid_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
            {
                if (AcademicYearsDataGrid.SelectedCells.Count > 0)
                {
                    var year = AcademicYearsDataGrid.SelectedItem as Year;

                  

                 }
            }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (AcademicYearsDataGrid.SelectedItem != null)
            {
                var selectedYear = (Year)AcademicYearsDataGrid.SelectedItem;
                YearNameTextBox.Text = selectedYear.Name;
                YearNumberTextBox.Text = selectedYear.Id.ToString();
            }
        }

        private async void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (AcademicYearsDataGrid.SelectedItem != null)
            {
                var selectedYear = (Year)AcademicYearsDataGrid.SelectedItem;
                var value = await _yearServices.Delete(selectedYear.Id);
                await LoadData();
                
            }
        }


    }


}