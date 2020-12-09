namespace MountainSocialNetwork.Services.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using MailKit.Net.Smtp;
    using Microsoft.Extensions.Options;
    using MimeKit;

    public class MailKitEmailSender : IEmailSender
    {
        public MailKitEmailSender(IOptions<MailKitEmailSenderOptions> options)
        {
            this.Options = options.Value;
        }

        public MailKitEmailSenderOptions Options { get; set; }

        public async Task SendEmailAsync(string from, string fromName, string to, string subject, string htmlContent)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(fromName, from));
                message.To.Add(new MailboxAddress(this.Options.Username));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = htmlContent + " Message was sent by: " + fromName + " E-mail: " + from,
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(this.Options.Server, this.Options.Port, this.Options.Host_SecureSocketOptions);
                    await client.AuthenticateAsync(this.Options.Username, this.Options.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
