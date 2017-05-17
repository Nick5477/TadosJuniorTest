using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Services
{
    public class ClientService:IClientService
    {
        private readonly IRepository<Client> _repository;

        public ClientService(IRepository<Client> repository)
        {
            if (repository==null)
                throw new ArgumentNullException(nameof(repository));
            _repository = repository;
        }
        public Client AddClient(int id,string name, string inn)
        {
            Client client=new Client(id, name, inn);
            _repository.Add(client);
            return client;
        }

        public void ChangeClientName(Client client, string newName)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            if (client.Name == newName)
                return;
            client.ChangeName(newName);
        }

        public bool DeleteClient(int id)
        {
            Client client = GetClientById(id);
            if (client == null)
                return false;
            _repository.Delete(client);
            return true;
        }
        public Client GetClientById(int id)
        {
            return !VerifyId(id) ? null : _repository.All().SingleOrDefault(client => client.Id == id);
        }
        public List<Client> GetClients(int offset, int count)
        {
            if (offset < 0)
                offset = 0;
            if (count < 0)
                count = 0;
            if (count > 100)
                count = 100;
            List<Client> clients = _repository.All().SkipWhile(x=>x.Id<offset).ToList();
            clients = clients.Take(count).ToList();
            clients.Sort((client1, client2) => client1.Name.CompareTo(client2.Name));
            return clients;
        }
        public bool VerifyId(int id)
        {
            if (!_repository.All().Any())
                return false;
            return _repository.All().Any(client => client.Id == id);
        }
    }
}
