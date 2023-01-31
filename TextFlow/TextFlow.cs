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
            this.apiKey = apiKey;
        }
        public void useKey(string apiKey)
        {
            if (apiKey == null) throw new ArgumentNullException(nameof(apiKey));
            this.apiKey = apiKey;
        }
        public async Task<TextFlowSendMessageResult> sendSMS(string recipient, string text)
        {
            if (recipient == null || recipient.Length == 0) return new TextFlowSendMessageResult(false, 400, "You have not specified the recipient. ", new TextFlowSendMessageData());
            if (text == null || text.Length == 0) return new TextFlowSendMessageResult(false, 400, "You have not specified the message body. ", new TextFlowSendMessageData());
            if (apiKey == null || apiKey.Length == 0) return new TextFlowSendMessageResult(false, 400, "You have not specified the API key. Specify it by calling the useKey function. ", new TextFlowSendMessageData());
            var data = new Dictionary<string, string>()
            {
                {"recipient", recipient },
                {"text", text },
                {"apiKey", apiKey }
            };
            var content = new FormUrlEncodedContent(data);
            var response = await httpClient.PostAsync("https://textflow.me/messages/send", content);
            var responseString = await response.Content.ReadAsStringAsync();

            return new TextFlowSendMessageResult(responseString);
        }
    }
}