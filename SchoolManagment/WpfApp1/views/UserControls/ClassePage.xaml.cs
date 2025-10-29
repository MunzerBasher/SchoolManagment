using SchoolBLL.IServices;
using SchoolBLL.Services;
using SchoolDLL;
using SchoolDLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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

namespace School.views.UserControls
{
    /// <summary>
    /// Interaction logic for ClassePage.xaml
    /// </summary>
    public partial class ClassePage : UserControl
    {
        public ClassePage(ISemesterServices semesterServices, IYearServices yearservices)
        {
            _yearservices = yearservices;
            _semesterServices = semesterServices;

            InitializeComponent();
        }
        private readonly ISemesterServices _semesterServices;
        private readonly IYearServices _yearservices;
        private int selectedId;


        private async Task LoadAcademicYears()
        {

            var data = await _yearservices.GetAllYear();
            AcademicYearComboBox.ItemsSource = data;
            LoadClasses(data[0].Id);
        }

        private async void LoadClasses(int id = -1)
        {
            var data = (id == -1) ? await _semesterServices.GetAllSemesters() : await _semesterServices.GetSemestersByYear(id);
            ClassesDataGrid.AutoGenerateColumns = true;
            ClassesDataGrid.ItemsSource = data.Select(n => new Semest {Id= n.Id, Name = n.Name, YearName = n.YearName }).ToList();
            await Dispatcher.InvokeAsync(() =>
            {
                if (ClassesDataGrid.Columns.Count >= 3)
                {
                    ClassesDataGrid.Columns[0].Header = "رقم الفصل";
                    ClassesDataGrid.Columns[1].Header = "اسم الفصل";
                    ClassesDataGrid.Columns[2].Header = "العام الدراسي";
                    ClassesDataGrid.Columns[0].Width = 150;
                    ClassesDataGrid.Columns[2].Width = 150;
                    ClassesDataGrid.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                }
            });


        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ClassesDataGrid.SelectedItem != null)
            {
                var selectedYear = (Semest) ClassesDataGrid.SelectedItem;
                ClassNameTextBox.Text = selectedYear.Name;
                ClassNumberTextBox.Text = selectedYear.Id.ToString();
            }
        }
        

        private async void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ClassesDataGrid.SelectedItem != null)
            {

                
                var selectedSemest = (Semest) ClassesDataGrid.SelectedItem;
                
               
                await _semesterServices.Delete(selectedSemest.Id);
                LoadClasses(selectedId);
                ClearFields();
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClassNameTextBox.Text) || AcademicYearComboBox.SelectedItem == null)
            {
                MessageBox.Show("يرجى إدخال جميع البيانات.", "تحذير", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var year = (Year)AcademicYearComboBox.SelectedItem;
            await _semesterServices.Save(new SchoolDLL.Entities.Semester {
                Name = ClassNameTextBox.Text,
                YearId = year.Id,
              
            });

            LoadClasses(selectedId);
            ClearFields();//
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            ClassNumberTextBox.Clear();
            ClassNameTextBox.Clear();
            AcademicYearComboBox.SelectedIndex = -1;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
           
        }

        private void AcademicYearComboBox_Selected(object sender, RoutedEventArgs e)
        {
           
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           await LoadAcademicYears();
            
        }

        private void AcademicYearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var year = (Year)AcademicYearComboBox.SelectedItem;
            selectedId = year is null ? 4 : year.Id;
            LoadClasses(selectedId);
        }

        struct Semest
        {
            public Semest()
            {

            }
            public int Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public string YearName { get; set; } = string.Empty;
        }

    }
    
}