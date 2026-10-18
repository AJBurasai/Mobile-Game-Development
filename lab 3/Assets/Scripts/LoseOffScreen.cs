using UnityEngine;
using UnityEngine.SceneManagement;
public class LoseOffScreen : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Camera.main.WorldToScreenPoint(transform.position).y < 0.0f)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
