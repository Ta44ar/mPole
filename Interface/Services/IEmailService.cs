using System.Threading.Tasks;

public interface IEmailService
{
    Task SendPlainTextEmailAsync(string subject, string body);
    Task SendTemplatedEmailAsync(string subject, string body, string email);
}