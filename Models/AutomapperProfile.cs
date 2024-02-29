using AutoMapper;
using BankManagement.Models.DTOs;

namespace BankManagement.Models
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile() {
            CreateMap<Customer, DTOCustomer>().ReverseMap();    
            CreateMap<Account, DTOAccount>().ReverseMap();
            CreateMap<Customer, DTOCustomerAdd>().ReverseMap();
            CreateMap<Account, DTOAccountAdd>().ReverseMap();
            CreateMap<Account, DTOAccountPut>().ReverseMap();
            CreateMap<Customer, DTOCustomerPut>().ReverseMap();
            CreateMap<TransactionDetail, DTOTransactionDetail>().ReverseMap();
        }
    }
}
