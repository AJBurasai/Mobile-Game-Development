using System.Collections;
using UnityEngine;

public class enemy1Movement : MonoBehaviour
{
        
    // --- Variables --- 
    public float enemySpeed = 3f;  

    void Update()
    {
        transform.Translate(Vector3.left * enemySpeed * Time.deltaTime);    

    }

    IEnumerator move() 
    {
        yield return new WaitForSeconds(2f);
        // flip sprite and make this coroutine the default state
    }
}
