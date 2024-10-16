using MailKit.Net.Smtp;
using MimeKit;

public class MailHelper
{
    private static MailboxAddress? _mailbox;

    public MailHelper() => _mailbox = new MailboxAddress("PORTAL", EnvHelper._GmailUsername);

    public static async Task<bool> MailRecepientAsync(string recepient, string verificationCode) {
        
        try {

            var email = new MimeMessage();
            email.From.Add(_mailbox);
            email.To.Add(new MailboxAddress("", recepient));
            email.Subject = "Your Verification Code";
            email.Body = new TextPart("plain") {
                Text = $"Your verification code is: {verificationCode}\nThis will expire after {DateTime.UtcNow.AddHours(-4).AddMinutes(5).Add(TimeSpan.FromHours(12))}"
            };

            using (var smtp = new SmtpClient()) {
                await smtp.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(EnvHelper._GmailUsername, EnvHelper._GmailPassword);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }

            return true;

        } catch {

            return false; 
        }
    }
}
