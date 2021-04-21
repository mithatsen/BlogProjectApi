using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.WebApi
{
    //appsettings json ile ilgili bir class
    public class CustomData
    {
        public List<string> Names { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
    }
}
