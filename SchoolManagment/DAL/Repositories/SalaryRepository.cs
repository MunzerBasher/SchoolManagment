using DAL.Entities;
using DAL.IRepositories;
using Microsoft.Data.SqlClient;
using SchoolDLL;
using SchoolDLL.Entities;
using System.Data;


public class SalaryRepository : ISalaryRepository
{
    private readonly SqlConnection _connection;
    private readonly IDBHelper _dBHelper;

    public SalaryRepository() : this(new DBHelper())
    {
    }


    public SalaryRepository(IDBHelper dBHelper)
    {
        _dBHelper = dBHelper;
        _connection = new SqlConnection("Server=DESKTOP-LMLA8GF\\MSSQLSERVERNEW;Database=School;User Id=sa;Password=sa123456;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;");
    }

    private Salary MapRowToSalary(DataRow row)
    {

        return new Salary
        {
            Id = (int)row["Id"],
            EmployeeName = row["EmployeeName"].ToString()!,
            BasicSalary = (decimal)row["BasicSalary"],
            Bonus = (decimal)row["Bonus"],
            Deductions = (decimal)row["Deductions"],
            Month = (int)row["Month"],
            Year = (int)row["Year"],
        };
    }

    private Employee MapRowToEmployee(DataRow row)
    {
        return new Employee
        {
            Id = (int)row["Id"],
            Name = row["Name"].ToString()!,
        };
    }




    public async Task<List<Salary>> GetAllSalariesAsync()
    {
        var result = await _dBHelper.ExecuteSelectProcedure("GetAllSalaries");

        List<Salary> Salaries = new List<Salary>();

        foreach (DataRow row in result.Rows)
        {
            Salaries.Add(MapRowToSalary(row));
        }
        return Salaries;
    }



    public async Task<int> AddSalaryAsync(SalaryModel model)
    {
        var rows = 0;
        using (SqlCommand cmd = new SqlCommand("AddSalary", _connection))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeId", model.EmployeeId);
            cmd.Parameters.AddWithValue("@BasicSalary", model.BasicSalary);
            cmd.Parameters.AddWithValue("@Bonus", model.Bonus);
            cmd.Parameters.AddWithValue("@Deductions", model.Deductions);
            cmd.Parameters.AddWithValue("@Month", model.Month);
            cmd.Parameters.AddWithValue("@Year", model.Year);
            cmd.Parameters.AddWithValue("@Notes", model.Notes ?? (object)DBNull.Value);

            await _connection.OpenAsync();
            rows = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            await _connection.CloseAsync();
        }
        return rows;
    }

    public async Task<int> UpdateSalaryAsync(SalaryModel model)
    {
        int rows = 0;
        using (SqlCommand cmd = new SqlCommand("UpdateSalary", _connection))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", model.Id);
            cmd.Parameters.AddWithValue("@BasicSalary", model.BasicSalary);
            cmd.Parameters.AddWithValue("@Bonus", model.Bonus);
            cmd.Parameters.AddWithValue("@Deductions", model.Deductions);
            cmd.Parameters.AddWithValue("@Notes", model.Notes ?? (object)DBNull.Value);

            await _connection.OpenAsync();
            rows = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            await _connection.CloseAsync();
        }
        return rows;
    }


    public async Task<int> DeleteSalaryAsync(int id)
    {
        var rows = 0;
        using (SqlCommand cmd = new SqlCommand("DeleteSalary", _connection))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            await _connection.OpenAsync();
            rows = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            await _connection.CloseAsync();
        }
        return rows;
    }


    public async Task<int> IsExistAsync(int id)
    {
        var rows = 0;
        using (SqlCommand cmd = new SqlCommand("IsExistSalary", _connection))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            await _connection.OpenAsync();
            rows = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            await _connection.CloseAsync();
        }
        return rows;
    }

    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        var result = await _dBHelper.ExecuteSelectProcedure("Employees_SelectComb");

        List<Employee> Employees = new List<Employee>();

        foreach (DataRow row in result.Rows)
        {
            Employees.Add(MapRowToEmployee(row));
        }
        return Employees;
    }
}
