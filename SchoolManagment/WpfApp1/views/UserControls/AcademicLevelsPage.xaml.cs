using SchoolBLL.IServices;
using SchoolBLL.Services;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static School.views.UserControls.ClassePage;

namespace School.views.UserControls
{
    /// <summary>
    /// Interaction logic for AcademicLevelsPage.xaml
    /// </summary>
    public partial class AcademicLevelsPage : UserControl
    {
        public AcademicLevelsPage(IYearServices yearServices, ISemesterServices semesterServices , ILevelServices levelServices)
        {

            _yearServices = yearServices;
            _levelServices = levelServices;
            _semesterServices = semesterServices;
            
            InitializeComponent();

        }

        private int SemesterId;
        private int selectedIsemesterd;
        private int AcademicYearId;
        private readonly IYearServices _yearServices;
        private readonly ILevelServices _levelServices;
        private readonly ISemesterServices _semesterServices;

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var semester = (MinSemester)SemesterComboBox.SelectedItem;
            if ((semester is null) ||(SemesterComboBox.SelectedItem is null) || string.IsNullOrEmpty(LevelNameTextBox.Text))
            {
                MessageBox.Show("يجب إدخال جميع الحقول", "تنبيه");
                return;
            }
            
            await _levelServices.Save(new Level { SemesterId = semester.Id, Name = LevelNameTextBox.Text });
            ClearInputs();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {

            ClearInputs();
        }

        private void ClearInputs()
        {
            LevelNameTextBox.Clear();
            AcademicYearComboBox.SelectedIndex = -1;
            SemesterComboBox.SelectedIndex = -1;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private async Task LoadAcademicYears()
        {

            var data = await _yearServices.GetAllYear();
            AcademicYearComboBox.ItemsSource = data;
        }

        private async Task LoadAcademicSemester(int id)
        {
            var data = await _semesterServices.GetAllMinSemester(id);
            SemesterComboBox.ItemsSource = data;
        }






    private async Task LoadClasses(int id = -1)
    {
        LevelsDataGrid.AutoGenerateColumns = true;
        LevelsDataGrid.ItemsSource =  await _levelServices.GetAllLevels(id); 
        await Dispatcher.InvokeAsync(() =>
        {
            if (LevelsDataGrid.Columns.Count >= 3)
            {
                LevelsDataGrid.Columns[0].Header = "رقم المرحلة";
                LevelsDataGrid.Columns[1].Header = "اسم المرحلة";
                LevelsDataGrid.Columns[2].Header = "العام الدراسي";
                LevelsDataGrid.Columns[3].Header = "الفصل الدراسي";
                LevelsDataGrid.Columns[0].Width = 150;
                LevelsDataGrid.Columns[2].Width = 150;
                LevelsDataGrid.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                LevelsDataGrid.Columns[3].Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            }
        });

    }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadAcademicYears();
            var year = (Year)AcademicYearComboBox.Items[0];
            await LoadAcademicSemester(year.Id);
            var semester = (MinSemester)SemesterComboBox.Items[0];
            await LoadClasses(semester.Id);
        }

        private async void AcademicYearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var year = (Year)AcademicYearComboBox.SelectedItem;
            AcademicYearId = (year is not null)? year.Id :((AcademicYearComboBox.Items.Count > 0) ? ((Year)AcademicYearComboBox.Items[0]).Id:0);
            
            await LoadAcademicSemester(AcademicYearId);
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (LevelsDataGrid.SelectedItem != null)
            { 
                var selectedYear = (LevleTable)LevelsDataGrid.SelectedItem;
                LevelNameTextBox.Text = selectedYear.Name;
                
            }

        }


        private async void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (LevelsDataGrid.SelectedItem != null)
            {

                var selectedsemester = (LevleTable)LevelsDataGrid.SelectedItem;
                await _levelServices.Delete(selectedsemester.Id);
                var semester = (MinSemester)SemesterComboBox.Items[0];
                await LoadClasses(semester.Id);
            }
        }

        private async void SemesterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var semester = (MinSemester)SemesterComboBox.SelectedItem;
            SemesterId = (semester is not null)? semester.Id : (SemesterComboBox.Items.Count > 0?((MinSemester)SemesterComboBox.Items[0]).Id:0);
            await LoadClasses(SemesterId);
        }
    }

}