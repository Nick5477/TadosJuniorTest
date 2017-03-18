using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Services
{
    public interface IClientService
    {
        Client AddClient(string name, string inn);
        void ChangeClientName(Client client, string newName);
        void DeleteClient(int id);
        Client GetClientById(int id);
        List<Client> GetClients(int offset, int count);
        int GetNewClientId();
        bool VerifyInn(string inn);
        bool VerifyId(int id);
    }
}
