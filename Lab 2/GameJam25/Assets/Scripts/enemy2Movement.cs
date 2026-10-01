using UnityEngine;

public class enemy2Movement : MonoBehaviour
{
    /*  This scirpt handles the movement for only the second enemy type, it causes this enemy
        to move in a sine wave pattern across the screen */

        
        // --- Variables --- 
    public float enemySpeed = 3f;       // enemy movement speed
    public float leftBoundary = -10f;   // destroy object when past this point
    public float amp = 1.5f;            // sine wave amplitude
    public float freq = 2f;             // sine wave frequency 
    private Vector3 startPos;           // initial position of enemy
    private Vector3 deathPos;           // position of enemy when destroyed

    void Start()
    {
        startPos = transform.position;      // keeps track of where the enemy spawned from
    }


    void Update()
    {
        float newX = transform.position.x - enemySpeed * Time.deltaTime;    // moves from right to left
        float newY = startPos.y + Mathf.Sin(Time.time * freq) * amp;    // apply sine wave motion to y position
        transform.position = new Vector3(newX, newY, 0);
        
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
            amp = 0f;
        }
    }
}
