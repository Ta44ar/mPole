namespace mPole.Data.Models
{
    public class EmailRequest
    {
        public string HostSmtp { get; set; } = string.Empty;
        public int Port { get; set; }
        public string ServerEmail { get; set; }
        public string ServerPassword { get; set; }
        public string HelpEmail { get; set; }



        public EmailRequest()
        {
            HostSmtp = "smtp.ethereal.email";
            Port = 587;
            ServerEmail = "patricia.fadel1@ethereal.email";
            ServerPassword = "ZAgnY2NM6GK6CT2Xfc";
        }
    }
}