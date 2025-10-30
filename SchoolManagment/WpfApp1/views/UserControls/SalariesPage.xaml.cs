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
using System.Windows.Controls.Primitives;
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
        private int selectedSalary ;
        private int selectedEmployee;

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


        private async Task LoadTeachers()
        {
            EmployeeComboBox.ItemsSource = await _salaryLogic.GetAllEmployeesAsync();
        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            await LoadSalaries();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            BasicSalaryTextBox.Text = "";
            BonusTextBox.Text = "";
            DeductionTextBox.Text  = "";

        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((dpBirthDate.SelectedDate is null) || (EmployeeComboBox.SelectedValue == null) || string.IsNullOrWhiteSpace(BasicSalaryTextBox.Text))
                {
                    MessageBox.Show("الرجاء إدخال جميع البيانات.", "تنبيه", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var salary = new SalaryModel
                {
                    Id = selectedSalary,
                    EmployeeId = selectedEmployee,
                    Month = dpBirthDate.SelectedDate.Value.Month,
                    Year = dpBirthDate.SelectedDate.Value.Year,
                    BasicSalary = decimal.Parse(BasicSalaryTextBox.Text),
                    Bonus = decimal.Parse(BonusTextBox.Text ?? "0"),
                    Notes = "",
                    Deductions = decimal.Parse(DeductionTextBox.Text ?? "0")
                };

                var value = await _salaryLogic.SaveAsync(salary);
                await LoadSalaries();
                MessageBox.Show("تمت إضافة الراتب بنجاح.", "نجاح", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"حدث خطأ أثناء الإضافة: {ex.Message}", "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dpBirthDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!dpBirthDate.SelectedDate.HasValue) return;
            
        }

        private void EmployeeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(EmployeeComboBox.SelectedValue != null)
            {
                selectedEmployee = (int)(EmployeeComboBox.SelectedValue);
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (SalariesDataGrid.SelectedItem is Salary selected)
            {
                BasicSalaryTextBox.Text = selected.BasicSalary.ToString();
                BonusTextBox.Text = selected.Bonus.ToString();
                DeductionTextBox.Text = selected.Deductions.ToString();

            }
            

        }


        private async void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var value = await _salaryLogic.DeleteSalaryAsync(selectedSalary);
            if (SalariesDataGrid.SelectedItem is Salary selected)
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

        private async Task LoadSalaries()
        {
            SalariesDataGrid.ItemsSource = await _salaryLogic.GetAllSalariesAsync();
            SalariesDataGrid.AutoGenerateColumns = true;
            
            await Dispatcher.InvokeAsync(() =>
            {   
                
                if (SalariesDataGrid.Columns.Count >= 7)
                {
                    SalariesDataGrid.Columns[0].Header = "الرقم";
                    SalariesDataGrid.Columns[1].Header = "الموظف";
                    SalariesDataGrid.Columns[2].Header = "الراتب الأساسي";
                    SalariesDataGrid.Columns[4].Header = "المكافأة";
                    SalariesDataGrid.Columns[3].Header = "الخصم";
                    SalariesDataGrid.Columns[6].Header = "الشهر";
                    SalariesDataGrid.Columns[5].Header = "السنة";

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
          await  LoadSalaries();
        }

        private void SalariesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SalariesDataGrid.SelectedItem is null) return;
            selectedSalary = (SalariesDataGrid.SelectedItem as Salary)!.Id;
          
        }
    }
}
