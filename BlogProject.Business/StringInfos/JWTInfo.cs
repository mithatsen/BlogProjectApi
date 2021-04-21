using System;
using System.Collections.Generic;
using System.Text;

namespace BlogProject.Business.StringInfos
{
    public class JWTInfo
    {
        public string Issuer { get; set; } = "http://localhost:57108";
        public string Audience { get; set; } = "http://localhost:5000";
        public string SecurityKey { get; set; } = "mithatmithat1903";
        public double TokenExpiration { get; set; } = 30;


    }
}
