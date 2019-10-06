using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MiniGraber.Utils
{
    static class ExternalActions
    {
        public static void OpenLink(string uri)
        {
            Process.Start(uri);
        }
    }
}
