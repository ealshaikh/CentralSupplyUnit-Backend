using CSU_Core.Models;
using CSU_Core.Repository;
using CSU_Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSU_Infra.Service
{
    public class WarehouseService : IWarehouseService
    {

        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseService(IWarehouseRepository warehouseRepository)
        {
            _warehouseRepository = warehouseRepository;
        }
        public async Task CreateWarehouse(Warehouse warehouse)
        {
            await _warehouseRepository.CreateWarehouse(warehouse);
        }

        public async Task DeleteWarehouse(int warehouseId)
        {
            await _warehouseRepository.DeleteWarehouse(warehouseId);
        }

        public async Task<IEnumerable<Warehouse>> GetAllWarehouses()
        {
          return  await _warehouseRepository.GetAllWarehouses();
        }

        public async Task UpdateWarehouse(Warehouse warehouse)
        {
            await _warehouseRepository.UpdateWarehouse(warehouse);
        }
    }
}
