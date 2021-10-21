using System;

namespace Core
{
    public class Car
    {
        public Guid Id { get; set; }

        public Guid ModelId { get; set; }

        public virtual CarModel Model { get; set; }

        public Guid UserId { get; set; }

        public virtual User User { get; set; }

        public int NumDoors { get; set; }

        public string Color { get; set; }

        public DateTime Year { get; set; }
    }
}
