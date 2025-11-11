
using ePizzahub.Applicationn.Contracts;
using ePizzahub.Applicationn.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ePizahub.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
        }

        [HttpGet("GetAllItems")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ItemResponseDto>>> Get()
        {
            var items = await _itemService.GetAllItemsAsync();

            //var response = new ApiResponseModelDto<IEnumerable<ItemResponseDto>>(true, items, "Items retrieved successfully");
            //We can use this response model for all the APIs but instead of that we will use middleware
            // so that we don't have to create response model for each and every API
            return Ok(items);
        }

        [HttpGet("{id:min(1)}")]
        public async Task<ActionResult<IEnumerable<ItemResponseDto>>> GetItemById(int id)
        {
            var items = await _itemService.GetItemByIdAsync(id);
            return Ok(items);
        }
    }
}
