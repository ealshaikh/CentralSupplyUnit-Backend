namespace CSU_Infra.Repository
{
    using CSU_Core.Common;
    using CSU_Core.Models;
    using CSU_Core.Repository;
    using Dapper;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    public class SupplyDocumentRepository : ISupplyDocsRepository
    {
        private readonly IDbContext _dbContext;

        public SupplyDocumentRepository(IDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task CreateSupplydocument(Supplydocument supplyDocument)
        {
            var p = new DynamicParameters();
            p.Add("p_documentname", supplyDocument.Documentname, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_documentsubject", supplyDocument.Documentsubject, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_createdby", supplyDocument.Createdby, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Input);
            p.Add("p_warehouseid", supplyDocument.Warehouseid, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Input);
            p.Add("p_itemid", supplyDocument.Itemid, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("supplydocument_package.create_supplydocument", p, commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteSupplydocument(int documentId)
        {
            var p = new DynamicParameters();
            p.Add("p_supplydocumentid", documentId, dbType: DbType.Int32, direction: ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("supplydocument_package.delete_supplydocument", p, commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Supplydocument>> GetAllSupplydocuments()
        {
            var items = await _dbContext.Connection.QueryAsync<Supplydocument>("supplydocument_package.getallsupplydocument", commandType: CommandType.StoredProcedure);
            return items;
        }

        public async Task UpdateSupplydocument(Supplydocument supplyDocument)
        {
            var p = new DynamicParameters();
            p.Add("p_supplydocumentid", supplyDocument.Documentname, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Input);
            p.Add("p_documentname", supplyDocument.Documentname, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_documentsubject", supplyDocument.Documentsubject, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            p.Add("p_createdby", supplyDocument.Createdby, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Input);
            p.Add("p_warehouseid", supplyDocument.Warehouseid, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Input);
            p.Add("p_itemid", supplyDocument.Itemid, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Input);
            p.Add("p_status", supplyDocument.Status, dbType: System.Data.DbType.String, direction: System.Data.ParameterDirection.Input);
            await _dbContext.Connection.ExecuteAsync("supplydocument_package.update_supplydocument", p, commandType: CommandType.StoredProcedure);
        }
    }
}
