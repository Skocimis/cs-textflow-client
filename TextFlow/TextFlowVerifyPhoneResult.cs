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
    public class TextFlowVerifyPhoneResult
    {
        public bool Ok { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public TextFlowVerifyPhoneData Data { get; set; }
        public String Json { get; }
        internal TextFlowVerifyPhoneResult(bool ok, int status, string message, TextFlowVerifyPhoneData data)
        {
            Json = "";
            Ok = ok;
            Status = status;
            Message = message;
            Data = data;
        }
        public TextFlowVerifyPhoneResult(string result)
        {
            try
            {
                Json = result.ToString();
                var jsonReader = JsonReaderWriterFactory.CreateJsonReader(Encoding.UTF8.GetBytes(result), new System.Xml.XmlDictionaryReaderQuotas());
                var root = XElement.Load(jsonReader);

                var ok = root.XPathSelectElement("//ok");
                var status = root.XPathSelectElement("//status");
                var message = root.XPathSelectElement("//message");
                var verificationCode = root.XPathSelectElement("//data/verification_code");
                var expires = root.XPathSelectElement("//data/expires");
                var messageText = root.XPathSelectElement("//data/message_text");
                jsonReader.Close();
                if (ok == null || status == null || message == null)
                {
                    Ok = false;
                    Status = 500;
                    Message = "Server error. ";
                    Data = new TextFlowVerifyPhoneData();
                    return;
                }
                Ok = bool.Parse(ok.Value.ToString());
                Status = int.Parse(status.Value);
                Message = message.Value;
                if (Ok)
                {
                    if(verificationCode==null || expires==null || message==null)
                    {
                        Ok = false;
                        Status = 500;
                        Message = "Server error. ";
                        Data = new TextFlowVerifyPhoneData();
                        return;
                    }
                    Data = new TextFlowVerifyPhoneData(verificationCode.Value, long.Parse(expires.Value), messageText.Value);
                }
                else
                {
                    Data = new TextFlowVerifyPhoneData();
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
            Data = new TextFlowVerifyPhoneData();

        }
    }
}
