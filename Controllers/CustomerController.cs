using BankManagement.Models.DTOs;
using BankManagement.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private ICustomerRepo _repo;
        public CustomerController(ICustomerRepo repo) {
            _repo = repo;
        }

        [HttpGet("get_all_customers")]
        public IActionResult GetAllCustomers()
        {
            return Ok(_repo.GetAllCustomers());
        }

        [HttpGet("get_customer_by_id/{id:int}")]
        public IActionResult GetCustomerById([FromRoute] int id) {
            if (_repo.GetCustomerById(id) != null) { return Ok(_repo.GetCustomerById(id)); }
            return NotFound("Customer does not exist");
            
        }

        [HttpPost("add_customer")]
        public IActionResult AddCustomer([FromBody] DTOCustomerAdd dto) {
            if (_repo.AddCustomer(dto) == 1) {
                return Ok("Customer Added Successfully");
            }
            return BadRequest("Could not add.");
        }

        [HttpDelete("delete_customer/{id:int}")]
        public IActionResult DeleteCustomerById([FromRoute]int id)
        {
            if (_repo.DeleteCustomer(id) != 0) {
                return Ok("Deletion Done");
            }
            return NotFound("id not found");
        }

        [HttpPut("update_customer/{id:int}")]
        public IActionResult UpdateCustomer([FromRoute] int id, [FromBody] DTOCustomerPut dto)
        {
            int result = _repo.UpdateCustomer(id, dto);
            if (result == 1)
            {
                return Ok("Account Updated");
            }
            return BadRequest();
        }

    }
}
