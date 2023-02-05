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
    public class TextFlowVerifyCodeResult
    {
        public bool Ok { get; }
        public int Status { get; }
        public string Message { get;  }
        public bool Valid { get; }
        public string? ValidCode { get; }
        public long? Expires { get; }
        public String Json { get; }
        internal TextFlowVerifyCodeResult(bool ok, int status, string message)
        {
            Json = "";
            Ok = ok;
            Status = status;
            Message = message;
            Valid = false;
        }
        public TextFlowVerifyCodeResult(string result)
        {
            try
            {
                Json = result.ToString();
                var jsonReader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(result), new System.Xml.XmlDictionaryReaderQuotas());
                var root = XElement.Load(jsonReader);

                var ok = root.XPathSelectElement("//ok");
                var status = root.XPathSelectElement("//status");
                var message = root.XPathSelectElement("//message");
                var valid = root.XPathSelectElement("//valid");
                var validCode = root.XPathSelectElement("//valid_code");
                var expires = root.XPathSelectElement("//expires");
                jsonReader.Close();
                if (ok == null || status == null || message == null || valid==null)
                {
                    Ok = false;
                    Status = 500;
                    Message = "Server error. ";
                    Valid = false;
                    return;
                }
                Ok = bool.Parse(ok.Value.ToString());
                Status = int.Parse(status.Value);
                Message = message.Value;
                Valid = bool.Parse(valid.Value.ToString());
                if (validCode != null)
                {
                    ValidCode = validCode.Value;
                }
                if (expires != null)
                {
                    Expires = long.Parse(expires.Value);
                }
                return;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if(Json == null)
            {
                Json = "";
            }
            Ok = false;
            Status = 500;
            Message = "Server error. ";
            Valid = false;

        }
    }
}
