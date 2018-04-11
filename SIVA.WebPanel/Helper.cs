using System.IO;
using System.Reflection;
using OAuth2.Client.Impl;
using SIVA.Backend.Handlers;

namespace SIVA.WebPanel
{
    public static class Helper
    {
        public static void WriteFile(this Stream s, string filename)
        {
            Assembly.GetAssembly(typeof(FileHandler)).GetManifestResourceStream(filename).CopyTo(s);
        }
    }
}