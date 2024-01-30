using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementDataLayer.Entities
{
    public class TopUpLimitTypeEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<TopUpLimitsEntity> TopUpLimits { get; set; }
    }
}
