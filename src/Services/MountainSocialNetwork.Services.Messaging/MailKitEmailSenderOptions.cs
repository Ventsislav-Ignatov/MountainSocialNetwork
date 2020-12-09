namespace MountainSocialNetwork.Services.Messaging
{
    using MailKit.Security;

    public class MailKitEmailSenderOptions
    {
        public MailKitEmailSenderOptions()
        {
            this.Host_SecureSocketOptions = SecureSocketOptions.Auto;
        }

        public SecureSocketOptions Host_SecureSocketOptions { get; set; }

        public string Server { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}