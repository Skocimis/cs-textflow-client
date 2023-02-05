using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextFlow
{
    public class TextFlowVerifyPhoneData
    {
        public string VerificationCode { get; }
        public long Expires { get; }
        public string MessageText { get; }
        public TextFlowVerifyPhoneData(string verificationCode, long expires, string messageText)
        {
            VerificationCode = verificationCode;
            Expires = expires;
            MessageText = messageText;
        }
        public TextFlowVerifyPhoneData()
        {
            VerificationCode = "";
            Expires = 0;
            MessageText = "";
        }

    }
}
