using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    /* This script handles the main menu buttons found at the bottom of the game screen */

    /*
    /// ------ find a neater way to do this or it will get very messy -----
    private bool isPaused = false;
    public GameObject clickerButton;
    public GameObject upgradesPanel;
    public GameObject storePanel;

    public void UpgradesButtonTapped()
    {
        if (!isPaused)          // check if the game is already paused or not
        {
            ShowUpgradesPanel();
            return;
        }
        if (isPaused)
        {
            HideUpgradesPanel();
            return;
        }
    }

    public void StoreButtonTapped()
    {
        if (!isPaused)          
        {
            ShowStorePanel();
            return;
        }
        if (isPaused)
        {
            HideStorePanel();
            return;
        }
    }    

    public void ShowUpgradesPanel()        //shows upgrades panel, pauses game
    {
        Time.timeScale = 0f;
        isPaused = true;
        upgradesPanel.SetActive(true);
        clickerButton.SetActive(false);
    }

    public void HideUpgradesPanel ()           // hides upgrades panel, resumes game
    {
        Time.timeScale = 1f;
        isPaused = false;
        upgradesPanel.SetActive(false);
        clickerButton.SetActive(true);
    }

    public void ShowStorePanel()
    {
        Time.timeScale = 0f;
        isPaused = true;
        storePanel.SetActive(true);
        clickerButton.SetActive(false);
    }
    public void HideStorePanel()           // hides upgrades panel, resumes game
    {
        Time.timeScale = 1f;
        isPaused = false;
        storePanel.SetActive(false);
        clickerButton.SetActive(true);
    }
    */

    public GameObject clickerButton;

    private GameObject currentPanel;    // panel that is currently open, null = all panels closed


    public void TogglePanel(GameObject panel)
    {
        if (currentPanel == panel)  // if same button tapped twice, close panel
        {
            CloseCurrentPanel();
            return;
        }

        if (currentPanel != null)   // if different panel is open, close it first then open new one
        {
            currentPanel.SetActive(false);
        }

        OpenPanel(panel);
    }

    private void OpenPanel(GameObject panel)    // show panel, pause game
    {
        currentPanel = panel;
        panel.SetActive(true);
        clickerButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void CloseCurrentPanel()     // close panel, resume game
    {
        if (currentPanel ==  null) { return; }  // ignore function if no panel active

        currentPanel.SetActive(false);
        currentPanel = null;
        clickerButton.SetActive(true);
        Time.timeScale = 1f;
    }
}
