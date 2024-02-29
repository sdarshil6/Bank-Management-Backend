using AutoMapper;
using BankManagement.Models;
using BankManagement.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BankManagement.Repositories
{
    public class AccountRepo : IAccountRepo
    {
        private BankContext _context;
        private IMapper _mapper;
        public AccountRepo(BankContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public List<DTOAccount> GetAllAccounts()
        {
            var result = _context.Accounts.ToList();
            List <DTOAccount> dtoList = new List<DTOAccount>();
            for (int i = 0; i < result.Count; i++) {
                dtoList.Add(_mapper.Map<DTOAccount>(result[i]));
                DTOCustomer tempDTO = _mapper.Map<DTOCustomer>(_context.Customers.ToList().Find(c => c.CustomerId == result[i].CustomerId));
                dtoList[i].DTOCustomer = tempDTO;
            }
            return dtoList;
        }

        public DTOAccount GetAccountByAccountNumber(int accNumber)
        {
            Account acc = _context.Accounts.ToList().Find(a => a.AccountNumber == accNumber);
            if (acc != null)
            {
                DTOCustomer dtoCus = _mapper.Map<DTOCustomer>(_context.Customers.ToList().Find(c => c.CustomerId == acc.CustomerId));
                DTOAccount dto = _mapper.Map<DTOAccount>(acc);
                dto.DTOCustomer = dtoCus;
                return dto;
            }
            return null;

        }

        public int AddAccount(DTOAccountAdd dto) {
            if (dto != null) {
                
                int num = FindNumberOfAccounts((int)dto.CustomerId, dto.AccountType);
                
                if ( num != 1 )
                {
                    _context.Accounts.Add(_mapper.Map<Account>(dto));
                    _context.SaveChanges();
                    return 1;
                }
                return 0;
            }
            return 0;
        }

        public int DeleteAccount(int accNumber)
        {
            Account account = _context.Accounts.Find(accNumber);
            if (account != null)
            {
                _context.Accounts.Remove(account);
                _context.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int UpdateAccount(int accNumber,  DTOAccountPut dto) {
            Account account = _context.Accounts.Find(accNumber);
            if (account == null)
            {
                return 0;
            }
            
            _mapper.Map(dto, account);
            _context.Accounts.Update(account);
            _context.SaveChanges();
            return 1;
           
        }

        public int AddMoney(int accNumber, int money) {
            Account account = _context.Accounts.Find(accNumber);
            if (account == null || account.AccountStatus == "Frozen")
            {
                return 0;
            }
            int currentBalance = (int)account.Balance;
            int newBalance = currentBalance + money;
            account.Balance = newBalance;
            _context.Accounts.Update(account);
            _context.SaveChanges();

            TransactionDetailsRepo.AddTransactionDetails((int)account.CustomerId, account.AccountNumber, money, DateTime.Now, _context, 1);


            return 1;
        }

        public int WithdrawMoney(int accNumber, int money)
        {
            Account account = _context.Accounts.Find(accNumber);
            if (account == null || account.AccountStatus == "Frozen")
            {
                return 0;
            }  
            int currentBalance = (int)account.Balance;
            if (money > currentBalance) {
                return 0;
            }
            int newBalance = currentBalance - money;
            account.Balance = newBalance;
            _context.Accounts.Update(account);
            _context.SaveChanges();

            TransactionDetailsRepo.AddTransactionDetails((int)account.CustomerId, account.AccountNumber, money, DateTime.Now, _context, 0);

            return 1;
        }

        public List<DTOTransactionDetail> GetTransactionDetails(int accNumber) {
            List<TransactionDetail> list = _context.TransactionDetails.ToList().FindAll(t => t.AccountNumber == accNumber);
            List<DTOTransactionDetail> details = new List<DTOTransactionDetail>();
            if (list != null) {
                for (int i = 0; i < list.Count; i++) {
                    details.Add(_mapper.Map<DTOTransactionDetail>(list[i]));
                } 
            }
            return details;
            
        }

        public List<DTOAccount> GetAllAccountsByCustomerId(int customerId) { 
            List<Account> list = _context.Accounts.ToList().FindAll( a => a.CustomerId == customerId);
            if (list != null)
            {
                List<DTOAccount> dtoList = new List<DTOAccount>();
                dtoList = _mapper.Map<List<DTOAccount>>(list);
                return dtoList;
            }
            return null;
            
        }

        public List<InterestRate> GetInterestRates() { 
            List<InterestRate> l = _context.InterestRates.ToList();
            if(l != null)
            {
                return l;
            }
            return null;
        }

        private int FindNumberOfAccounts(int customerID, string accountType) {

            var param = new SqlParameter[] {

                new SqlParameter(){
                    ParameterName = "@CustomerID",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = customerID,
                },

                new SqlParameter(){
                    ParameterName = "@Type",
                    SqlDbType= System.Data.SqlDbType.VarChar,
                    Size = 25,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = accountType,
                },

                new SqlParameter(){
                    ParameterName = "@num_accounts",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                }

            };

            var query = $"EXEC CheckTotalNumberOfAccounts @CustomerID, @Type, @num_accounts OUT";
            _context.Database.ExecuteSqlRaw(query, param.ToArray());
            var output = int.Parse(param[2].Value.ToString());
            return output;
        }

        
    }
}
