using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] public float startingHealth;
    public float currentHealth {  get; private set; }
    public Animator anim;
    private bool dead;

    [Header("iFrames")]

    [SerializeField] public float iFramesDuration;
    [SerializeField] public float numberOfFlashes;
    private SpriteRenderer spriteRend;

    private void Awake()
    {
        // code to set starting health & to establish the Animator functionality 
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        
    }
    private void OnTriggerEnter2D(Collider2D other)
        {
        // code to ensure the player takes damage when colliding with Enemies
            if (other.CompareTag ("Enemy"))
            {
                GetComponent<Health>().TakeDamage(1);
            }
        }

    public void TakeDamage(float _damage)
    {
        // code to calculate damage states of player
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        
        if (currentHealth > 0)
        {
            // player hurt
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else

        { // player dead
            if (!dead)
            {
                StartCoroutine(GameOver());
            }
        }
        
        
    }

    private IEnumerator Invunerability()
    {
        // code for invunerabiliy frames
        Physics2D.IgnoreLayerCollision(0, 3, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1,0,0,0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            }
        Physics2D.IgnoreLayerCollision(0, 3, false);
    }
   
   private IEnumerator GameOver()
   {
        anim.SetTrigger("die");
        GetComponent<playerMovement>().enabled = false;
        dead = true;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game Over");
   }
}
