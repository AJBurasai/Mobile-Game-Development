using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.U2D;


public class MobileMovement : MonoBehaviour
{
    Rigidbody2D rb;
    float xInput;
    float yInput;
    public float moveSpeed = 10f;

    private void Awake()
    => rb = GetComponent<Rigidbody2D>();


    [SerializeField] InputActionReference moveAction;

    void OnEnable() => moveAction.action.Enable();
    void OnDisable() => moveAction.action.Disable();

    void Update()
    {
        Vector2 move = moveAction.action.ReadValue<Vector2>();

    }

    [System.Obsolete]
    private void FixedUpdate()
    {
        // code for establishing player position in relation to movement direction & speed
        rb.velocity = new Vector2(yInput * moveSpeed, rb.velocity.y);
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.x);

    }
}
