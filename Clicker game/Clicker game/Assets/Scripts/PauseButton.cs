using UnityEngine;

public class PauseButton : MonoBehaviour
{
    /* this script pauses the game, nothing crazy */

    private bool isPaused = false;
    public GameObject settingsCanvas;
    public GameObject clickerButton;

    public void ButtonTapped()            // call function when button clicked
    {
        Debug.Log("Pause button tapped");
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
        Debug.Log("Game Paused");
        Time.timeScale = 0f;
        isPaused = true;
        settingsCanvas.SetActive(true);
        clickerButton.SetActive(false);
    }

    public void ResumeGame()           // resume game
    {
        Debug.Log("game resumed");
        Time.timeScale = 1f;
        isPaused = false;
        settingsCanvas.SetActive(false);
        clickerButton.SetActive(true);
    }
}
