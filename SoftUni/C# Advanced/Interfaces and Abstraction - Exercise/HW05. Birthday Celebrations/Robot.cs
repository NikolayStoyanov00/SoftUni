using System;
using System.Collections.Generic;
using System.Text;

namespace _05
{
    public class Robot : IId
    {
        public string Model { get; set; }

        public string Id { get; private set; }

        public Robot(string model, string id)
        {
            this.Model = model;
            this.Id = id;
        }
    }
}
