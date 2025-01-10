using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using mPole.Data.Models;

public class EmailService : IEmailService
{
    private readonly EmailRequest _emailRequest;
    private readonly IServiceProvider _serviceProvider;

    public EmailService(EmailRequest emailRequest, IServiceProvider serviceProvider)
    {
        _emailRequest = emailRequest;
        _serviceProvider = serviceProvider;
    }

    public async Task SendPlainTextEmailAsync(string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_emailRequest.ServerEmail));
        email.To.Add(MailboxAddress.Parse(_emailRequest.ServerEmail));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = body
        };

        using (var smtp = new SmtpClient())
        {
            await smtp.ConnectAsync(_emailRequest.HostSmtp, _emailRequest.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_emailRequest.ServerEmail, _emailRequest.ServerPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }

    public async Task SendTemplatedEmailAsync(string subject, string body, string email)
    {
        var htmlContent = $@"
    <html>
    <body>
        <h1>{subject}</h1>
        <p>{body}</p>
        <p>Od: {email}</p>
    </body>
    </html>";

        await SendPlainTextEmailAsync(subject, htmlContent);
    }
}