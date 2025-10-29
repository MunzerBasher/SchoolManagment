using BLL.IServices;
using BLL.Services;
using DAL.Entities;
using Microsoft.IdentityModel.Tokens;
using SchoolBLL.IServices;
using SchoolBLL.Services;
using SchoolDLL.Entities;
using SchoolDLL.Entities.DLL.Entities;
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
    /// Interaction logic for StudentsPage.xaml
    /// </summary>
    public partial class StudentsPage : UserControl
    {
        public StudentsPage(IStudentServices studentServices, IShareServices shareServices)
        {
            _studentServices = studentServices;
            _shareServices = shareServices;
            InitializeComponent();
        }


        private async void LoadData()
        {
            cbGender.ItemsSource = await _studentServices.GetGender();
            cbClass.ItemsSource = await _shareServices.GetClassesInActiveYear();

            selectedClassId = ((ClassComb)cbClass.Items[0]).Id;
            cbStage.ItemsSource = await _shareServices.GetActiveLevelsComb();
            cbSection.ItemsSource = await _shareServices.GetAllCombSections(selectedClassId);

        }

        private readonly IStudentServices _studentServices;
        private readonly IShareServices _shareServices;
        private string selectedBirthDate = string.Empty;
        private int selectedSectionId;
        private int selectedGenderId;
        private int selectedStageId;
        private int selectedClassId;
        private int selectedStudentId;



        private void cbGender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbGender.SelectedItem is null) return;
            selectedGenderId = ((Gender)cbGender.SelectedItem).Id;
        }

        private async void cbStage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbStage.SelectedItem is null) return;
            selectedStageId = ((LevleComb)cbStage.SelectedItem).Id;
            cbClass.ItemsSource = await _shareServices.GetClassesCombByLevel(selectedStageId);
        }

        private async void cbClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbClass.SelectedItem is null) return;
            selectedClassId = ((ClassComb)cbClass.SelectedItem).Id;
            cbSection.ItemsSource = await _shareServices.GetAllCombSections(selectedClassId);


        }

        private void cbSection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbSection.SelectedItem is null) return;
            selectedSectionId = ((SchoolDLL.Entities.SectionComb)cbSection.SelectedItem).Id;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
            await LoaddgClassSections();
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFullName.Text)|| string.IsNullOrEmpty(txtGuardianPhone.Text) || 
                (cbSection.SelectedItem is null) || (cbClass.SelectedItem is null) || (cbClass.SelectedItem is null)
                || (cbGender.SelectedItem is null)
                )
            {
                return;
            }
            
            var StudentId = (string.IsNullOrEmpty(txtStudentId.Text) || !(int.TryParse(txtStudentId.Text, out int value))) ? 0: value;

            var result = await _studentServices.Save(new Student {
                Id = StudentId, 
                BirthDate = DateTime.Parse(dpBirthDate.Text),
                ParentPhone = txtGuardianPhone.Text,
                SectionId= selectedSectionId,
                GenderId= selectedGenderId,
                ClassId = selectedClassId,
                Name = txtFullName.Text
                
            }
            );
            if(result.StudentId > 0)
                await LoaddgClassSections();
        }

        private async Task LoaddgClassSections()
        {
            dgStudents.AutoGenerateColumns = true;
            dgStudents.ItemsSource = await _studentServices.GetAllStudents();
            await Dispatcher.InvokeAsync(() =>
            {
                if (dgStudents.Columns.Count >= 3)
                {
                    dgStudents.Columns[0].Header = "الرقم";
                    dgStudents.Columns[1].Header = "الاسم الكامل";
                    dgStudents.Columns[2].Header = "المرحلة";
                    dgStudents.Columns[4].Header = "الجنس";
                    dgStudents.Columns[3].Header = "الصف الدراسي";
                    dgStudents.Columns[5].Header = "الشعبة";
                    dgStudents.Columns[6].Header = "الرقم ولي الأمر";

                    dgStudents.Columns[0].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    dgStudents.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    dgStudents.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    dgStudents.Columns[3].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    dgStudents.Columns[4].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    dgStudents.Columns[5].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    dgStudents.Columns[6].Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                }
            });
        }

        private void dpBirthDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedBirthDate = dpBirthDate.Text;
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgStudents.SelectedItem is not null)
            {
               var student = (StudentTable)dgStudents.SelectedItem; 
               
               txtFullName.Text = student.Name;
               txtGuardianPhone.Text = student.ParentPhone;
               txtStudentId.Text = student.Id.ToString();

            }
        }


        private async void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgStudents.SelectedItem is not null)
            {

                var student = (StudentTable)dgStudents.SelectedItem;
                var result = await _studentServices.Delete(student.Id);
                await LoaddgClassSections();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            selectedStudentId = 0;
            txtFullName.Text = string.Empty;
            txtGuardianPhone.Text = string.Empty;
        }
    }


}

