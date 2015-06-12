using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackendAssessment.Utilities
{
    public static class DefaultApplicationSettings
    {
        public static void ApplyToCurrentEnvironment()
        {
            ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, policyErrors) => true;
            ServicePointManager.DefaultConnectionLimit = 100;
            ServicePointManager.Expect100Continue = false;
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }
    }
}
