using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Survey : MonoBehaviour
{
    public GameObject popupPanel;
    public GameObject[] infoPages;
    public Toggle conditionToggle;
    public GameObject redPopup;
    public Button backButton;
    public Button continueButton;
    private int currentPage = 0;

    public TMP_InputField inputField1;
    public TMP_InputField inputField2;
    public TMP_InputField inputField3;
    public TMP_Dropdown dropdown;
    public GameObject PopupAlert;

    void Start()
    {
        // hide all pages except the first one initially
        for (int i = 1; i < infoPages.Length; i++)
        {
            infoPages[i].SetActive(false);
        }

        // hide back btn initially
        backButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
    }

    public void ShowPopup()
    {
        popupPanel.SetActive(true);
        ShowCurrentPage();
    }

    public void ClosePopup()
    {
    popupPanel.SetActive(false);
    }

        public void NextPage()
    {
        // Check the toggle state before allowing navigation to page four
        if (currentPage == 2 && !conditionToggle.isOn)
        {
            // Show the red popup
            redPopup.SetActive(true);
            return; // do not proceed if the toggle is not ON
        }

        // dide the red popup if it was visible
        redPopup.SetActive(false);

        // Validate the fields only on page 4 before proceeding
        if (currentPage == 3 && !ValidateFields())
        {
            return; // stop if val fail
        }

        // hide current page and show the next one
        infoPages[currentPage].SetActive(false);
        currentPage = (currentPage + 1) % infoPages.Length;
        ShowCurrentPage();

        backButton.gameObject.SetActive(currentPage >= 1);
        continueButton.gameObject.SetActive(currentPage != 8);
    }


    public void PrevPage()
    {
        // Hide the current page and show the previous one
        infoPages[currentPage].SetActive(false);
        currentPage = (currentPage - 1 + infoPages.Length) % infoPages.Length;
        ShowCurrentPage();

        backButton.gameObject.SetActive(currentPage >= 1);
        continueButton.gameObject.SetActive(currentPage != 8);
    }

    private void ShowCurrentPage()
    {
        infoPages[currentPage].SetActive(true);
    }

    public void GoBack()
    {
        // hide the current page and show the previous one
        infoPages[currentPage].SetActive(false);
        currentPage = (currentPage - 1 + infoPages.Length) % infoPages.Length;
        ShowCurrentPage();

        backButton.gameObject.SetActive(currentPage >= 1);
        continueButton.gameObject.SetActive(currentPage != 8);
    }
    
    public void HideRedPopup()
    {
        redPopup.SetActive(false);
    }
    public void HideEntireSurvey()
    {
        popupPanel.SetActive(false);
    }
    private bool ValidateFields()
    {
        if (string.IsNullOrEmpty(inputField1.text) || string.IsNullOrEmpty(inputField2.text) || string.IsNullOrEmpty(inputField3.text))
        {
            PopupAlert.SetActive(true);
            return false;
        }
        return true;
    }

    public void hidePopup()
    {
        PopupAlert.SetActive(false);
    }

}
