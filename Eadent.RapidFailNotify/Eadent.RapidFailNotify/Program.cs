using Eadent.RapidFailNotify.Helpers;
using System;

namespace Eadent.RapidFailNotify
{
    class Program
    {
        static int Main(string[] args)
        {
            int exitCode = 0;

            string applicationNamePool = null;
            string eMailName = null;
            string eMailAddress = null;

            if (args.Length < 6)
            {
                exitCode = -1;
            }
            else
            {
                if (string.Equals(args[0], "-ApplicationPoolName", StringComparison.OrdinalIgnoreCase))
                {
                    applicationNamePool = args[1];
                }

                if (string.Equals(args[2], "-EMailName", StringComparison.OrdinalIgnoreCase))
                {
                    eMailName = args[3];
                }

                if (string.Equals(args[4], "-EMailAddress", StringComparison.OrdinalIgnoreCase))
                {
                    eMailAddress = args[5];
                }

                if ((applicationNamePool == null) || (eMailName == null) || (eMailAddress == null))
                {
                    exitCode = -2;
                }
                else
                {
                    DateTime utcNow = DateTime.UtcNow;

                    string htmlBody = $"The IIS Rapid Fail Protection has been invoked.<br><br>" +
                        $"Application Pool Name: <strong>{applicationNamePool}</strong><br><br>" +
                        $"Machine Name: <strong>{Environment.MachineName}</strong><br><br>" +
                        $"Date & Time (UTC): <strong>{utcNow:dddd, d-MMM-yyyy h:mm:ss tt}</strong><br><br>" +
                        $"Date & Time (Local): <strong>{utcNow.ToLocalTime():dddd, d-MMM-yyyy h:mm:ss tt}</strong>";

                    EMail.Send(eMailName, eMailAddress, $"IIS Rapid Fail Protection: {applicationNamePool}", htmlBody);
                }
            }

            return exitCode;
        }
    }
}
