using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CourceProject.Utility {
  public class EmailService {
    public async Task SendEmailAsync(string email, string subject, string message) {
      var emailMessage = new MimeMessage();

      emailMessage.From.Add(new MailboxAddress("Администрация сайта", "admin@metanit.com"));
      emailMessage.To.Add(new MailboxAddress("", email));
      emailMessage.Subject = subject;
      emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) {
        Text = message
      };

      using(var client = new SmtpClient()) {
        await client.ConnectAsync("smtp.gmail.com", 587, false);
        await client.AuthenticateAsync("emailapp320@gmail.com", "9_zhniunya");
        await client.SendAsync(emailMessage);

        await client.DisconnectAsync(true);
      }
    }
  }
}
