using System;

namespace Domain.Entities
{
    public class Client : IEntity
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        public string Inn { get; protected set; }

        public Client(int id,string name, string inn)
        {
            Name = name;
            Id = id;
            Inn = inn;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }
    }
}

