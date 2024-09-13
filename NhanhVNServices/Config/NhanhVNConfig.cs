using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhanhVNServices.Config
{
    public class NhanhVNConfig
    {
        public static string ConfigName => "NhanhVn";
        public string Version { get; set; } = string.Empty;
        public string AppId { get; set; } = string.Empty;
        public string BusinessId { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
    }
}
