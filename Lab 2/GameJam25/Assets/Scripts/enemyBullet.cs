using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    /* This is the script that handles the movement of enemy bullets and their collision */

    
    // --- Variables --- 
    public float bulletSpeed = 8f;      // speed of bullet
    private Vector3 direction;          // direction bullet fires in

    public void SetDirection(Vector3 dir)   // function for picking what direction to go
    {
        direction = dir.normalized;     // normalise vector
    }

    void Update()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime;     // bullet moves throught space in chosen direction
        transform.Rotate(0f, 0f, 360f * Time.deltaTime);        // makes the bullet spin as it moves 
        Destroy(this.gameObject, 5f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(this.gameObject);       // bullet is destroyed on collision with player
        }
    }
}
