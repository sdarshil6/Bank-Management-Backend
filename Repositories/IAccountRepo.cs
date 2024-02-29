
using BankManagement.Models;
using BankManagement.Models.DTOs;

namespace BankManagement.Repositories
{
    public interface IAccountRepo
    {
        List<DTOAccount> GetAllAccounts();
        DTOAccount GetAccountByAccountNumber(int accNumber);
        int AddAccount(DTOAccountAdd dto);
        int DeleteAccount(int accNumber);
        int UpdateAccount(int accNumber, DTOAccountPut dto);
        int AddMoney(int accNumber, int money);
        int WithdrawMoney(int accNumber, int money);
        List<DTOTransactionDetail> GetTransactionDetails(int accNumber);
        List<DTOAccount> GetAllAccountsByCustomerId(int customerId);
        List<InterestRate> GetInterestRates();
    }
}
