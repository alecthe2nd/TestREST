using Microsoft.AspNetCore.Mvc;
using TestREST.Repositories;
using TestREST.Entities;
using TestREST.DTOs;
using TestREST;
using System;

namespace TestREST.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<ItemDTO> getItems()
        {
            var items = repository.getItems().Select(item => item.asDTO());
            return items;
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDTO> GetItem(Guid id)
        {
            var item = repository.getItem(id);
            if (item == null)
            {
                return NotFound();
            }
            return item.asDTO();
        }

        [HttpPost]
        public ActionResult<ItemDTO> CreateItem(CreateItemDTO itemDTO)
        {
            Item item = new()
            {
                Id = Guid.NewGuid(),
                Name = itemDTO.Name,
                Price = itemDTO.Price,
                Amount = itemDTO.Amount,
                CreatedDate = DateTimeOffset.UtcNow
            };
            repository.createItem(item);
            return CreatedAtAction(nameof(GetItem), new { id = item.Id }, item.asDTO());
        }

        [HttpPut("{id}")]
        public ActionResult updateItem(Guid id, UpdateItemDTO itemDTO)
        {
            var existingItem = repository.getItem(id);

            if (existingItem == null)
            {
                return NotFound();
            }

            Item updatedItem = existingItem with
            {
                Name = itemDTO.Name,
                Price = itemDTO.Price,
                Amount = itemDTO.Amount,
            };

            repository.updateItem(updatedItem);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult deleteItem(Guid id)
        {
            var existingItem = repository.getItem(id);

            if (existingItem == null)
            {
                return NotFound();
            }
            repository.deleteItem(id);
            return NoContent();
        }

        [HttpPost("filter")]
        public IEnumerable<ItemDTO> getSorted(IEnumerable<FilterDTO> filterDTOs)
        {
            var now = DateTimeOffset.UtcNow;
            var items = repository.getItems();
            foreach (FilterDTO filterDTO in filterDTOs)
            {
                if (filterDTO.Attribute=="date_created")
                {
                    //magic number used here is the conversion from hours to ticks.
                    var timeinticks = (long)(filterDTO.Comparator * 36000000000);

                    if (filterDTO.Operator == "<=")
                    {
                        items = items.Where(item => now - item.CreatedDate <= new TimeSpan(timeinticks));
                    }
                    else if (filterDTO.Operator == ">=")
                    {
                        items = items.Where(item => now - item.CreatedDate >= new TimeSpan(timeinticks));
                    }
                    else if (filterDTO.Operator == "==")
                    {
                        items = items.Where(item => now - item.CreatedDate == new TimeSpan(timeinticks));
                    }
                    else if (filterDTO.Operator == "<")
                    {
                        items = items.Where(item => now - item.CreatedDate < new TimeSpan(timeinticks));
                    }
                    else if (filterDTO.Operator == ">")
                    {
                        items = items.Where(item => now - item.CreatedDate > new TimeSpan(timeinticks));
                    }
                    else if (filterDTO.Operator == "=")
                    {
                        items = items.Where(item => now - item.CreatedDate == new TimeSpan(timeinticks));
                    }
                    else
                    {
                        return new List<ItemDTO>();
                    }
                } else if (filterDTO.Attribute=="amount")
                {
                    if (filterDTO.Operator == "<=")
                    {
                        items = items.Where(item => item.Amount <= filterDTO.Comparator);
                    }
                    else if (filterDTO.Operator == ">=")
                    {
                        items = items.Where(item => item.Amount >= filterDTO.Comparator);
                    }
                    else if (filterDTO.Operator == "==")
                    {
                        items = items.Where(item => item.Amount == filterDTO.Comparator);
                    }
                    else if (filterDTO.Operator == "<")
                    {
                        items = items.Where(item => item.Amount < filterDTO.Comparator);
                    }
                    else if (filterDTO.Operator == ">")
                    {
                        items = items.Where(item => item.Amount > filterDTO.Comparator);
                    }
                    else if (filterDTO.Operator == "=")
                    {
                        items = items.Where(item => item.Amount == filterDTO.Comparator);
                    }
                    else
                    {
                        return new List<ItemDTO>();
                    }
                } else if (filterDTO.Attribute=="price")
                {
                    if (filterDTO.Operator == "<=")
                    {
                        items = items.Where(item => item.Price <= filterDTO.Comparator);
                    }
                    else if (filterDTO.Operator == ">=")
                    {
                        items = items.Where(item => item.Price >= filterDTO.Comparator);
                    }
                    else if (filterDTO.Operator == "==")
                    {
                        items = items.Where(item => item.Price == filterDTO.Comparator);
                    }
                    else if (filterDTO.Operator == "<")
                    {
                        items = items.Where(item => item.Price < filterDTO.Comparator);
                    }
                    else if (filterDTO.Operator == ">")
                    {
                        items = items.Where(item => item.Price > filterDTO.Comparator);
                    }
                    else if (filterDTO.Operator == "=")
                    {
                        items = items.Where(item => item.Price == filterDTO.Comparator);
                    }
                    else
                    {
                        return new List<ItemDTO>();
                    }
                }

            }

            return items.Select(item => item.asDTO());
        }

    }
}