using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Learning.Resource.Domain.Entities
{
    public class Configuration
    {
        public required Guid ConfigurationId { get; set; }
        public required string ConfigurationName { get; set; }
        public required string ConfigurationValue { get; set; }
    }
}
