using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp8
{
    public class RadioBand
    {
        public RadioBand(int id, string code, string description = null)
        {
            Id = id;
            Code = code;
            Description = description;
        }

        public int Id { get; }

        public string Code { get; }

        public string Description { get; set; }
    }
}
