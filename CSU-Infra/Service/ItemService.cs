using CSU_Core.Models;
using CSU_Core.Repository;
using CSU_Core.Service;
using CSU_Infra.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSU_Infra.Service
{
    public class ItemService : I_ItemService
    {
        private readonly I_ItemRepository _itemRepository;

        public ItemService(I_ItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task CreateItem(Item item)
        {
            await _itemRepository.CreateItem(item);
        }

        public async Task DeleteItem(int itemId)
        {
            await _itemRepository.DeleteItem(itemId);
        }



        public async Task UpdateItem(Item item)
        {
            await _itemRepository.UpdateItem(item);
        }

        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await _itemRepository.GetAllItems();
        }

        //public async Task<IEnumerable<Item>> GetAllItems()
        //{
        //    var items = await _itemRepository.GetAllItems();
        //    var warehouses = await _warehouseRepository.GetAllWarehouses();

        //    foreach (var item in items)
        //    {
        //        if (item.Warehouseid.HasValue)
        //        {
        //            // Find the warehouse with the matching ID
        //            var warehouse = warehouses.FirstOrDefault(w => w.WarehouseId == item.Warehouseid.Value);
        //            item.Warehouse = warehouse;
        //        }
        //    }

        //    return items;
        //}


    }
}
