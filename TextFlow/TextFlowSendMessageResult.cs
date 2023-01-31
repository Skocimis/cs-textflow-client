using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
namespace TextFlow
{
    public class TextFlowSendMessageResult
    {
        public bool Ok { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public TextFlowSendMessageData Data { get; set; }
        internal TextFlowSendMessageResult(bool ok, int status, string message, TextFlowSendMessageData data)
        {
            Ok = ok;
            Status = status;
            Message = message;
            Data = data;
        }
        public TextFlowSendMessageResult(string result)
        {
            try
            {
                var jsonReader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(result), new System.Xml.XmlDictionaryReaderQuotas());
                var root = XElement.Load(jsonReader);

                var ok = root.XPathSelectElement("//ok");
                var status = root.XPathSelectElement("//status");
                var message = root.XPathSelectElement("//message");
                var to = root.XPathSelectElement("//data/to");
                var content = root.XPathSelectElement("//data/content");
                var country_code = root.XPathSelectElement("//data/country_code");
                var price = root.XPathSelectElement("//data/price");
                var timestamp = root.XPathSelectElement("//data/timestamp");
                jsonReader.Close();
                if (ok == null || status == null || message == null)
                {
                    Ok = false;
                    Status = 500;
                    Message = "Server error. ";
                    Data = new TextFlowSendMessageData();
                    return;
                }
                Ok = bool.Parse(ok.Value.ToString());
                Status = int.Parse(status.Value);
                Message = message.Value;
                if (Ok)
                {
                    if(to == null || content == null || country_code == null || price == null || timestamp == null)
                    {
                        Ok = false;
                        Status = 500;
                        Message = "Server error. ";
                        Data = new TextFlowSendMessageData();
                        return;
                    }
                    Data = new TextFlowSendMessageData(to.Value, content.Value, country_code.Value,float.Parse(price.Value), Convert.ToInt64(timestamp.Value));
                }
                else
                {
                    Data = new TextFlowSendMessageData();
                }
                return;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Ok = false;
            Status = 500;
            Message = "Server error. ";
            Data = new TextFlowSendMessageData();

        }
    }
}
