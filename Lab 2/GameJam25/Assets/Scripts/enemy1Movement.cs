using UnityEngine;

public class enemy1Movement : MonoBehaviour
{
    /*  This script handles the movement for most enemy types, causes them to move from right to left
        in a straight line, then destroy themselves when off screen. */

        
    // --- Variables --- 
    public float enemySpeed = 3f;       // enemy movement speed
    public float leftBoundary = -10f;   // destroy object when past this point

    void Update()
    {
        transform.Translate(Vector3.left * enemySpeed * Time.deltaTime);        // enemy moves left over time

        if (transform.position.x < leftBoundary)        // if enemy travels off screen, destroy it 
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player")) || (other.CompareTag("laser")))
        {
            enemySpeed = 0f;        // kill speed when destroyed by bullet or player
        }
    }
}
