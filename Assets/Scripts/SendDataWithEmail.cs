using System.Collections;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SendDataWithEmail : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(SendMail());
    }

    /// <summary>
    /// Sends the player data CSV files to an email address.
    /// </summary>
    /// <returns></returns>
    public IEnumerator SendMail()
    {
        yield return new WaitForSeconds(0f);

        MailMessage mail = new()
        {
            From = new MailAddress("aigroup2@outlook.com")
        };
        mail.To.Add("aigroup2@outlook.com");
        mail.Subject = "Player Data";
        mail.Body = "Player Data";

        Attachment file = new("player_data.csv");
        Attachment file2 = new("player_pregame.csv");
        mail.Attachments.Add(file);
        mail.Attachments.Add(file2);

        SmtpClient smtp = new("smtp.outlook.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("aigroup2@outlook.com", "@Group2!"),
            EnableSsl = true
        };
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        { return true; };
        bool emailSent = false;
        smtp.SendCompleted += (s, e) =>
        {
            emailSent = true;
        };

        smtp.SendAsync(mail, null);

        while (!emailSent)
        {
            yield return null;
        }

        Debug.Log("Email sent successfully!");
    }
}
