using Npgsql.TypeHandlers.NumericHandlers;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace INCServer
{
    public partial class UserInfo
    {
        public int Userid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime Birthday { get; set; }
        public string Imgsrc { get; set; }
        public decimal Money { get; set; }
        public virtual User User { get; set; }
    }
}
