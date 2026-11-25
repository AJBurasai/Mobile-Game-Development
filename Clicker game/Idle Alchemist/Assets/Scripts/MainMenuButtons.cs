using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    /* This script handles the main menu buttons found at the bottom of the game screen */

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
        Haptics.Impact(Haptics.Style.Medium);    // vibration on panel open
    }

    public void CloseCurrentPanel()     // close panel, resume game
    {
        if (currentPanel ==  null) { return; }  // ignore function if no panel active

        currentPanel.SetActive(false);
        currentPanel = null;
        clickerButton.SetActive(true);
        Time.timeScale = 1f;
        Haptics.Impact(Haptics.Style.Rigid);     // vibration on panel close
    }
}
