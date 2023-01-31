# Textflow C# client

[![NuGet](https://img.shields.io/nuget/v/TextFlow.svg)](https://www.nuget.org/packages/TextFlow)

## Installation
`dotnet add package TextFlow`

## Sample Usage

To send an SMS, you have to create an API key using the [Textflow dashboard](https://textflow.me/api). When you register an account, you automatically get an API key with one free SMS which you can send anywhere.

### Just send a message

```c#
using TextFlow;

TextFlowClient client = new TextFlowClient("YOUR_API_KEY");
client.sendSMS("+381611231234", "Dummy message text...");
```

### Handle send message request result

```c#
var res = await client.sendSMS("+381611231234", "Dummy message text...");
if (res.Ok)
    Console.WriteLine(res.Data.Timestamp);
else
    Console.WriteLine(res.Message);
```

### Example properties of the successfully sent message result

`result` is an instance of `TextFlowSendMessageResult` class.

```json
{
    "Ok": true,
    "Status": 200,
    "Message": "Message sent successfully",
    "Data": {
        "To": "+381611231234",
        "Content": "Dummy message text...",
        "CountryCode": "RS",
        "Price": 0.05,
        "Timestamp": 1674759108881
    }
}
```

### Example properties of the unsuccessfully sent message result

`result` is an instance of `TextFlowSendMessageResult` class.

```json
{
    "Ok": false,
    "Status": 404,
    "Message": "API key not found"
}
```

## Getting help

If you need help installing or using the library, please check the [FAQ](https://textflow.me) first, and contact us at [support@textflow.me](mailto://support@textflow.me) if you don't find an answer to your question.

If you've found a bug in the API, package or would like new features added, you are also free to contact us!
