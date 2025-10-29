using DAL.Entities;
using DAL.IRepositories;
using Microsoft.Data.SqlClient;
using System.Data;

public class SalaryRepository : ISalaryRepository
{
    private readonly SqlConnection _connection;

    public SalaryRepository()
    {
        _connection = new SqlConnection("Server=DESKTOP-LMLA8GF\\MSSQLSERVERNEW;Database=School;User Id=sa;Password=sa123456;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;");
    }

    public async Task<List<SalaryModel>> GetAllSalariesAsync()
    {
        var list = new List<SalaryModel>();
        using (SqlCommand cmd = new SqlCommand("GetAllSalaries", _connection))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            await _connection.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                list.Add(new SalaryModel
                {
                    Id = (int)reader["Id"],
                    EmployeeName = reader["EmployeeName"].ToString()!,
                    BasicSalary = (decimal)reader["BasicSalary"],
                    Bonus = (decimal)reader["Bonus"],
                    Deductions = (decimal)reader["Deductions"],
                    Month = (int)reader["Month"],
                    Year = (int)reader["Year"],
                    IsPaid = (bool)reader["IsPaid"],
                    PaymentDate = reader["PaymentDate"] as DateTime?,
                    Notes = reader["Notes"]?.ToString()!
                });
            }

            await _connection.CloseAsync();
        }
        return list;
    }

    public async Task AddSalaryAsync(SalaryModel model)
    {
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
            var rows = await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }
    }

    public async Task UpdateSalaryAsync(SalaryModel model)
    {
        using (SqlCommand cmd = new SqlCommand("UpdateSalary", _connection))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", model.Id);
            cmd.Parameters.AddWithValue("@BasicSalary", model.BasicSalary);
            cmd.Parameters.AddWithValue("@Bonus", model.Bonus);
            cmd.Parameters.AddWithValue("@Deductions", model.Deductions);
            cmd.Parameters.AddWithValue("@Notes", model.Notes ?? (object)DBNull.Value);

            await _connection.OpenAsync();
            var rows = await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }
    }

    public async Task DeleteSalaryAsync(int id)
    {
        using (SqlCommand cmd = new SqlCommand("DeleteSalary", _connection))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            await _connection.OpenAsync();
            var rows = await cmd.ExecuteNonQueryAsync();
            await _connection.CloseAsync();
        }
    }
}
