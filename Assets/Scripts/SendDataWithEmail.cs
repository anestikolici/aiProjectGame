using System.Collections;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.IO;

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

        bool filesFound = false;

        Attachment file1;
        if (File.Exists("player_data.csv"))
        {
            file1 = new("player_data.csv");
            mail.Attachments.Add(file1);
            filesFound = true;
        }

        Attachment file2;
        if (File.Exists("player_pregame.csv"))
        {
            file2 = new("player_pregame.csv");
            mail.Attachments.Add(file2);
            filesFound = true;
        }

        // Check if any files were found
        if (!filesFound)
        {
            Debug.LogError("No files found. Email not sent.");
            yield break; // Exit the coroutine if no files were found
        }


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
