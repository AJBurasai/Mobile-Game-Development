using Unity.VisualScripting;
using UnityEngine;

public class enemyDeath : MonoBehaviour
{
    /*  This script will be applied to most enemy types, it will handle collision with the player
        and the players bullets, both cases will result in the enemy being destroyed  */

        
    // --- Variables --- 
    public Animator animator;
    public bool dead;
    public AudioClip explosion;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    private AudioSource audioSource;

    void Start()
    {
        dead = false;
        audioSource = GetComponent<AudioSource>();
    }

    
    void OnTriggerEnter2D(Collider2D other)     // if colliding with the player or bullet, destroy enemy
    {
        if (other.CompareTag("Player"))       
        {
            Die();
        }

        else if (other.CompareTag("laser"))
        {
            Die();
            Destroy(other.gameObject);
        }



    }

    void Die()
    {
        if (this.gameObject == enemy1)
        {
            scoreTracker.score += 100;
        }

        else if (this.gameObject == enemy2)
        {
            scoreTracker.score += 150;
        }

        else if (this.gameObject == enemy3)
        {
            scoreTracker.score += 200;
        }

        if (!dead)
        {
            
            audioSource.PlayOneShot(explosion);     // play explosion sfx
            dead = true;
            animator.SetBool("deathAnim", dead);        // explosion animation
            Destroy(this.gameObject, 0.9f);     // destroy game object
        }
    }
}
