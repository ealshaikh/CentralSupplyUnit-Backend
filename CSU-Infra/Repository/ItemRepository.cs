namespace CSU_Infra.Repository
{
    using CSU_Core.Common;
    using CSU_Core.Models;
    using CSU_Core.Repository;
    using Dapper;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    public class ItemRepository : I_ItemRepository
    {
        private readonly IDbContext _dbContext;

        public ItemRepository(IDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public  Task CreateItem(Item item)
        {

            var p = new DynamicParameters();
            p.Add("p_iteme_name", item.Itemname, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_item_description", item.Itemdescription, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_quantity", item.Quantity, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Input);
            p.Add("p_warehouseid", item.Warehouseid, dbType: DbType.Int32, direction: ParameterDirection.Input);
          return   _dbContext.Connection.ExecuteAsync("item_package.create_item", p, commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteItem(int itemId)
        {
            var p = new DynamicParameters();
            p.Add("p_itemid", itemId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("item_package.delete_item", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Item>> GetAllItems()
        {
            var items = await _dbContext.Connection.QueryAsync<Item>("item_package.getall_items", commandType: CommandType.StoredProcedure);
            return items;
        }

        public async Task UpdateItem(Item item)
        {
            var p = new DynamicParameters();
            p.Add("p_itemid", item.Itemid, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Input);
            p.Add("p_iteme_name", item.Itemname, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_item_description", item.Itemdescription, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_quantity", item.Quantity, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Input);
            p.Add("p_warehouseid", item.Warehouseid, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("item_package.update_item", p, commandType: CommandType.StoredProcedure);
        }
    }
}
