using BLL.IServices;
using DAL.Entities;
using DLL.Entities;
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
    /// Interaction logic for SalariesPage.xaml
    /// </summary>
    public partial class SalariesPage : UserControl
    {
        private readonly ISalaryServices _salaryLogic;
        public SalariesPage(ISalaryServices salaryServices)
        {
            _salaryLogic = salaryServices;
            InitializeComponent();
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadSalaries();
            await LoadTeachers();
        }

        private async Task LoadSalaries()
        {
            var salaries = await _salaryLogic.GetAllSalariesAsync();
            SalariesDataGrid.ItemsSource = salaries;
        }

        private async Task LoadTeachers()
        {
            //var teachers = await _salaryLogic.();
            //cbTeacher.ItemsSource = teachers;
            //cbTeacher.DisplayMemberPath = "FullName";
            //cbTeacher.SelectedValuePath = "Id";
        }

        private async void AddSalary_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    if (cbTeacher.SelectedValue == null || string.IsNullOrWhiteSpace(txtAmount.Text))
            //    {
            //        MessageBox.Show("الرجاء إدخال جميع البيانات.", "تنبيه", MessageBoxButton.OK, MessageBoxImage.Warning);
            //        return;
            //    }

            //    var salary = new SalaryModel
            //    {
            //        TeacherId = (int)cbTeacher.SelectedValue,
            //        Month = dpMonth.SelectedDate?.ToString("yyyy-MM") ?? "",
            //        Amount = decimal.Parse(txtAmount.Text),
            //        Notes = txtNotes.Text
            //    };

            //    await _salaryLogic.AddSalaryAsync(salary);
            //    await LoadSalaries();
            //    MessageBox.Show("تمت إضافة الراتب بنجاح.", "نجاح", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"حدث خطأ أثناء الإضافة: {ex.Message}", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }

        private async void EditSalary_Click(object sender, RoutedEventArgs e)
        {
            //if (SalariesDataGrid.SelectedItem is SalaryModel selected)
            //{
            //    selected.Amount = decimal.Parse(txtAmount.Text);
            //    selected.Notes = txtNotes.Text;

            //    await _salaryLogic.UpdateSalaryAsync(selected);
            //    await LoadSalaries();
            //    MessageBox.Show("تم تعديل الراتب بنجاح.", "نجاح", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //else
            //{
            //    MessageBox.Show("الرجاء تحديد صف للتعديل.", "تنبيه", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}
        }

        private async void DeleteSalary_Click(object sender, RoutedEventArgs e)
        {
            if (SalariesDataGrid.SelectedItem is SalaryModel selected)
            {
                if (MessageBox.Show("هل أنت متأكد من حذف هذا الراتب؟", "تأكيد", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    await _salaryLogic.DeleteSalaryAsync(selected.Id);
                    await LoadSalaries();
                    MessageBox.Show("تم حذف الراتب.", "تم", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("الرجاء تحديد صف للحذف.", "تنبيه", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            await LoadSalaries();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dpBirthDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EmployeeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }


        private async void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private async Task LoaddgClassSections()
        {
            SalariesDataGrid.AutoGenerateColumns = true;
           // SalariesDataGrid.ItemsSource = await _studentServices.GetAllStudents();
            await Dispatcher.InvokeAsync(() =>
            {   
                //"الموظف" 
                //"الراتب الأساسي" 
                //"المكافأة"
                //"الخصم" 
                //"الصافي"
                //"الشهر"
                //"السنة"
                //"مدفوع"
                //"تاريخ الدفع"
                
                if (SalariesDataGrid.Columns.Count != -1)
                {
                    SalariesDataGrid.Columns[0].Header = "الرقم";
                    SalariesDataGrid.Columns[1].Header = "الاسم الكامل";
                    SalariesDataGrid.Columns[2].Header = "المرحلة";
                    SalariesDataGrid.Columns[4].Header = "الجنس";
                    SalariesDataGrid.Columns[3].Header = "الصف الدراسي";
                    SalariesDataGrid.Columns[5].Header = "الشعبة";
                    SalariesDataGrid.Columns[6].Header = "الرقم ولي الأمر";

                    SalariesDataGrid.Columns[0].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    SalariesDataGrid.Columns[1].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    SalariesDataGrid.Columns[2].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    SalariesDataGrid.Columns[3].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    SalariesDataGrid.Columns[4].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    SalariesDataGrid.Columns[5].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                    SalariesDataGrid.Columns[6].Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                }
            });
        }

        private async void SalariesDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
          await  LoaddgClassSections();
        }

        private void SalariesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
