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
    <head>
        <style>
            body {{
                font-family: Arial, sans-serif;
                margin: 0;
                padding: 0;
                background-color: #f4f4f4;
            }}
            .container {{
                width: 100%;
                max-width: 600px;
                margin: 0 auto;
                background-color: #ffffff;
                padding: 20px;
                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            }}
            .header {{
                background-color: #007bff;
                color: #ffffff;
                padding: 10px 0;
                text-align: center;
            }}
            .content {{
                margin: 20px 0;
                font-size: 16px; /* Zwiêkszona czcionka */
            }}
            .footer {{
                text-align: center;
                color: #888888;
                font-size: 12px;
                margin-top: 20px;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <div class='header'>
                <h1>{subject}</h1>
            </div>
            <div class='content'>
                <p>{body}</p>
            </div>
            <div class='footer'>
                <p>Sent from: {email}</p>
                <p>&copy; {DateTime.Now.Year} mPole Studio. All rights reserved.</p>
            </div>
        </div>
    </body>
    </html>";

        await SendPlainTextEmailAsync(subject, htmlContent);
    }
}