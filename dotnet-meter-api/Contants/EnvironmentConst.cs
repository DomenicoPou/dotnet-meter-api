using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_meter_api.Constants
{
    /// <summary>
    /// Definition of different environments we have
    /// </summary>
    public class EnvironmentConst
    {
        public const string Production = "PRODUCTION"; // Cloud (Users are using it)
        public const string Testing = "TESTING"; // In house (Users are testing with it)
        public const string SandBox = "SANDBOX"; // Sandbox (Devs are testing with it)
        public const string Dev = "DEV"; // Local (Devs are developing with it)
    }
}
