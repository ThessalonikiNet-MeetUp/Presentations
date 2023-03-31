using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Common.Services
{
	public class SmsService : ISmsService
	{
		private readonly ServicesConfiguration _config;

		public SmsService(ServicesConfiguration config)
		{
			_config= config;
		}

		public void SendAsync(string phone, string content)
		{
			// Initialize the Twilio client
			TwilioClient.Init(_config.Twilio.AccountSid, _config.Twilio.AuthToken);

			var message = MessageResource.Create(
				body: content,
				from: new PhoneNumber(_config.Twilio.FromNumber),
				to: new PhoneNumber(phone)
			);

			Console.WriteLine(message.Sid);
		}
	}
}
