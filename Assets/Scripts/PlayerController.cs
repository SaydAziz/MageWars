using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    Camera cam;

    Vector2 moveInput;
    Vector2 lookInput;

    float mouseSensitivity = .1f;
    float xRot;

    Vector3 moveDir;
    float moveSpeed = 1f;
    float jumpHeight = 8f;

    bool isGrounded;
    LayerMask groundLayer;

    private void Start()
    {
        groundLayer = LayerMask.GetMask("Ground");
    }

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        doLook();
    }

    private void FixedUpdate()
    {

        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f, groundLayer);

        if (isGrounded)
        {
            rb.drag = 6;
            moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
            rb.AddForce(moveDir.normalized * moveSpeed, ForceMode.VelocityChange);
        }
        else
        {
            rb.drag = 0;
        }

        

        Debug.Log(rb.velocity.magnitude);
    }


    private void doLook()
    {
        cam.transform.Rotate(Vector3.right * (-lookInput.y * mouseSensitivity)); //needs to be clamped
        transform.Rotate(Vector3.up * (lookInput.x * mouseSensitivity));
    }

    public void getMoveInput(Vector2 value)
    {
        moveInput = value;      
    }

    public void getLookInput(Vector2 value)
    {
        lookInput = value;
    }

    public void getJumpInput()
    {
        if (isGrounded)
        {
            rb.drag = 0;
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
    }
}

