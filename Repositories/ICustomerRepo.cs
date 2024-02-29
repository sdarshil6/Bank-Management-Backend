using BankManagement.Models.DTOs;

namespace BankManagement.Repositories
{
    public interface ICustomerRepo
    {
        List<DTOCustomer> GetAllCustomers();
        DTOCustomer GetCustomerById(int id);
        int AddCustomer(DTOCustomerAdd dto);
        int DeleteCustomer(int id);
        int UpdateCustomer(int id, DTOCustomerPut dto);
    }
}
