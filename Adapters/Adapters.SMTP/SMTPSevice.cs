using Domain.Abstractions.Models;

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
        var message = Map(command.Message);

        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(smtpOptions.Host, smtpOptions.Port, true, cancellationToken);
            client.AuthenticationMechanisms.Remove("XOAUTH2");

            await client.AuthenticateAsync(smtpOptions.UserName, smtpOptions.Password, cancellationToken);
            await client.SendAsync(message, cancellationToken);
        }
    }

    private MimeKit.MimeMessage Map(Message message)
    {
        var mimeKitMessage = new MimeKit.MimeMessage();

        mimeKitMessage.From.Add(Map(message.From));
        mimeKitMessage.To.Add(Map(message.To));
        mimeKitMessage.Subject = message.Subject;
        mimeKitMessage.Body = Map(message.Body);

        return mimeKitMessage;
    }

    private MimeKit.InternetAddress Map(Recipient recipient)
    {
        return new MimeKit.MailboxAddress(System.Text.Encoding.UTF8, recipient.Name, recipient.EmailAddress);
    }

    private MimeKit.MimeEntity Map(string body)
    {
        var builder = new MimeKit.BodyBuilder();
        builder.HtmlBody = body;
        return builder.ToMessageBody();
    }
}
