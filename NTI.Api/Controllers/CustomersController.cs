using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NTI.Application.InputModels.Customers;
using NTI.Application.Interfaces.Services;

namespace NTI.Api.Controllers
{
    /// <summary>
    /// Customers controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _customersService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="customersService"></param>
        public CustomersController(ICustomersService customersService)
        {
            _customersService = customersService;
        }

        /// <summary>
        /// Gets all the customers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customersService.GetAllAsync();
            if (result.Succeeded)
            {
                return Ok(result.Payload);
            }
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Gets a customer by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _customersService.GetByIdAsync(id);
            if (result.Succeeded)
            {
                return Ok(result.Payload);
            }
            return BadRequest(result.Errors);
        }


        /// <summary>
        /// Creates a new customer.
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerInputModel inputModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));

            var result = await _customersService.CreateAsync(inputModel);
            if (result.Succeeded)
            {
                return Ok(result.Payload);
            }
            return BadRequest(result.Errors);
        }

        /// <summary>
        /// Updates a customer.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerInputModel inputModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));

            var result = await _customersService.UpdateAsync(id, inputModel);
            if (result.Succeeded)
            {
                return Ok(result.Payload);
            }
            return BadRequest(result.Errors);
        }


        /// <summary>
        /// Deletes a customer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _customersService.DeleteAsync(id);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result.Errors);
        }
    }
}