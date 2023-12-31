using System.Collections;
using System.IO;
using System.Net.Mail;
using System.Net.Security;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class MenuLogicParkour : MonoBehaviour
{
    public Timer timer;
    public GameObject Player; //for removing gun on pause

    // Player Movement script
    [Tooltip("Player Movement Script")]
    [SerializeField]
    private PlayerMovement1 playerMovement;

    // CameraController script
    [Tooltip("CameraController script")]
    [SerializeField]
    private CameraController cameraController;

    // Shooting script
    [Tooltip("Shooting Script")]
    [SerializeField]
    private Shooting shooting;

    // Main camera
    [Tooltip("Main Camera")]
    [SerializeField]
    private Camera mainCamera;

    // Children of the main camera
    [Tooltip("Children of the main camera")]
    [SerializeField]
    private GameObject cameraChildren;

    // Main Menu Panel
    [Tooltip("Main Menu Panel")]
    [SerializeField]
    private GameObject mainMenu;

    // Controls Menu Panel
    [Tooltip("Controls Menu Panel")]
    [SerializeField]
    private GameObject controlsMenu;

    // Options Menu Panel
    [Tooltip("Options Menu Panel")]
    [SerializeField]
    private GameObject optionsMenu;

    [Tooltip("Game Goal Panel")]
    [SerializeField]
    private GameObject goalPanel;  

    [Tooltip("Ammo Count")]
    [SerializeField]
    private GameObject ammoCountPanel;

    [Tooltip("Pause")]
    [SerializeField]
    private GameObject pausePanel;

    [Tooltip("Crosshair")]
    [SerializeField]
    private GameObject crosshair;

    // Controls if the menu buttons can be pressed
    private bool canPress = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        cameraController.DisableCameraControl();
    }

    // Start is called before the first frame update
    public void StartButton()
    {
        StartCoroutine(LerpPosition(new Vector3(0f, 0.67f, 0.2f), Quaternion.identity, 2));
    }

    IEnumerator LerpPosition(Vector3 targetPosition, Quaternion targetRotation, float duration)
    {
        canPress = false;

        float time = 0;
        Vector3 startPosition = mainCamera.transform.localPosition;
        Quaternion startRotation = mainCamera.transform.localRotation;
        while (time < duration)
        {
            mainCamera.transform.SetLocalPositionAndRotation(Vector3.Lerp(startPosition, targetPosition, time / duration),
                Quaternion.Lerp(startRotation, targetRotation, time / duration));
            time += Time.deltaTime;
            yield return null;
        }
        mainCamera.transform.SetLocalPositionAndRotation(targetPosition, targetRotation);

        if (cameraChildren != null)
            cameraChildren.SetActive(true);

        crosshair.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameObject.SetActive(false);

        // start timer after the start button is pressed 
        if (timer != null)
        {
            timer.StartTimer();
        }
        canPress = true;
        cameraController.EnableCameraControl();
        playerMovement.EnableInput();

    }

    public void ControlsButton()
    {
        if (canPress)
        {
            mainMenu.SetActive(false);
            controlsMenu.SetActive(true);
            ammoCountPanel.SetActive(false);
            pausePanel.SetActive(false);
        }
    }

    public void OptionsButton()
    {
        if (canPress)
        {
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
            ammoCountPanel.SetActive(false);
            pausePanel.SetActive(false);
        }
    }

    public void BackButton()
    {
        controlsMenu.SetActive(false);
        optionsMenu.SetActive(false);
        goalPanel.SetActive(false);
        mainMenu.SetActive(true);   
        ammoCountPanel.SetActive(true);
        pausePanel.SetActive(true); 
        crosshair.SetActive(true);
        crosshair.SetActive(false);
         
    }

    public void GoalButton()
    {
        if (canPress)
        {
            mainMenu.SetActive(false);
            goalPanel.SetActive(true);
            ammoCountPanel.SetActive(false);
            pausePanel.SetActive(false);
        }
    }

    public void ExitButton()
    {
        if (canPress)
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
            Debug.Log("No files found. Email not sent.");
            Application.Quit();
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
        Application.Quit();
    }

    public void ResumeButton()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mainMenu.SetActive(false);
        cameraController.EnableCameraControl();
        playerMovement.EnableInput();
        crosshair.SetActive(true);
        timer.StartTimer();
        shooting.SetCanShoot(true);

        Player.SetActive(true); //Enable player
    }
}
