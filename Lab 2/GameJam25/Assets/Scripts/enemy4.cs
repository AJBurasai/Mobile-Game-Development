using UnityEngine;
using System.Collections;
using System;

public class enemy4Behaviour: MonoBehaviour
{
    /*  This script handles the movement for most enemy types, causes them to move from right to left
        in a straight line, then destroy themselves when off screen. This script also handles the death 
        animations for only the 4th enemy type as it has health that must be depleated through multiple shots 
        and provides player feedback through a flashing sprite when this enemy takes damage from bullets. */


    // --- Variables --- 
    public float enemySpeed = 3f;       // enemy movement speed
    public float leftBoundary = -10f;   // destroy object when past this point
    public Animator animator;
    public bool dead;
    public int health;
    public Color flashColor = Color.white;
    public float flashDuration = 0.3f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public AudioClip hit;
    public AudioClip explosion;
    private AudioSource audioSource;


    void Start()
    {
        dead = false;
        health = 3;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        audioSource = GetComponent<AudioSource>();
    }

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
        if (other.CompareTag("Player"))         // even though this enemy has multiple hit points, will still die if it hits the player once
        {
            enemySpeed = 0f;
            audioSource.PlayOneShot(explosion);     // play explosion sfx
            dead = true;
            animator.SetBool("deathAnim", dead);        // explosion animation
            Destroy(this.gameObject, 0.9f);         // destroy game object after 0.9 sec
        }

        else if (other.CompareTag("laser"))      // if collides with bullet and health more than 0, take damage, provide feedback
        {
            health -= 1;
            audioSource.PlayOneShot(hit);
            StartCoroutine(FlashEffect());

            if (health <= 0)
            {
                Die();
            }
        }


        Destroy(other.gameObject);
    }

    void Die()      // function for killing the enemy
    {

        if (!dead)
        {
            
            scoreTracker.score += 300;
            enemySpeed = 0f;
            audioSource.PlayOneShot(explosion);     // play explosion sfx
            dead = true;
            animator.SetBool("deathAnim", dead);        // explosion animation
            Destroy(this.gameObject, 0.9f);         // destroy game object after 0.9 sec
        }
    }

    IEnumerator FlashEffect()       // coroutine for flashing effect, player feedback
    {
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        spriteRenderer.color = originalColor;
    }

}
