using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;
public class restartButton : MonoBehaviour
{
    
    public void Restart()
    {
        SceneManager.LoadScene("1st_Level");
    }


    
}