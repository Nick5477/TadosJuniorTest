using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace WebApp.Structures
{
    public struct ErrorObject
    {
        public string Error { get; set; }

        public ErrorObject(string message)
        {
            Error = message;
        }
    }
}
