using System;
using System.Collections.Generic;

namespace RevStack.QuickBooksOnline.Context
{
    public class RestResponse
    {
        public Dictionary<string, string> Headers { get; set; }
        public int StatusCode { get; set; }
        public string StatusString { get; set; }
        public string Body { get; set; }
        public string ContentType { get; set; }
        public string OSessionId { get; set; }
        public long ContentLength { get; set; }
        public Exception Exception { get; set; }
    }
}
