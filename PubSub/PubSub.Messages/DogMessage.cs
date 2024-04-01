using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubSub.Messages
{
    public class DogOwner
    {
        public string Name { get; set; }

        public string Description { get; set; }
        public string Address { get; set; }
    }
    public class DogMessage 
    {
        public string Name { get; }
        public string Description { get; }
        public DogOwner Owner { get; set; } = new DogOwner();

        public DogMessage(string name)
        {
            Name = name;
            Description = "Some Mr. Rando";
            Owner.Name = Description;
            Owner.Description = Description;
            Owner.Address = "Rando St. Somewhere Ontario";
        }
    }
}
