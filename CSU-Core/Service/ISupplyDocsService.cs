using CSU_Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSU_Core.Service
{
    public interface ISupplyDocsService
    {
        Task<IEnumerable<Supplydocument>> GetAllSupplydocuments();

        Task CreateSupplydocument(Supplydocument supplyDocument);

        Task UpdateSupplydocument(Supplydocument supplyDocument);

        Task DeleteSupplydocument(int documentId);
    }
}
