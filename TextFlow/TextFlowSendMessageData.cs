using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFlow
{
    public class TextFlowSendMessageData
    {
        public string To { get; }
        public string Content { get; }
        public string CountryCode { get; }
        public float Price { get; }
        public Int64 Timestamp { get; }
        public TextFlowSendMessageData(string to, string content, string country_code, float price, Int64 timestamp)
        {
            To = to;
            Content = content;
            CountryCode = country_code;
            Price = price;
            Timestamp = timestamp;
        }
        public TextFlowSendMessageData()
        {
            To = "";
            Content = "";
            CountryCode = "";
            Price = 0;
            Timestamp = 0;
        }

    }
}
