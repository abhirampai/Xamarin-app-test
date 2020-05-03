using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App4.Models
{
    public class Token
    {
        [PrimaryKey,AutoIncrement]
        public int id { get; set; }
        [JsonProperty("success")]
        public string success { get; set; }
        [JsonProperty("access_token")]
        public string access_token { get; set; }
        public string error_description { get; set; }
        public Token() { }
    }
}
