using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ConfigurationData
    {
        public ConfigurationData(string connection) => ConnectionString = connection;
        public string ConnectionString { get; set; }
    }
}
