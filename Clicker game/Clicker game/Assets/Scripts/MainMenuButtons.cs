using UnityEngine;

public class MainMenuButtons : MonoBehaviour
{
    /* This script handles the main menu buttons found at the bottom of the game screen */

    private bool isPaused = false;
    public GameObject clickerButton;
    public GameObject upgradesPanel;

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
}
