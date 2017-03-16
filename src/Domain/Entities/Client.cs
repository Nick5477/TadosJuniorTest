using System;

namespace Domain.Entities
{
    public class Client : IEntity
    {
        public string Name { get; protected set; }
        public int Id { get; protected set; }
        public string Inn { get; protected set; }

        public Client(string name,int id, string inn)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (inn.Length!=10 || inn.Length!=12 && CheckDigits(inn))
                throw new ArgumentException("INN is incorrect!");
            Name = name;
            Id = id;
            Inn = inn;
        }

        public void ChangeName(string name)
        {
            Name = name;
        }

        private bool CheckDigits(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (!Char.IsDigit(str, i))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

