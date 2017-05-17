using System;
using System.Linq;

namespace Domain.Entities
{
    public class Client : IEntity
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string Inn { get; protected set; }

        public Client(int id,string name, string inn)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (!CheckCorrectInn(inn))
                throw new ArgumentException("Incorect INN");
            Name = name;
            Id = id;
            Inn = inn;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Wrong new client name");
            Name = name;
        }
        private bool CheckCorrectInn(string inn)
        {
            if (inn.Length != 10 && inn.Length != 12)
                return false;
            return inn.All(c => c <= '9' && c >= '0');
        }
    }
}

