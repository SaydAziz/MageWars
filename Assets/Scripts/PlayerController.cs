using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    Vector2 moveInput;
    Vector3 moveDir;
    float moveSpeed = 3f;

    private void Awake()
    {
        
    }

    private void FixedUpdate()
    {
        moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
        rb.AddForce(moveDir.normalized * moveSpeed, ForceMode.VelocityChange);
    }

    public void Move(Vector2 value)
    {
        moveInput = value;      
    }
}
