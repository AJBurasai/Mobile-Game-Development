using UnityEngine;
using System.Collections;

public class enemyShooting : MonoBehaviour
{
    /*  This scipt handles the shooting functionality for the third enemy type. It calculates 
        what direction to shoot based on the players current position then instantiates the 
        enemy bullet game object */

        
    // --- Variables --- 
    public GameObject enemyBullet;          // bullet prefab goes here
    public float shootInterval = 2f;        // time between shots
    private Transform playerPos;            // player position stored here

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(ShootAtPlayer());
    }

    IEnumerator ShootAtPlayer()     //coroutine that calls shoot function at predefined intervals
    {
        while (true)
        {
            Shoot();        // calls shoot function
            yield return new WaitForSeconds(shootInterval);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("Player")) || (other.CompareTag("laser")))
        {
            shootInterval = 99f;        // prevents enemy from shooting the player while enemy is being destroyed
        }
    }

    void Shoot()
    {
        if (playerPos == null) return;         // safety measure: if player isnt there, dont run function
        GameObject bullet = Instantiate(enemyBullet, transform.position, Quaternion.identity);
        Vector3 direction = (playerPos.position - transform.position).normalized;      // bullet aims towards player
        bullet.GetComponent<enemyBullet>().SetDirection(direction);            // set direction of bullet 
    }
}
