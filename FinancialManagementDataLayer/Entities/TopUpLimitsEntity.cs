using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManagementDataLayer.Entities
{
    public class TopUpLimitsEntity
    {
        public int Id { get; set; }
        public int TopUpLimit { get; set; }
        public int TopUpLimitTypeId { get; set; }
        public TopUpLimitTypeEntity TopUpLimitType { get; set; }
    }
}
