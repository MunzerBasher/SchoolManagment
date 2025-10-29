using BLL.IServices;
using System.Windows;
using SchoolDLL.Entities;
using SchoolBLL.IServices;
using System.Windows.Controls;



namespace School.views.UserControls
{
    /// <summary>
    /// Interaction logic for ClassSectionsPage.xaml
    /// </summary>
    public partial class ClassSectionsPage : UserControl
    {
        public ClassSectionsPage(IShareServices shareservices, ISectionServices sectionServices)
        {
            _sectionServices = sectionServices;
           _shareservices = shareservices;
            InitializeComponent();
            LoadData();
        }
        private readonly ISectionServices _sectionServices;
        private readonly IShareServices _shareservices;
        private int SelectedSemesterId;
        private int SelectedLevelId;
        private int SelectedYearId;
        private int SelectedClassId;
        private int SelectedStage;
        private int SelectedClass;
        private int SelectedSectionId;
        private int SelectedClassSections;




        private async void LoadData()
        {
            cbAcademicYear.ItemsSource = await _shareservices.GetYearCombAsync();
            var year = (Year)cbAcademicYear.Items[0];
            cbSemester.ItemsSource = await _shareservices.GetMinSemesterCombAsync(year.Id);
            var semester = (MinSemester)cbSemester.Items[0];
            cbStage.ItemsSource = await _shareservices.GetLevleCombAsync(semester.Id);
            var levle = (LevleComb)cbStage.Items[0];
            cbClass.ItemsSource = await _shareservices.GetClassesCombByLevel(levelId: levle.Id);
            SelectedClass = ((ClassComb)cbClass.Items[0]).Id;

            LoaddgClassSections(SelectedClass);
        }

        private async void LoaddgClassSections(int classId)
        {
            dgClassSections.AutoGenerateColumns = true;
            dgClassSections.ItemsSource = await _sectionServices.GetSectionsByClass(classId);
            await Dispatcher.InvokeAsync(() =>
            {
                if (dgClassSections.Columns.Count >= 3)
                {
                    dgClassSections.Columns[0].Header = "رقم الشعبة";
                    dgClassSections.Columns[1].Header = "الشعبة";
                    dgClassSections.Columns[2].Header = " الصف";
                    dgClassSections.Columns[4].Header = "الفصل الدراسي";
                    dgClassSections.Columns[3].Header = " المرحلة";
                    dgClassSections.Columns[5].Header = "العام الدراسي";
                 
                    dgClassSections.Columns[0].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    dgClassSections.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    dgClassSections.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    dgClassSections.Columns[3].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    dgClassSections.Columns[4].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    dgClassSections.Columns[5].Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                }
            });
        }

        private  void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
             LoadData();
        }

        
        private void cbClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbClass.SelectedItem is null)
                return;
            SelectedClass = ((ClassComb)cbClass.SelectedItem).Id;
            LoaddgClassSections(SelectedClass);
        }


        
        private async void cbStage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbStage.SelectedItem is null)
                return;
            
            SelectedStage = ((LevleComb)cbStage.SelectedItem).Id;
            cbClass.ItemsSource = await _shareservices.GetClassesCombByLevel(levelId: SelectedStage);
        }



        private async void cbSemester_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbSemester.SelectedItem is null)
                return;
            SelectedSemesterId = ((MinSemester)cbSemester.SelectedItem).Id;
            cbStage.ItemsSource = await _shareservices.GetLevleCombAsync(SelectedSemesterId);
           
        }

        private async void cbAcademicYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbAcademicYear.SelectedItem is null)
                return;

            SelectedYearId = ((Year)cbAcademicYear.SelectedItem).Id;
            cbSemester.ItemsSource = await _shareservices.GetMinSemesterCombAsync(SelectedYearId);

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtSectionName.Text) || SelectedClass <= 0)
            {
                MessageBox.Show("يجب إدخال جميع الحقول", "تنبيه");
                return;
            }
            var value = await _sectionServices.Save(new SchoolDLL.Entities.Section {Id = SelectedSectionId , ClassId = SelectedClass, Name = txtSectionName.Text });

            if(value == -1)
            {
                MessageBox.Show("لا يمكن أضافة شعب بنفس الاسم للصف الحالي", "تنبيه");
                return;
            }
            LoaddgClassSections(SelectedClass);
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgClassSections.SelectedItem != null)
            {
                var selectedSection = (SectionTable)dgClassSections.SelectedItem;
                txtSectionName.Text = selectedSection.Name;
                SelectedSectionId = selectedSection.Id;
            }

        }


        private async void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (dgClassSections.SelectedItem != null)
            {
                var selectedSection = (SectionTable)dgClassSections.SelectedItem;
                var value = await _sectionServices.Delete(selectedSection.Id);
                LoaddgClassSections(SelectedClass); 
            }
        }

        private void dgClassSections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgClassSections.SelectedItem is null)
                return;
            SelectedClassSections = ((SectionTable)cbClass.SelectedItem).Id;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SelectedSectionId = 0;
            txtSectionName.Text = string.Empty;
        }
    
    }



}