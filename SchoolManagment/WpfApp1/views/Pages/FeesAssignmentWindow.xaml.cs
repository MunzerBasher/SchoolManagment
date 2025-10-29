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
    /// Interaction logic for FeesAssignmentWindow.xaml
    /// </summary>
    public partial class FeesAssignmentWindow : Window
    {
        public FeesAssignmentWindow()
        {
            InitializeComponent();
        }




        private void LoadFeesData()
        {
            // بيانات تجريبية (هتستبدلها من قاعدة البيانات لاحقًا)
            var fees = new List<FeeDisplayModel>
            {
                new FeeDisplayModel { AcademicYear = "2024-2025", ClassName = "الأول الثانوي", Amount = 2500 },
                new FeeDisplayModel { AcademicYear = "2024-2025", ClassName = "الثاني الثانوي", Amount = 3000 },
                new FeeDisplayModel { AcademicYear = "2024-2025", ClassName = "الثالث الثانوي", Amount = 3500 },
            };

            FeesDataGrid.ItemsSource = fees;
        }

        private void AddFee_Click(object sender, RoutedEventArgs e)
        {
            //var addWindow = new AddTuitionFeeWindow();
            //addWindow.Owner = this;
            //addWindow.ShowDialog();

            //if (addWindow.IsSaved)
            //{
            //    // بعد الحفظ أعد تحميل الجدول
            //    LoadFeesData();
            //}
        }

        private void EditFee_Click(object sender, RoutedEventArgs e)
        {
            if (FeesDataGrid.SelectedItem is FeeDisplayModel selected)
            {
                MessageBox.Show($"تعديل: {selected.ClassName}");
            }
        }

        private void DeleteFee_Click(object sender, RoutedEventArgs e)
        {
            if (FeesDataGrid.SelectedItem is FeeDisplayModel selected)
            {
                MessageBox.Show($"حذف: {selected.ClassName}");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveFee_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    public class FeeDisplayModel
    {
        public string AcademicYear { get; set; }
        public string ClassName { get; set; }
        public decimal Amount { get; set; }
    }

}
