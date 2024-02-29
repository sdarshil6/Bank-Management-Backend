using BankManagement.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BankManagement.Repositories
{
    public class TransactionDetailsRepo : ITransactionDetailsRepo
    {

        

        public static void AddTransactionDetails(int customerID, int accNumber, int amount, DateTime dateAdded, BankContext _context, int transactionType)
        {
            var param = new SqlParameter[] {
 

                new SqlParameter()
                {
                    ParameterName = "@AccountNumber",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = accNumber,
                },

                new SqlParameter(){
                    ParameterName = "@Amount",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = amount,
                },

                new SqlParameter()
                {
                    ParameterName = "@DateAdded",
                    SqlDbType = System.Data.SqlDbType.DateTime,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = dateAdded,
                },

                new SqlParameter()
                {
                    ParameterName = "@TransactionType",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = transactionType,
                }

            };

            var query = $"EXEC AddTransactionDetails @AccountNumber, @Amount, @DateAdded, @TransactionType";
            _context.Database.ExecuteSqlRaw(query, param.ToArray());
            
        }
    }
}
