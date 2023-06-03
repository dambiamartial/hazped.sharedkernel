using Twilio;
using Twilio.Exceptions;
using Twilio.Rest.Api.V2010.Account;

namespace hazped.sharedkernel.Common;

public static class TwilioTextMessageSender
{
    public static async Task<bool> SendMessage(ILogger logger, string to, string body, string sid, string authToken, string number)
    {
        TwilioClient.Init(sid, authToken);

        bool isSucceed = false;

        try
        {
            var message = await MessageResource.CreateAsync(
            body: body,
            from: new Twilio.Types.PhoneNumber(number),
            to: new Twilio.Types.PhoneNumber(to)
            );
            isSucceed = true;
            logger.LogInformation($"Message sent to {to}");
        }
        catch (ApiException e)
        {
            logger.LogError($"Error sending message to {to} : Error {e.Code} - {e.MoreInfo}");
        }
        finally { }

        return isSucceed;
    }
}