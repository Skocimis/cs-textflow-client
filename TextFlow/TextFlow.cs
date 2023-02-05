using System.Net.Http;

namespace TextFlow
{
    public class TextFlowClient
    {
        private string apiKey;
        private static readonly HttpClient httpClient = new HttpClient();

        public TextFlowClient(string apiKey)
        {
            if (apiKey == null) throw new ArgumentNullException(nameof(apiKey));
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
            this.apiKey = apiKey;
        }
        public void useKey(string apiKey)
        {
            if (apiKey == null) throw new ArgumentNullException(nameof(apiKey));
            this.apiKey = apiKey;
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + apiKey);
        }
        public async Task<TextFlowSendMessageResult> SendSMS(string phoneNumber, string text)
        {
            if (phoneNumber == null || phoneNumber.Length == 0) return new TextFlowSendMessageResult(false, 400, "You have not specified the recipient. ", new TextFlowSendMessageData());
            if (text == null || text.Length == 0) return new TextFlowSendMessageResult(false, 400, "You have not specified the message body. ", new TextFlowSendMessageData());
            if (apiKey == null || apiKey.Length == 0) return new TextFlowSendMessageResult(false, 400, "You have not specified the API key. Specify it by calling the useKey function. ", new TextFlowSendMessageData());
            var data = new Dictionary<string, string>()
            {
                {"phone_number", phoneNumber },
                {"text", text }
            };
            var content = new FormUrlEncodedContent(data);
            var response = await httpClient.PostAsync("https://textflow.me/api/send-sms", content);
            var responseString = await response.Content.ReadAsStringAsync();
            return new TextFlowSendMessageResult(responseString);
        }
        public async Task<TextFlowVerifyPhoneResult> SendVerificationSMS(string phoneNumber, int seconds)
        {
            return await SendVerificationSMS(phoneNumber, null, seconds);
        }
        public async Task<TextFlowVerifyPhoneResult> SendVerificationSMS(string phoneNumber, string serviceName)
        {
            return await SendVerificationSMS(phoneNumber, serviceName, null);
        }
        public async Task<TextFlowVerifyPhoneResult> SendVerificationSMS(string phoneNumber)
        {
            return await SendVerificationSMS(phoneNumber, null, null);
        }
        public async Task<TextFlowVerifyPhoneResult> SendVerificationSMS(string phoneNumber, string? serviceName, int? seconds)
        {
            if (phoneNumber == null || phoneNumber.Length == 0) return new TextFlowVerifyPhoneResult(false, 400, "You have not specified the phone number to verify. ", new TextFlowVerifyPhoneData());
            if (apiKey == null || apiKey.Length == 0) return new TextFlowVerifyPhoneResult(false, 400, "You have not specified the API key. Specify it by calling the useKey function. ", new TextFlowVerifyPhoneData());
            var data = new Dictionary<string, string>()
            {
                {"phone_number", phoneNumber },
                {"test", "true" }
            };
            if (serviceName != null)
            {
                data.Add("service_name", serviceName);
            }
            if(seconds != null)
            {
                data.Add("seconds", seconds.Value.ToString());
            }
            var content = new FormUrlEncodedContent(data);
            var response = await httpClient.PostAsync("https://textflow.me/api/send-code", content);
            var responseString = await response.Content.ReadAsStringAsync();
            return new TextFlowVerifyPhoneResult(responseString);
        }

        public async Task<TextFlowVerifyCodeResult> VerifyCode(string phone_number, string code)
        {
            if (phone_number == null || phone_number.Length == 0) return new TextFlowVerifyCodeResult(false, 400, "You have not specified the recipient. ");
            if (code == null || code.Length == 0) return new TextFlowVerifyCodeResult(false, 400, "You have not specified the code. ");
            if (apiKey == null || apiKey.Length == 0) return new TextFlowVerifyCodeResult(false, 400, "You have not specified the API key. Specify it by calling the useKey function. ");
            var data = new Dictionary<string, string>()
            {
                {"phone_number", phone_number },
                {"code", code },
                {"test", "true" }
            };
            var content = new FormUrlEncodedContent(data);
            var response = await httpClient.PostAsync("https://textflow.me/api/verify-code", content);
            var responseString = await response.Content.ReadAsStringAsync();
            return new TextFlowVerifyCodeResult(responseString);
        }
    }
}