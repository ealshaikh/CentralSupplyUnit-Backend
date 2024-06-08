using CSU_Core.Models;
using CSU_Core.Repository;
using CSU_Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSU_Infra.Service
{
    public class SupplyDocService : ISupplyDocsService
    {
        private readonly ISupplyDocsRepository _supplyDocs ;

        public SupplyDocService(ISupplyDocsRepository supplyDocs)
        {
            _supplyDocs = supplyDocs;
        }

        public async Task CreateSupplydocument(Supplydocument supplyDocument)
        {
            await _supplyDocs.CreateSupplydocument(supplyDocument);
        }

        public async Task DeleteSupplydocument(int documentId)
        {
            await _supplyDocs.DeleteSupplydocument(documentId);
        }

        public async Task<IEnumerable<Supplydocument>> GetAllSupplydocuments()
        {
          return  await _supplyDocs.GetAllSupplydocuments();
        }

        public async Task UpdateSupplydocument(Supplydocument supplyDocument)
        {
            await _supplyDocs.UpdateSupplydocument(supplyDocument);
        }
    }
}
