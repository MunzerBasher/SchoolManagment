using BLL.IServices;
using BLL.Services;
using SchoolBLL.IServices;
using SchoolBLL.Services;
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

namespace School.views.UserControls
{
    /// <summary>
    /// Interaction logic for AcademicClassesPage.xaml
    /// </summary>
    public partial class AcademicClassesPage : UserControl
    {
       
        public AcademicClassesPage(IShareServices shareservices , IClassServices classServices)
        {
            _shareservices = shareservices;
            _classServices = classServices;             
            InitializeComponent();
         
        }

        private readonly IClassServices _classServices;
        private readonly IShareServices _shareservices;
        private int AcademicYearId;
        private int SemesterId;
        private int LevelId;
        private int ClassId;

        async Task LoadBox()
        {
            YearComboBox.ItemsSource = await _shareservices.GetYearCombAsync();
            var year = (Year)YearComboBox.Items[0];
            SemesterComboBox.ItemsSource = await _shareservices.GetMinSemesterCombAsync(year.Id);
            var semester = (MinSemester)SemesterComboBox.Items[0];
            LevelComboBox.ItemsSource = await _shareservices.GetLevleCombAsync(semester.Id);
            var levle = (LevleComb)LevelComboBox.Items[0];
            await FullTable(levle.Id);
        }


        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ClassNameTextBox.Text) || SemesterId < 0)
            {
                
                MessageBox.Show("الرجاء إدخال اسم الصف الدراسي");
                return;
            }

            var name = ClassNameTextBox.Text;
            var added = await _classServices.Save(new Class {Id = ClassId, LevelId = LevelId, Name = name });
           

            ClearInputs();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            ClassNameTextBox.Clear();
            YearComboBox.SelectedIndex = -1;
            SemesterComboBox.SelectedIndex = -1;
            LevelComboBox.SelectedIndex = -1;
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = SearchTextBox.Text.ToLower();
            
        }


        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
           await LoadBox();
        }


        private async void YearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var year = (Year)YearComboBox.SelectedItem;
            AcademicYearId = (year is not null) ? year.Id : ((YearComboBox.Items.Count > 0) ? ((Year)YearComboBox.Items[0]).Id : 0);
            SemesterComboBox.ItemsSource = await _shareservices.GetMinSemesterCombAsync(AcademicYearId);
        }

        private async void SemesterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var semester = (MinSemester)SemesterComboBox.SelectedItem;
            SemesterId = (semester is not null) ? semester.Id : (SemesterComboBox.Items.Count > 0 ? ((MinSemester)SemesterComboBox.Items[0]).Id : 0);
            LevelComboBox.ItemsSource = await _shareservices.GetLevleCombAsync(SemesterId);
        }

        private async void LevelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var level = (LevleComb)LevelComboBox.SelectedItem;
            LevelId = (level is not null) ? level.Id : (LevelComboBox.Items.Count > 0 ? ((LevleComb)LevelComboBox.Items[0]).Id : 0);
            await FullTable(LevelId);
        
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ClassesDataGrid.SelectedItem != null)
            {
                var cla = (ClassTable)ClassesDataGrid.SelectedItem;
                if (cla.Id < 0)
                {
                    MessageBox.Show("الرجاء إدخال اسم الصف الدراسي");
                    return;
                }
                
                ClassNameTextBox.Text = cla.Name;
                ClassId = cla.Id;


            }
        }


        private async void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (ClassesDataGrid.SelectedItem != null)
            {
                var cla = (ClassTable)ClassesDataGrid.SelectedItem;
                var value = await _classServices.Delete(cla.Id);
                await FullTable(LevelId);

            }

        }



        private async Task FullTable(int levelId)
        {
            
            ClassesDataGrid.AutoGenerateColumns = true;
            var data = await _classServices.GetClassesByLevel(levelId);
            ClassesDataGrid.ItemsSource = data.Select(a => new ClassTable 
            {
                Id = a.Id ,
                Name = a.Name,
                LevelName = a.LevelName , 
                SemesterName = a.SemesterName, 
                YearName = a.YearName
            });
            await Dispatcher.InvokeAsync(() =>
            {
                if (ClassesDataGrid.Columns.Count >= 3)
                {
                    ClassesDataGrid.Columns[3].Header = "العام الدراسي";
                    ClassesDataGrid.Columns[2   ].Header = "الفصل الدراسي";
                    ClassesDataGrid.Columns[4].Header = "اسم المرحلة";
                    ClassesDataGrid.Columns[1].Header = "اسم الصف";
                    ClassesDataGrid.Columns[0].Header = "رقم الصف";
                    ClassesDataGrid.Columns[0].Width = 150;
                    ClassesDataGrid.Columns[2].Width = 150;
                    ClassesDataGrid.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    ClassesDataGrid.Columns[3].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    ClassesDataGrid.Columns[4].Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                }
            });
        }
    }




}

public class ClassTable
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string SemesterName { get; set; } = string.Empty;

    public string YearName { get; set; } = string.Empty;

    public string LevelName { get; set; } = string.Empty;

}