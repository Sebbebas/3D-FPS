using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpForce = 8f;
    [SerializeField] LayerMask groundCheckLayerMask;
    [SerializeField] Vector2 groundCheckPos;
    [SerializeField] float groundCheckRadius = 0.5f;

    Vector2 moveInput;
    Rigidbody myRigidbody;
    bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            Jump();
        }
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Grounded")
        {
            isGrounded = true;
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheckPos, groundCheckRadius);
    }
}
