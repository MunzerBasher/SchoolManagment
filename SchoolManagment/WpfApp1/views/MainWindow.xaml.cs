using BLL.IServices;
using BLL.Services;
using Microsoft.Extensions.DependencyInjection;
using School.views;
using SchoolBLL.IServices;
using SchoolBLL.Services;
using SchoolDLL;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow(IExamService examService,ISalaryServices salaryServices ,IFeesServices feeServices,ITeacherService teacherService,IStudentServices studentServices,ISubjectServices subjectServices, ISectionServices sectionServices, IClassServices classServices ,IYearServices yearServices , ISemesterServices semesterServices, ILevelServices levelServices,IShareServices shareServices)
        {
            InitializeComponent();
            _semesterServices = semesterServices;
            _sectionServices = sectionServices;
            _subjectServices = subjectServices;
            _levelServices = levelServices;
            _shareServices = shareServices;
            _classServices = classServices;
            _yearService = yearServices;
            _studentServices = studentServices;
            _teacherService = teacherService;
            _feeServices = feeServices;
            _salaryServices = salaryServices;
            _examService = examService;
        }

        private readonly ISalaryServices _salaryServices;
        private readonly IFeesServices _feeServices;
        private readonly ITeacherService _teacherService;
        private readonly IStudentServices _studentServices;
        private readonly IShareServices _shareServices;
        private readonly IYearServices _yearService;
        private readonly ISemesterServices _semesterServices;
        private readonly ILevelServices _levelServices;
        private readonly IClassServices _classServices;
        private readonly ISectionServices _sectionServices;
        private readonly ISubjectServices _subjectServices;
        private readonly IExamService _examService;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dashboard = new DashboardWindow(_examService,_salaryServices, _feeServices, _teacherService, _studentServices, _subjectServices, _sectionServices,_classServices, _shareServices, _yearService, _semesterServices, _levelServices);
            dashboard.Show();

            
            Application.Current.Windows[0].Close();
        }
    }
}