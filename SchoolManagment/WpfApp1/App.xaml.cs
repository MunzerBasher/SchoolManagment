using BLL.IServices;
using BLL.Services;
using DAL.IRepositories;
using DAL.Repositories;
using DLL.IRepositories;
using DLL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SchoolBLL.IServices;
using SchoolBLL.Services;
using SchoolDLL;
using SchoolDLL.IRepositories;
using SchoolDLL.Repositories;
using System.Windows;
using System.Xaml;





namespace WpfApp1
{
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<IDBHelper, DBHelper>();
            services.AddScoped<IYearRepository, YearRepository>();
            services.AddScoped<IYearServices, YearServices>();
            services.AddScoped<ISemesterRepository, SemesterRepository>();
            services.AddScoped<ISemesterServices, SemesterServices>();
            services.AddScoped<ILevelRepository, LevelRepository>();
            services.AddScoped<ILevelServices,LevelServices>();
            services.AddScoped<IShareServices ,ShareServices>();
            services.AddScoped<IClassRepository, ClassRepository>();    
            services.AddScoped<IClassServices, ClassServices>();
            services.AddScoped<ISectionRepository, SectionRepository>();
            services.AddScoped<ISectionServices, SectionServices>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ISubjectServices, SubjectServices>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IStudentServices, StudentServices>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IFeesRepository, FeesRepository>();
            services.AddScoped<IFeesServices, FeesServices>();
            services.AddScoped<ISalaryRepository, SalaryRepository>();
            services.AddScoped<ISalaryServices, SalaryServices>();
            services.AddScoped<IExamRepository, ExamRepository>();
            services.AddScoped<IExamService, ExamService>();


        }


        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }    
    }




}