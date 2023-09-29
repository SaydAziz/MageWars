using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] AssetController assetController;


    Camera cam;
    [SerializeField] GameObject RightHand;

    Vector2 moveInput;
    Vector2 lookInput;

    float mouseSensitivity = .1f;
    float xRot;

    Vector3 moveDir;
    float moveSpeed = 1.3f;
    float jumpHeight = 8f;
    float dashSpeed = 16f;

    bool wallJumped;
    bool dashed;
    bool isGrounded; 
    LayerMask groundLayer;
    LayerMask wallLayer;

    //public float wallRunForce, maxWallRunTime;
    //private float wallRunTimer;

    private RaycastHit WallHit;

    private bool wallFwdR, wallFwdL;

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
        RaycastHit hit;
        isGrounded = Physics.SphereCast(transform.position, .5f, Vector3.down, out hit, 0.8f, groundLayer);
        

        if (rb.velocity.y < 2 && rb.velocity.y > -2)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * 3 * Time.deltaTime;
        }

        if (isGrounded)
        {
            dashed = false;
            rb.drag = 4;
            moveDir = transform.forward * moveInput.y + transform.right * moveInput.x;
            rb.AddForce(moveDir.normalized * moveSpeed, ForceMode.VelocityChange);
        }
        else
        {
            rb.drag = 0;
        }



        //Debug.Log(rb.velocity.magnitude);
    }


    private void doLook()
    {
        cam.transform.Rotate(Vector3.right * (-lookInput.y * mouseSensitivity)); //needs to be clamped
        transform.Rotate(Vector3.up * (lookInput.x * mouseSensitivity));
    }

    public void getDashInput()
    {
        if (!dashed)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(((transform.forward * moveInput.y + transform.right * moveInput.x) + transform.up * .2f)* dashSpeed, ForceMode.Impulse);
            dashed = true;
        }
        
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
            wallJumped = false;
            rb.drag = 0;
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
        }
        else if (CheckWall() && !wallJumped)
        {
            StartCoroutine(WJCD());
            rb.drag = 0;
            rb.velocity = Vector3.zero;
            rb.AddForce((transform.forward + (Vector3.up))  * (2 + jumpHeight),  ForceMode.Impulse);
        }
    }

    public void prepShot()
    {
        RightHand.GetComponentInChildren<Spell>().Create();
        assetController.ToggleHandFlip();
    }

    public void releaseShot()
    {
        RightHand.GetComponentInChildren<Spell>().Shoot();
        assetController.ToggleHandFlip();

    }

    private bool CheckWall()
    {
        wallFwdR = Physics.Raycast(transform.position, transform.right, out WallHit, 3f, groundLayer);
        wallFwdL = Physics.Raycast(transform.position, -transform.right, out WallHit, 3f, groundLayer);
        //Debug.Log(wallFwd);

        if (wallFwdL || wallFwdR)
        {
            return true;
        }
        else return false;
    }

    IEnumerator WJCD()
    {
        wallJumped = true;
        yield return new WaitForSeconds(.5f);
        wallJumped = false;
    }

    //private void CheckWallRun()
    //{
    //    if (wallLeft || wallRight && moveInput.y > 0 && !isGrounded)
    //    {

    //    }
    //}

    //private void StartWallRun()
    //{

    //}
    //private void DoWallRun()
    //{
    //    Vector3 wallNormal = wallRight ? RightWallHit.normal : LeftWallHit.normal;

    //    Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

    //    rb.AddForce(wallForward * wallRunForce, ForceMode.Force);
    //}

    //private void StopWallRun()
    //{

    //}
}

