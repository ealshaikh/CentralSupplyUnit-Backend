using CSU_Core.Models;
using CSU_Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSU_Core.Service
{
    public interface I_ItemService
    {
        Task<IEnumerable<Item>> GetAllItems();
        Task CreateItem(Item item);
        Task UpdateItem(Item item);
        Task DeleteItem(int itemId);

    }
}
