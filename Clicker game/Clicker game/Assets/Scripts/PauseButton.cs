using UnityEngine;

public class PauseButton : MonoBehaviour
{
    /* this script pauses the game, nothing crazy */

    private bool isPaused = false;
    public GameObject settingsPanel;
    public GameObject clickerButton;

    public void PauseButtonTapped()            // call function when button clicked
    {
        
        if (!isPaused)          // check if the game is already paused or not
        {
            PauseGame();
            return;
        }
        if (isPaused)
        {
            ResumeGame();
            return;
        }
    }
    public void PauseGame()        // pause game
    {
        Time.timeScale = 0f;
        isPaused = true;
        settingsPanel.SetActive(true);
        clickerButton.SetActive(false);
    }

    public void ResumeGame()           // resume game
    {
        Time.timeScale = 1f;
        isPaused = false;
        settingsPanel.SetActive(false);
        clickerButton.SetActive(true);
    }
}
