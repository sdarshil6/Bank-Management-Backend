using AutoMapper;
using BankManagement.Models;
using BankManagement.Models.DTOs;

namespace BankManagement.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private BankContext _context;
        private IMapper _mapper;
        public CustomerRepo(BankContext context, IMapper mapper) { 
            _context = context;
            _mapper = mapper;
        }
        public List<DTOCustomer> GetAllCustomers()
        {
            var result = _context.Customers.Select(c => _mapper.Map<DTOCustomer>(c)).ToList();
            return result;
        }

        public DTOCustomer GetCustomerById(int id) {
            Customer x = _context.Customers.ToList().Find(c => c.CustomerId == id);
            if (x == null) {
                return null;
            }
            return _mapper.Map<DTOCustomer>(x);
        }
        public int AddCustomer(DTOCustomerAdd dto)
        {
            if (dto != null)
            {
                _context.Customers.Add(_mapper.Map<Customer>(dto));
                _context.SaveChanges();
                return 1;
            }
            return 0;
        }
        public int DeleteCustomer(int id) {
            Customer c = _context.Customers.Find(id);
            
            if (c != null)
            {
                _context.Customers.Remove(c);
                _context.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int UpdateCustomer(int id, DTOCustomerPut dto)
        {
           Customer c = _context.Customers.Find(id);
            if (c == null)
            {
                return 0;
            }

            _mapper.Map(dto, c);
            _context.Customers.Update(c);
            _context.SaveChanges();
            return 1;

        }


        /*
        // If the reference of customer exists in Accounts, find the account in Accounts using the customerID and delete that corrressponding row in Accounts
        private bool CheckDeleteInAccounts(int C_Id)
        {
            Console.WriteLine("REACHED CheckDeleteInAccounts");
            CheckDeleteInTransactionDetails(C_Id);
            List<Account> a = _context.Accounts.ToList().FindAll(al => al.CustomerId == C_Id);

            if (a != null) {
                for (int i = 0; i < a.Count; i++) {
                    _context.Accounts.Remove(a[i]);
                    
                }
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        private bool CheckDeleteInTransactionDetails(int C_Id) {
            Console.WriteLine("REACHED CheckDeleteInTransactionDetails");
            List<TransactionDetail> list = _context.TransactionDetails.ToList().FindAll(l => l.CustomerId == C_Id);
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++) {
                    _context.TransactionDetails.Remove(list[i]);
                }
                _context.SaveChanges();
                return true;
            }
            return false;
        }*/

    }
}
