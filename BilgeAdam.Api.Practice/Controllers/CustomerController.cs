using BilgeAdam.Common.Dtos;
using BilgeAdam.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BilgeAdam.Api.Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService service;

        public CustomerController(ICustomerService Service)
        {
            service = Service;
        }

        [HttpGet("list")]
        public IActionResult GetPagedCustomers([FromQuery] int count, [FromQuery] int page)
        {
            var result = service.GetPagedCustomers(count, page);
            return Ok(result);
        }

        [HttpGet("get/{id}")]
        public IActionResult GetById([FromRoute] string id)
        {
            var result = service.GetCustomerById(id);
            if (result is null)
            {
                return BadRequest("Bu id de bir veri bulunamadı!");
            }
            return Ok(result);
        }
        [HttpPost("add")]
        public IActionResult Create([FromBody] CustomerAddDto dto)
        {
            var result = service.AddNewCustomer(dto);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
        [HttpDelete("delete/{id}")]
        public IActionResult Remove([FromRoute] string id)
        {
            var result = service.RemoveCustomer(id);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
        }
       
        [HttpPost("update/{id}")]
        public IActionResult Update([FromRoute] string id,[FromBody]CustomerAddDto dto)
        {
            var result = service.UpdateCustomer(id,dto);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
            
        }


        // https://localhost:7000/api/customer/list?count=10&page=1
        // https://localhost:7000/api/customer/get/MKCMI
        // https://localhost:7000/api/customer/add
        // https://localhost:7000/api/customer/delete/MKCMI
        // https://localhost:7000/api/customer/update/MKCMI
    }
}
