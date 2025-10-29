using BLL.IServices;
using BLL.Services;
using DLL.Entities;
using SchoolBLL.IServices;
using SchoolBLL.Services;
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
    /// Interaction logic for SubjectsPage.xaml
    /// </summary>
    public partial class SubjectsPage : UserControl
    {
        public SubjectsPage(IShareServices shareServices , ISubjectServices subjectServices)
        {
            _subjectServices = subjectServices;
            _ShareServices = shareServices;
            InitializeComponent();
        }


        private readonly ISubjectServices _subjectServices;
        private readonly IShareServices _ShareServices;
        private int selectedClass;
        private int SelectedSubjectId;
        private string SelectedClassName = default!;

        private async void LoaddgClassSections()
        {
            dgSubjects.AutoGenerateColumns = true;
            dgSubjects.ItemsSource = await _subjectServices.GetAllSubjects();
            await Dispatcher.InvokeAsync(() =>
            {
                if (dgSubjects.Columns.Count >= 3)
                {
                    dgSubjects.Columns[0].Header = "الرقم";
                    dgSubjects.Columns[1].Header = "كود المادة";
                    dgSubjects.Columns[2].Header = "اسم المادة";
                    dgSubjects.Columns[4].Header = "عدد الساعات";
                    dgSubjects.Columns[3].Header = "الصف الدراسي";
                    dgSubjects.Columns[5].Header = "الوصف";

                    dgSubjects.Columns[0].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    dgSubjects.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    dgSubjects.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    dgSubjects.Columns[3].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    dgSubjects.Columns[4].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    dgSubjects.Columns[5].Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                }
            });
        }



        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgSubjects.SelectedItem != null)
            {
                var selectedSubject  = (SubjectTable)dgSubjects.SelectedItem;
                txtName.Text = selectedSubject.Name;
                txtCode.Text = selectedSubject.Code;
                SelectedSubjectId = selectedSubject.Id;

            }
            
        }


        private async void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgSubjects.SelectedItem != null)
            {
                var selectedSubject = (SubjectTable)dgSubjects.SelectedItem;
                await _subjectServices.Delete(selectedSubject.Id);

                LoaddgClassSections();
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtName.Text)|| string.IsNullOrEmpty(txtCode.Text))
            {

                return;
            }
            var save = await _subjectServices.save(new Subject { Id = SelectedSubjectId,ClassId = selectedClass,Code = txtCode.Text, Name = txtName.Text });

            LoaddgClassSections(); ;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void dgSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private async void ScrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            var data = await _ShareServices. GetClassesInActiveYear();
            cbClass.ItemsSource = data;
            LoaddgClassSections();
        }

        private void cbClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbClass.SelectedItem is null) 
                return;
            var clas = ((ClassComb)cbClass.SelectedItem);
            SelectedClassName = clas.Name;
            selectedClass = clas.Id;
        }



    }


}