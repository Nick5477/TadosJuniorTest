using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Domain.Repositories;

namespace Domain.Services
{
    class ClientService:IClientService
    {
        private readonly IRepository<Client> _repository;

        public ClientService(IRepository<Client> repository)
        {
            if (repository==null)
                throw new ArgumentNullException(nameof(repository));
            _repository = repository;
        }
        public Client AddClient(string name, string inn)
        {
            if (name=="")
                throw new ArgumentException("Wrong client name");
            if (!VerifyInn(inn))
                throw new ArgumentException("Client with this INN already exists");
            Client client=new Client(name,GetNewClientId(),inn);
            _repository.Add(client);
            return client;
        }

        public void ChangeClientName(Client client, string newName)
        {
            if (client==null)
                throw new ArgumentNullException(nameof(client));
            if (newName == "")
                throw new ArgumentException("Wrong new client name");
            client.ChangeName(newName);
        }

        public void DeleteClient(int id)
        {
            Client client = GetClientById(id);
            _repository.Delete(client);
        }

        public Client GetClientById(int id)
        {
            if (!VerifyId(id))
                throw new ArgumentException("No client with this id");
            return _repository.All().SingleOrDefault(x => x.Id == id);
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
            clients.Sort(delegate (Client client1, Client client2)
            { return client1.Name.CompareTo(client2.Name); });
            return clients;
        }
        public List<Bill> GetClientBills(int id, int offset, int count)
        {
            throw new NotImplementedException();
        }

        public int GetNewClientId()
        {
            return _repository.All().Max(x => x.Id) + 1;
        }

        public bool VerifyInn(string inn)
        {
            return _repository.All().All(x => x.Inn != inn);
        }

        public bool VerifyId(int id)
        {
            return _repository.All().Any(x => x.Id == id);
        }


    }
}
