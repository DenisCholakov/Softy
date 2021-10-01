using System;
using System.Collections.Generic;
using System.Text;

namespace BorderControl
{
    public class Robot : IIdentifyable
    {
        public Robot(string name, string id)
        {
            this.Model = name;
            this.Id = id;
        }

        public string Model { get; set; }
        public string Id { get; set; }
    }
}
