using System;
using System.Data;
using Microsoft.Data.SqlClient;



namespace SchoolDLL

{

    public class DBHelper : IDBHelper
    {
        public DBHelper()
        {
            ConnectionString = "Server=DESKTOP-LMLA8GF\\MSSQLSERVERNEW;Database=School;User Id=sa;Password=sa123456;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;";
                //GetConnectionString("ConnectionStrings");
        }


        private  readonly string ConnectionString;



       

        public  async Task<DataTable>  ExecuteSelectProcedure(string procedureName, SqlParameter[] parameters = null!, CancellationToken cancellationToken = default)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        await conn.OpenAsync();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        await conn.CloseAsync();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
            return dt;
        }

   

    public  async Task<object> ExecuteScalarProcedure(string procedureName, SqlParameter[] parameters = null!,CancellationToken cancellationToken = default)
    {
        object result = null!;
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(procedureName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        await conn.OpenAsync();
                        result = await cmd.ExecuteScalarAsync(cancellationToken: cancellationToken);
                        await conn.CloseAsync();
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        return result;
    }
}


}