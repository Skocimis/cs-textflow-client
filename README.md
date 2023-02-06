# Textflow C# client

[![NuGet](https://img.shields.io/nuget/v/TextFlow.svg)](https://www.nuget.org/packages/TextFlow)

## Installation
`dotnet add package TextFlow`

## Sending an SMS

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

## Verifying a phone number

You can also use our service to easily verify a phone number, without storing data about the phones that you are about to verify, because we can do it for you.

### Example usage

```c#
//User has sent his phone number for verification
var resultSMS = await client.SendVerificationSMS ("+11234567890", SERVICE_NAME, SECONDS);

//Show him the code submission form
//We will handle the verification code ourselves

//The user has submitted the code
var resultCode = await client.VerifyCode("+11234567890", "USER_ENTERED_CODE");
//if `resultCode.Valid` is true, then the phone number is verified. 
```

#### Verification options

`SERVICE_NAME` is what the user will see in the verification message, e. g. `"Your verification code for Guest is: CODE"`

`SECONDS` is how many seconds the code is valid. Default is 10 minutes. Maximum is one day. 

## Getting help

If you need help installing or using the library, please check the [FAQ](https://textflow.me) first, and contact us at [support@textflow.me](mailto://support@textflow.me) if you don't find an answer to your question.

If you've found a bug in the API, package or would like new features added, you are also free to contact us!
