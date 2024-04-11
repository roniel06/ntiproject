using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NTI.Application.InputModels.CustomerItems;
using NTI.Application.Interfaces.Services;

namespace NTI.Api.Controllers
{
    /// <summary>
    /// Customer items Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerItemsController : ControllerBase
    {
        private readonly ICustomerItemService _customerItemService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customerItemService"></param>
        public CustomerItemsController(ICustomerItemService customerItemService)
        {
            _customerItemService = customerItemService;
        }

        /// <summary>
        /// Get all customer items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Getall()
        {
            var result = await _customerItemService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Get customer item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([Required] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _customerItemService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Get customer items by customer id
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId([Required] int customerId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _customerItemService.GetByCustomerIdAsync(customerId);
            return Ok(result);
        }

        /// <summary>
        /// Create a new customer item
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerItemInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _customerItemService.CreateAsync(inputModel);
            return Ok(result);
        }

        /// <summary>
        /// Update a customer item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([Required] int id, [FromBody] CustomerItemInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _customerItemService.UpdateAsync(id, inputModel);
            return Ok(result);
        }


        /// <summary>
        /// Deletes a customer item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([Required] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _customerItemService.DeleteAsync(id);
            return Ok(result);
        }

    }
}