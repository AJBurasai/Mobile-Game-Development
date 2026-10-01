
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;
using UnityEngine.Windows;

[RequireComponent(typeof(Rigidbody2D))]
public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    Rigidbody2D rb;
    float xInput;
    float yInput;
    public Animator animator;
    public GameObject Laser;
    public GameObject player_plane;
    bool laserJustShot;
    public float WaitForSeconds;

    [SerializeField] InputActionReference moveAction;
    [SerializeField] InputActionReference attackAction;

    void OnEnable() => moveAction.action.Enable();
    void OnDisable() => moveAction.action.Disable();

    void Update()
    {
        Vector2 move = moveAction.action.ReadValue<Vector2>();


        // Horizontal Movement
        xInput = UnityEngine.Input.GetAxisRaw("Horizontal");

        // <Vertical Movement
        yInput = UnityEngine.Input.GetAxisRaw("Vertical");
        bool flyingUp = UnityEngine.Input.GetAxisRaw("Vertical") > 0;
        animator.SetBool("flyingUp", flyingUp);
        bool flyingDown = UnityEngine.Input.GetAxisRaw("Vertical") < 0;
        animator.SetBool("flyingDown", flyingDown);

     
        if (UnityEngine.Input.////////) && laserJustShot == true)
        {
            Instantiate(Laser, player_plane.transform.position, Quaternion.identity).GetComponent<Rigidbody2D>().AddForce(player_plane.transform.right * 10, ForceMode2D.Impulse);
            StartCoroutine(nameof(laserCooldown));
            laserJustShot = false;
        }
    }

    private void Start()
    {
        laserJustShot = true;
        
    }
    private void Awake()
      => rb = GetComponent<Rigidbody2D>();

    // Code for setting the laser cooldown
    IEnumerator laserCooldown() {
        yield return new WaitForSeconds(WaitForSeconds);
        laserJustShot = true;
    }
    

    [System.Obsolete]
    private void FixedUpdate()
    {
        // code for establishing player position in relation to movement direction & speed
       rb.velocity = new Vector2(yInput * moveSpeed, rb.velocity.y);
       rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.x);

    }
}