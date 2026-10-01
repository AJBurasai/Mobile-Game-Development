using UnityEngine;

public class laserMovement : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip shootingAudioClip;
    Rigidbody2D rb;

    private void OnBecameInvisible()
    {
        // Code to destroy shot after it goes off screen. 
        Destroy(gameObject);
    }


    void Start()
    { // Audio to play on spawn
        audioSource.PlayOneShot(shootingAudioClip);
    }

}
