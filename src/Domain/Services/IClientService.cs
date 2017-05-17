using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Services
{
    public interface IClientService
    {
        Client AddClient(int id,string name, string inn);
        void ChangeClientName(Client client, string newName);
        bool DeleteClient(int id);
        Client GetClientById(int id);
        List<Client> GetClients(int offset, int count);
        bool VerifyId(int id);
    }
}
