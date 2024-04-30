using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    //Movement
    [SerializeField] float movementSpeed = 5f;
    
    //Jump
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float jumpCooldown = 0.5f;
    //Ground Check
    [SerializeField] LayerMask groundCheckLayerMask;
    [SerializeField] Vector3 groundCheckPos;
    [SerializeField] float groundCheckRadius = 0.5f;

    Vector2 moveInput;
    Rigidbody myRigidbody;
    bool isGrounded = true;
    float jumpCooldownAtStart;

    // Start is called before the first frame update
    void Start()
    {
        jumpCooldownAtStart = jumpCooldown;
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        Run();
        IsGrounded();

        if (Input.GetButtonDown("Jump") && isGrounded == true && jumpCooldown <= 0f)
        {
            Jump();
            isGrounded = false;
            jumpCooldown = jumpCooldownAtStart;
        }

        jumpCooldown -= Time.deltaTime;
        if (jumpCooldown < 0f) { jumpCooldown = 0; }
    }

    void Run()
    {
        Vector3 playerVelocity = new Vector3(moveInput.x * movementSpeed, myRigidbody.velocity.y, moveInput.y * movementSpeed);
        myRigidbody.velocity = transform.TransformDirection(playerVelocity);
    }

    void Jump()
    {
        myRigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        isGrounded = false;
    }

    void IsGrounded()
    {
        if (Physics.CheckSphere(new(transform.position.x + groundCheckPos.x, transform.position.y + groundCheckPos.y, transform.position.z + groundCheckPos.z), groundCheckRadius, groundCheckLayerMask) && jumpCooldown == 0f)
        {
            isGrounded = true;
        }
        else { isGrounded = false; }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new(transform.position.x + groundCheckPos.x, transform.position.y + groundCheckPos.y, transform.position.z + groundCheckPos.z), groundCheckRadius);
    }
}
