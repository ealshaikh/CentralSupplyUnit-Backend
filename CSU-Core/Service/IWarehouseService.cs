using CSU_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSU_Core.Service
{
    public interface IWarehouseService
    {
        Task<IEnumerable<Warehouse>> GetAllWarehouses();

        Task CreateWarehouse(Warehouse warehouse);

        Task UpdateWarehouse(Warehouse warehouse);

        Task DeleteWarehouse(int warehouseId);
    }
}
