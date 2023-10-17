using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Linq;


public class Survey : MonoBehaviour
{
    private string csvFilePath = "player_pregame.csv"; 

    string[] questionAnswers = new string[3] { null, null, null };

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
    public TMP_Dropdown dropdown1;
    public TMP_Dropdown dropdown2;
    public GameObject PopupAlert;

     public ToggleGroup toggleGroup1;
     public ToggleGroup toggleGroup2;
     public ToggleGroup toggleGroup3;

    private bool isDone = false;

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
        if(!isDone)
        {
            popupPanel.SetActive(true);
            ShowCurrentPage();
        }
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
        
        SaveSelectedToggleName();

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
        isDone = true;
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

    public void SaveToCSV()
    {
        // Open or create the CSV file
        StreamWriter sw = new StreamWriter(csvFilePath, true);

        // Write header if the file is empty (assuming the columns are fixed)
        if (sw.BaseStream.Length == 0)
        {
            sw.WriteLine("Age;Gender;Education;ExperienceGames;ExperiencePuzzles;Valence;Arousal;Dominance");
        }

        // Write the data to the CSV file
        Toggle theActiveToggle = toggleGroup1.ActiveToggles().FirstOrDefault();
        string line = $"{inputField1.text};{inputField2.text};{inputField3.text};{dropdown1.value};{dropdown2.value};{questionAnswers[0]};{questionAnswers[1]};{questionAnswers[2]};";
        sw.WriteLine(line);

        // Close the file
        sw.Close();
    }
    private void SaveSelectedToggleName()
    {
        // determine active toggle group based on the current page
        ToggleGroup activeToggleGroup = currentPage == 5 ? toggleGroup1 :
                                        currentPage == 6 ? toggleGroup2 :
                                        currentPage == 7 ? toggleGroup3 : null;

        if (activeToggleGroup != null)
        {
            // Get the selected toggle from the toggle group
            Toggle selectedToggle = activeToggleGroup.ActiveToggles().FirstOrDefault();

            if (selectedToggle != null)
            {
                if (currentPage == 5)
                {
                    questionAnswers[0] = selectedToggle.gameObject.name;

                }
                else if (currentPage == 6)
                {
                    questionAnswers[1] = selectedToggle.gameObject.name;
                }
                else if (currentPage == 7)
                {
                    questionAnswers[2] = selectedToggle.gameObject.name;
                }
            }
        }
    }
}
