using System;

namespace IntegrationLibrary.Core
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool Deleted { get; set; }
    }
}
