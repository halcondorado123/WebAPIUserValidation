using Microsoft.Data.SqlClient;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessClients
{
    public class ClientsRepository : IClientsRepository
    {
        private ConfigurationData _connectionString { get; set; }

        public ClientsRepository(ConfigurationData connectionString)
        {
            _connectionString = connectionString;
        }

        protected SqlConnection DBConnection()
        {
            return new SqlConnection(_connectionString.ConnectionString);
        }

        public List<ClientME> GetClients()
        {
            throw new NotImplementedException();
        }

        public ClientME GetClient(int id)
        {
            throw new NotImplementedException();
        }

        public int CreateClient(ClientME client)
        {
            throw new NotImplementedException();
        }
        public int UpdateClient(ClientME client)
        {
            throw new NotImplementedException();
        }

        public int DeleteClient(int id)
        {
            throw new NotImplementedException();
        }
      
    }
}
