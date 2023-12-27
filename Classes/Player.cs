using Newtonsoft.Json;
using System;

namespace CP4.Classes
{

    public class Player
    {

        public Guid Id { get; set; }


        public string Name { get; set; }

        public Guid TeamId { get; set; }


        public string TeamName { get; set; }
    }
}
