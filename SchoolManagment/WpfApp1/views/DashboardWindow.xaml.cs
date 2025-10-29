using School.views.Pages.UserControls;
using School.views.UserControls;
using SchoolBLL.IServices;
using School.views.Pages;
using SchoolBLL.Services;
using System.Windows;
using SchoolDLL;
using BLL.IServices;
using BLL.Services;


namespace School.views
{
    /// <summary>
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow : Window
    {
        public DashboardWindow(IExamService examService,ISalaryServices salaryServices , IFeesServices feeServices,ITeacherService teacherService, IStudentServices studentServices,ISubjectServices subjectServices ,ISectionServices sectionServices, IClassServices classServices, IShareServices shareservices , IYearServices yearServices, ISemesterServices semesterServices , ILevelServices levelServices)
        {
            InitializeComponent();
            MainFrame.Content = new HomePage();
            _yearServices = yearServices;
            _feeServices = feeServices;
            _levelServices = levelServices;
            _semesterServices = semesterServices;
            _shareServices = shareservices;
            _classServices = classServices;
            _sectionServices = sectionServices;
            _subjectServices = subjectServices;
            _studentServices = studentServices;
            _teacherService = teacherService;
            _salaryServices = salaryServices;
            _examService = examService;
        }

        private readonly ISalaryServices _salaryServices;
        private readonly ITeacherService _teacherService;
        private readonly IStudentServices _studentServices;
        private readonly IShareServices _shareServices;
        private IClassServices _classServices;
        private readonly IYearServices _yearServices;
        private readonly ILevelServices _levelServices;
        private readonly ISemesterServices _semesterServices;
        private readonly ISubjectServices _subjectServices;
        private readonly IFeesServices _feeServices;
        private readonly IExamService _examService;
        private readonly ISectionServices _sectionServices;

        private void Home_Click(object sender, RoutedEventArgs e) =>
            MainFrame.Content = new HomePage();
        private void Students_Click(object sender, RoutedEventArgs e) => 
            MainFrame.Content = new StudentsPage(_studentServices, _shareServices);
        private void Teachers_Click(object sender, RoutedEventArgs e) => 
            MainFrame.Content = new TeachersPage(_teacherService,_shareServices);
        private void Employees_Click(object sender, RoutedEventArgs e) =>
            MainFrame.Content = new EmployeesPage();
        private void Classes_Click(object sender, RoutedEventArgs e) 
           => MainFrame.Content = new ClassesPage(_yearServices);
        private void Fees_Click(object sender, RoutedEventArgs e) =>
            MainFrame.Content = new FeesPage(_feeServices);
        private void Reports_Click(object sender, RoutedEventArgs e) => 
            MainFrame.Content = new ReportsPage();

        private void Exam_Click(object sender, RoutedEventArgs e) =>
            MainFrame.Content = new ExamsPage(_examService, _shareServices);    
        private void Button_Click(object sender, RoutedEventArgs e)
        => MainFrame.Content = new ClassePage(_semesterServices, _yearServices);

        private void Button_Click_1(object sender, RoutedEventArgs e)=>
            MainFrame.Content = new AcademicLevelsPage(_yearServices, _semesterServices,_levelServices);

        private void Button_Click_2(object sender, RoutedEventArgs e)
       => MainFrame.Content = new AcademicClassesPage(_shareServices , _classServices);

        private void Button_Click_3(object sender, RoutedEventArgs e)
       => MainFrame.Content = new ClassSectionsPage(_shareServices, _sectionServices);

        private void Subject_Click(object sender, RoutedEventArgs e)
      => MainFrame.Content = new SubjectsPage(_shareServices, _subjectServices);
        private void Salary_Click(object sender, RoutedEventArgs e)
      => MainFrame.Content = new SalariesPage(_salaryServices);
    }

}
