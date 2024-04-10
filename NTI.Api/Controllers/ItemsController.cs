using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NTI.Application.InputModels.Items;
using NTI.Application.Interfaces.Services;

namespace NTI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemsService;

        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        /// <summary>
        /// Gets all items
        /// </summary>
        /// <returns>A List of items</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _itemsService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Gets an item by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _itemsService.GetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ItemInputModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _itemsService.CreateAsync(item);
            return Ok(result);
        }


        /// <summary>
        /// Updates an existing item
        /// </summary>
        /// <param name="id">The id of the record</param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([Required] int id, [FromBody] ItemInputModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _itemsService.UpdateAsync(id, item);
            return Ok(result);
        }

        /// <summary>
        /// Gets an item by its item number
        /// </summary>
        /// <param name="itemNumber">The item number</param>
        /// <returns></returns>
        [HttpGet("ByItemNumber/{itemNumber}")]
        public async Task<IActionResult> GetByItemNumber([Required] int itemNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _itemsService.GetByItemNumberAsync(itemNumber);
            return Ok(result);
        }


        /// <summary>
        /// Deletes an item by its id
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
            var result = await _itemsService.DeleteAsync(id);
            return Ok(result);
        }
    }
}