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







    public static class MyPocoExtensions
    {
        public static async Task<string> Write(this MyPoco val, string location)
        {
            var json = JsonConvert.SerializeObject(val);

            using (var fs = new FileStream(location, FileMode.Create))
            using (var writer = new StreamWriter(fs))
            {
                await writer.WriteAsync(json);
            }
        }
    }
}