using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace Identity.Services
{
	public class EmailService : IEmailService
	{
		private readonly AuthMessageSenderOptions _options;
		private readonly ILogger<EmailService> _logger;

		public EmailService(IOptions<AuthMessageSenderOptions> optionsAccessor, ILogger<EmailService> logger)
		{
			_options = optionsAccessor.Value;
			_logger = logger;
		}

		public async Task SendAsync(string to, string subject, string body)
		{
			if (string.IsNullOrEmpty(_options.SendGridKey))
			{
				throw new Exception("Null SendGridKey");
			}

			var client = new SendGridClient(_options.SendGridKey);
			var msg = new SendGridMessage()
			{
				From = new EmailAddress("xaralaboskaripidis@gmail.com"),
				Subject = subject,
				PlainTextContent = body,
				HtmlContent = body
			};
			msg.AddTo(new EmailAddress(to));

			// Disable click tracking.
			// See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
			msg.SetClickTracking(false, false);
			var response = await client.SendEmailAsync(msg);
			_logger.LogInformation(response.IsSuccessStatusCode
								   ? $"Email to {to} queued successfully!"
								   : $"Failure Email to {to}");
		}
	}
}
