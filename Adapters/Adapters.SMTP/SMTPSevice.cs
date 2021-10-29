using System;
using System.Threading;
using System.Threading.Tasks;
using Adapters.SMTP.Configuration;
using Common.CQRS.Abstractions;
using Domain.Abstractions.Events;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace Adapters.SMTP
{
    public class SMTPService : IEventHandler<AnswerSentEvent>
    {
        private SMTPOptions smtpOptions;

        public SMTPService(IOptions<SMTPOptions> smtpOptions)
        {
            if(smtpOptions == null || smtpOptions.Value == null)
                throw new ArgumentNullException(nameof(smtpOptions));

            this.smtpOptions = smtpOptions.Value;

        }
        
        public async Task Handle(AnswerSentEvent @event, CancellationToken cancellationToken)
        {
            var message = CreateMessage(@event);

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpOptions.Host, smtpOptions.Port, true, cancellationToken);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                
                await client.AuthenticateAsync(smtpOptions.UserName, smtpOptions.Password);
                await client.SendAsync(message, cancellationToken);
            }
        }

        private MimeKit.MimeMessage CreateMessage(AnswerSentEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
