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