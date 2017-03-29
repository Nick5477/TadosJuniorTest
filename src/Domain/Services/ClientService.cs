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
        public Client AddClient(string name, string inn)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (!CheckCorrectInn(inn))
                throw new ArgumentException("Incorect INN");
            if (!VerifyInn(inn))
                throw new ArgumentException("Client with this INN already exists");
            Client client=new Client(GetNewClientId(), name, inn);
            _repository.Add(client);
            return client;
        }

        public void ChangeClientName(Client client, string newName)
        {
            if (client==null)
                throw new ArgumentNullException(nameof(client));
            if (client.Name == newName)
                return;
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
            return !VerifyId(id) ? null : _repository.All().SingleOrDefault(client => client.Id == id);
        }
        //тестить OK
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
        public int GetNewClientId()
        {
            if (!_repository.All().Any())
                return 1;
            return _repository.All().Max(client => client.Id) + 1;
        }

        public bool VerifyInn(string inn)
        {
            if (!_repository.All().Any())
                return true;
            return _repository.All().All(client => client.Inn != inn);
        }

        public bool VerifyId(int id)
        {
            if (!_repository.All().Any())
                return false;
            return _repository.All().Any(client => client.Id == id);
        }

        private bool CheckCorrectInn(string inn)
        {
            if (inn.Length != 10 && inn.Length != 12)
                return false;
            return inn.All(c => c <= '9' && c >= '0');
        }
    }
}
