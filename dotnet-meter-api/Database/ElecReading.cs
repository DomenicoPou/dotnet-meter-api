using System;
using System.Collections.Generic;

#nullable disable

namespace dotnet_meter_api.Database
{
    public partial class ElecReading
    {
        public int AccountId { get; set; }
        public DateTime Date { get; set; }
        public int? Value { get; set; }
    }
}
