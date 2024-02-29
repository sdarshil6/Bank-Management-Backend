using BankManagement.Models;
using BankManagement.Models.DTOs;
using BankManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private IAccountRepo _repo;
        public AccountController(IAccountRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("get_all_accounts")]
        public IActionResult GetAllAccounts()
        {
            return Ok(_repo.GetAllAccounts());
        }

        [HttpGet("get_acccount_by_accNumber/{accNumber:int}")]
        public IActionResult GetAccountByAccNumber([FromRoute] int accNumber) {
            if (_repo.GetAccountByAccountNumber(accNumber) != null) {
                return Ok(_repo.GetAccountByAccountNumber(accNumber));
            }
            return BadRequest("Account Number does not exist");

        }

        [HttpGet("get_all_transaction_details/{accNumber:int}")]
        public IActionResult GetAllTransactionDetails([FromRoute] int accNumber) {
            return Ok(_repo.GetTransactionDetails(accNumber));
        }

        [HttpPost("add_account")]
        public IActionResult AddAccount([FromBody] DTOAccountAdd dto) {
            if (_repo.AddAccount(dto) == 1)
            {
                return Ok("Account Added Successfully");
            }
            return BadRequest("Could not add.");
        }

        [HttpPut("update_account/{accNumber:int}")]
        public IActionResult UpdateAccount([FromRoute] int accNumber, [FromBody] DTOAccountPut dto) {
            int result = _repo.UpdateAccount(accNumber, dto);
            if (result == 1) {
                return Ok("Account Updated");
            }
            return BadRequest();
        }

        [HttpPut("add_money/{accNumber:int}/{money:int}")]
        public IActionResult AddMoney([FromRoute] int accNumber, [FromRoute] int money)
        {
            int result = _repo.AddMoney(accNumber, money);
            if (result == 1)
            {
                return Ok("Money Added");
            }
            return BadRequest("Either account does not exist or unacceptable money");
        }

        [HttpPut("withdraw_money/{accNumber:int}/{money:int}")]
        public IActionResult WithdrawMoney([FromRoute] int accNumber, [FromRoute] int money)
        {
            int result = _repo.WithdrawMoney(accNumber, money);
            if (result == 1)
            {
                return Ok("Money Withdraw Done");
            }
            return BadRequest("Either account does not exist or unacceptable money");
        }

        [HttpDelete("delete_account/{accNumber:int}")]
        public IActionResult DeleteAccount([FromRoute] int accNumber) {
            int result = _repo.DeleteAccount(accNumber);
            if (result == 1)
            {
                return Ok("Account Deleted");
            }
            return BadRequest();
        }

        [HttpGet("get_all_accounts_by_customerId/{customerId:int}")]
        public IActionResult GetAllAccountsByCustomerId([FromRoute] int customerId)
        {
            List<DTOAccount> l = _repo.GetAllAccountsByCustomerId(customerId);
            if (l != null) {
                return Ok(l);
            }
            return BadRequest("Customer Id does not exist");
        }

        [HttpGet("getInterestRates")]
        public IActionResult GetInterestRates() {
            List<InterestRate> l = _repo.GetInterestRates();
            if(l != null)
            {
                return Ok(l);
            }
            return BadRequest("Something bad happened");
        }

    }
}
