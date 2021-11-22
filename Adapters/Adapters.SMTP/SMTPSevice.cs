namespace Adapters.SMTP;

public class SMTPService : ICommandHandler<SendMessage>
{
    private SMTPOptions smtpOptions;

    public SMTPService(IOptions<SMTPOptions> smtpOptions)
    {
        if (smtpOptions == null || smtpOptions.Value == null)
            throw new ArgumentNullException(nameof(smtpOptions));

        this.smtpOptions = smtpOptions.Value;

    }

    public async Task Handle(SendMessage command, CancellationToken cancellationToken)
    {
        var message = Map(command);

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(smtpOptions.Host, smtpOptions.Port, true, cancellationToken);
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            await client.AuthenticateAsync(smtpOptions.UserName, smtpOptions.Password);
            await client.SendAsync(message, cancellationToken);
        }
    }

    private MimeKit.MimeMessage Map(SendMessage command)
    {
        var message = new MimeKit.MimeMessage();

        throw new NotImplementedException();

        return message;
    }
}
