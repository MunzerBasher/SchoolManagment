using System.Data;
using Microsoft.Data.SqlClient;



namespace SchoolDLL
{
        
    public interface IDBHelper
    {


        Task<DataTable> ExecuteSelectProcedure(string procedureName, SqlParameter[] parameters = null!, CancellationToken cancellationToken = default);


        Task<object> ExecuteScalarProcedure(string procedureName, SqlParameter[] parameters = null!, CancellationToken cancellationToken = default);
        
    }


}