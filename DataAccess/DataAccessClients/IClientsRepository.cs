using ApiUserValidation.Models.Entities;
using ApiUserValidation.Models.Entities.ApiModelME;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataAccessClients
{
    public interface IClientsRepository
    {
        List<ClientME> GetClients();
        ClientME GetClientById(int id);
        ApiResponse CreateClient(ClientME client);
        ApiResponse ModifyClient(ClientME client);
        ApiResponse DeleteClient(int id);
    }
}
