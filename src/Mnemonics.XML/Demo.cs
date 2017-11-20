using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mnemonics.XML
{
    public class Poco
    {
        private readonly IEnumerable<Poco> _myPocos;

        public Poco(IEnumerable<Poco> myPocos)
        {
            _myPocos = myPocos ?? throw new ArgumentNullException(nameof(myPocos));
        }

        private Task<IEnumerable<string>> _strings;

    }
}