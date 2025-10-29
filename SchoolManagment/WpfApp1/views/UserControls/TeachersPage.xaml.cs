using BLL.IServices;
using DLL.Entities;
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
    /// Interaction logic for TeachersPage.xaml
    /// </summary>
    public partial class TeachersPage : UserControl
    {
        public TeachersPage(ITeacherService teacherService, IShareServices shareServices)
        {
            _teacherService = teacherService;
            _shareServices = shareServices;
            InitializeComponent();
        }


        private readonly ITeacherService _teacherService;
        private readonly IShareServices _shareServices;
        private int _SelectedsubjectId;
        private int _teacherId;

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            TeacherNameTextBox.Text = string.Empty;
            PhoneTextBox.Text = string.Empty;
            _teacherId = 0;
        }

        private async Task Load()
        {
            TeachersDataGrid.AutoGenerateColumns = true;
            TeachersDataGrid.ItemsSource = await _teacherService.GetAllTeachers();

            await Dispatcher.InvokeAsync(() =>
            {
                if (TeachersDataGrid.Columns.Count >= 3)
                {
                    TeachersDataGrid.Columns[0].Header = "رقم المعلم";
                    TeachersDataGrid.Columns[1].Header = "اسم المعلم";
                    TeachersDataGrid.Columns[2].Header = "المادة";
                    TeachersDataGrid.Columns[3].Header = "رقم الهاتف";
                    TeachersDataGrid.Columns[4].Header =  "الحالة";
                    TeachersDataGrid.Columns[0].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    TeachersDataGrid.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    TeachersDataGrid.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    TeachersDataGrid.Columns[3].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    TeachersDataGrid.Columns[4].Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                }
            });

        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TeacherNameTextBox.Text)|| string.IsNullOrEmpty(PhoneTextBox.Text)
                || (cbSection.SelectedItem is null)
                ) {
                MessageBox.Show("","");
                return;
            }
            var result = await _teacherService.Save(new DLL.Entities.Teacher {
                TeacherId = _teacherId,
                Phone = PhoneTextBox.Text,
                Name = TeacherNameTextBox.Text,
            }, _SelectedsubjectId);
             await Load();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TeachersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (TeachersDataGrid.SelectedItems.Count > 0)
            {
                var Teacher = ((Teacher)TeachersDataGrid.SelectedItem);
                _teacherId = Teacher.TeacherId;
                TeacherNameTextBox.Text = Teacher.Name;
                PhoneTextBox.Text = Teacher.Phone;
            }

        }


        private async void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(TeachersDataGrid.SelectedItems.Count > 0)
            {
                var Id = ((Teacher)TeachersDataGrid.SelectedItem).TeacherId;
                var result = await _teacherService.DeleteTeacher(Id);
                await Load();
            }
        }

        private async Task compbox()
        {
            cbSection.ItemsSource = await _shareServices.GetAllSubjectsCombox();
        }

        private void cbSection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSection.SelectedItem is null) return;

            _SelectedsubjectId = ((SubjectsCombox)cbSection.SelectedItem).Id;
        }
        
        private async void TeachersDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            await compbox();
            await Load();
        }



    }

}