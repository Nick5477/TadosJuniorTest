﻿using System;

namespace Domain.Entities
{
    public class Bill : IEntity
    {
        public int Id { get; protected set; }
        public decimal Sum { get; protected set; }
        public int ClientId { get; protected set; }
        public int Number { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? PayedAt { get; protected set; }
        public string DisplayNumber => $"{CreatedAt.Month:00}.{CreatedAt.Year:0000}-{Number:000000}";

        public Bill(int id,decimal sum,int clientid, int number,DateTime createdAt)
        {
            Sum = sum;
            ClientId = clientid;
            Number = number;
            CreatedAt=createdAt;
        }

        public void Pay(DateTime payedAt)
        {
            PayedAt = payedAt;
        }
    }
}
