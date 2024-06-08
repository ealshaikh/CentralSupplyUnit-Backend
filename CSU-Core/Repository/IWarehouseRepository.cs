using CSU_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSU_Core.Repository
{
    public interface IWarehouseRepository
    {

        Task<IEnumerable<Warehouse>> GetAllWarehouses();

        Task CreateWarehouse(Warehouse warehouse);

        Task UpdateWarehouse(Warehouse warehouse);

        Task DeleteWarehouse(int warehouseId);
    }
}
