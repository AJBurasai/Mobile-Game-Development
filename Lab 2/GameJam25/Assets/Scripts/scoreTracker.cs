using UnityEngine;
using TMPro;

public class scoreTracker : MonoBehaviour
{
    /*  This script will track the score gained from destroying enemies, it 
        will track it using a global variable. Must be applied to player game object */

    // --- Variables --- 
    public static int score;            // score variable to be used in other scripts
    public TextMeshProUGUI scoreText;          // score ui element

    void Start()
    {
        score = 0;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score; 
    }

    void Update()
    {
        UpdateScoreUI();
    }
}
