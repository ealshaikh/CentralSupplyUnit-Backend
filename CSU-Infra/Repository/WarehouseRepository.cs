using CSU_Core.Common;
using CSU_Core.Models;
using CSU_Core.Repository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSU_Infra.Repository
{
    public class WarehouseRepository : IWarehouseRepository
    {

        private readonly IDbContext _dbContext;

        public WarehouseRepository(IDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task CreateWarehouse(Warehouse warehouse)
        {
            var p = new DynamicParameters();
            p.Add("p_name", warehouse.Warehousename, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_description", warehouse.Warehousedescription, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_createdby", warehouse.Createdby, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("warehouses_package.create_warehouses", p, commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteWarehouse(int warehouseId)
        {
            var p = new DynamicParameters();
            p.Add("p_warehousesid", warehouseId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("warehouses_package.delete_warehouses", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Warehouse>> GetAllWarehouses()
        {
            var items = await _dbContext.Connection.QueryAsync<Warehouse>("warehouses_package.getallwarehouses", commandType: CommandType.StoredProcedure);
            return items;
        }

        public async Task UpdateWarehouse(Warehouse warehouse)
        {
            var p = new DynamicParameters();
            p.Add("p_warehousesid", warehouse.Warehousename, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Input);
            p.Add("p_name", warehouse.Warehousename, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_description", warehouse.Warehousedescription, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_createdby", warehouse.Createdby, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("warehouses_package.update_warehouses", p, commandType: CommandType.StoredProcedure);
        }
    }
}
