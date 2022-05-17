using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Contracts;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Common;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IRepository<Item> itemsRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public ItemsController(IRepository<Item> itemsRepository, IPublishEndpoint publishEndpoint)
        {
            this.itemsRepository = itemsRepository;
            this.publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetAsync()
        {
            var items = (await itemsRepository.GetAllAsync()).Select(item => item.AsDto());
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
        {
            var item =  await itemsRepository.GetAsync(id);

            if (item is null)
                return NotFound();

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto createItemDto)
        {
            var item = new Item
            {
                Name = createItemDto.Name,
                Description = createItemDto.Description,
                Price = createItemDto.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await itemsRepository.CreateAsync(item);

            await publishEndpoint.Publish(new CatalogItemCreated(item.Id, item.Name, item.Description));

            return CreatedAtAction(nameof(GetByIdAsync), new { id = item.Id}, item);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Put(Guid id, UpdateItemDto updateItemDto)
        {
            var itemToUpdate = await itemsRepository.GetAsync(id);
            
            if (itemToUpdate is null)
                return NotFound();

            itemToUpdate.Name = updateItemDto.Name;
            itemToUpdate.Description = updateItemDto.Description;
            itemToUpdate.Price = updateItemDto.Price;

            await itemsRepository.UpdateAsync(itemToUpdate);

            await publishEndpoint.Publish(new CatalogItemUpdated(itemToUpdate.Id, itemToUpdate.Name, itemToUpdate.Description));

            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var itemToDelete = await itemsRepository.GetAsync(id);

            if (itemToDelete is null)
                return NotFound();

            await itemsRepository.RemoveAsync(itemToDelete.Id);

            await publishEndpoint.Publish(new CatalogItemDeleted(itemToDelete.Id));

            return NoContent();
        }
    }
}