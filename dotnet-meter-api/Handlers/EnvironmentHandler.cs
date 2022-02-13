using dotnet_meter_api.Constants;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_meter_api.Handlers
{
    /// <summary>
    /// Handles all of our environment needs
    /// </summary>
    public static class EnvironmentHandler
    {
        /// <summary>
        /// Obtains the environment variable, that should be set by the devs
        /// </summary>
        /// <returns>The environment string</returns>
        public static string Get()
        {
            string? value = Environment.GetEnvironmentVariable("SOFTWARE_ENV");
            if (value != null && value != "")
            {
                switch (value)
                {
                    case "PRODUCTION":
                        return EnvironmentConst.Production;

                    case "TESTING":
                        return EnvironmentConst.Testing;

                    case "SANDBOX":
                        return EnvironmentConst.SandBox;
                }
                return EnvironmentConst.Dev;
            } else
            {
                return EnvironmentConst.Dev;
            }
        }


        /// <summary>
        /// Returns true if its production
        /// </summary>
        /// <returns></returns>
        public static bool IsProduction()
        {
            return (EnvironmentHandler.Get() == EnvironmentConst.Production);
        }


        /// <summary>
        /// Return true if its testing
        /// </summary>
        /// <returns></returns>
        public static bool IsTesting()
        {
            return (EnvironmentHandler.Get() == EnvironmentConst.Testing);
        }


        /// <summary>
        /// Return true if its sandbox
        /// </summary>
        /// <returns></returns>
        public static bool IsSandBox()
        {
            return (EnvironmentHandler.Get() == EnvironmentConst.SandBox);
        }


        /// <summary>
        /// Returns true if its dev
        /// </summary>
        /// <returns></returns>
        public static bool IsDev()
        {
            return (EnvironmentHandler.Get() == EnvironmentConst.Dev);
        }


        /// <summary>
        /// Returns the correct text to show to our clients/dev on swagger
        /// </summary>
        /// <returns></returns>
        public static string GetHtmlTest()
        {
            string value = EnvironmentHandler.Get();
            switch (value)
            {
                case "PRODUCTION":
                    return "Production";

                case "TESTING":
                    return "Testing";

                case "SANDBOX":
                    return "Hello Sandbox";

                case "DEV":
                    return "Hello Developer";
            }
            throw new Exception("Unkown Environment");
        }
    }
}
