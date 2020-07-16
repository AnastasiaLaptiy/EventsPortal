﻿using System.Collections.Generic;

namespace Core.Entities
{
    public class UserRole
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
