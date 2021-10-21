using System;
using System.Collections.Generic;

namespace Core
{
    public class User
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
