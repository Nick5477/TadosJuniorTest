using System;

namespace Domain.Entities
{
    public class Bill : IEntity
    {
        public int Id { get; protected set; }
        public int ClientId { get; protected set; }
        public int Number { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? PayedAt { get; protected set; }
        public decimal Sum { get; protected set; }
        public string DisplayNumber => $"{CreatedAt.Month:00}.{CreatedAt.Year:0000}-{Number:000000}";
        public bool WasPayed => PayedAt.HasValue;
        public Bill(int id,decimal sum,int clientid, int number,DateTime createdAt)
        {
            if (sum < 0)
                throw new ArgumentException("Bill sum should be not negative!");
            Id = id;
            Sum = sum;
            ClientId = clientid;
            Number = number;
            CreatedAt=createdAt;
        }

        public void Pay(DateTime payedAt)
        {
            PayedAt = payedAt;
        }

        public string GetReverseDisplayNumber()
        {
            return $"{CreatedAt.Year:00}.{CreatedAt.Month:00}-{Number:000000}";
        }
    }
}
