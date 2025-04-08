using Company.DAL.Models;
using Company.PL.Settings;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Company.PL.Helpers
{
    public class TwilioService(IOptions<TwilioSettings> _options) : ITwilioService
    {

        public MessageResource SendSms(Sms sms)
        {
            // Initilze Connection

            TwilioClient.Init(_options.Value.AccountSId,_options.Value.AuthToken);


            // bulid Message

            var message = MessageResource.Create(
                
                body:sms.Body,
                to:sms.To,
                from:new Twilio.Types.PhoneNumber(_options.Value.PhoneNumber)
            );

            // return message

            return message;

        }
    }
}
