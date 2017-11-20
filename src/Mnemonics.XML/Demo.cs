using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mnemonics.XML
{
    public class MyPoco
    {
        public int Id { get; }
        public double CreatedAt { get; }
        public string Name { get; private set; }

        public MyPoco(int id, double createdAt, string name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            Id = id;
            CreatedAt = createdAt;
            Name = name;
        }     
        
        public bool UpdateName(string value)
        {
            if (value != null)
            {
                Name = value;
                return true;
            }

            return false;
        }
    }
}