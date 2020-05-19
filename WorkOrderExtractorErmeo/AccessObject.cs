using System;
using System.Collections.Generic;
using System.Text;

namespace WorkOrderExtractorErmeo
{
    public class AccessObject
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
        public string refresh_token { get; set; }
        public string exceptionMessage { get; set; }
    }
}
