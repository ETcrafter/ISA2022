using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyController : MonoBehaviour
{
    [SerializeField] public Rigidbody PlayerBody;
    [SerializeField] public GameObject camPivot;

    [SerializeField] public Transform GroundCheck;
    [SerializeField] public LayerMask FloorMask;
    [SerializeField] private bool IsGrounded;

    [SerializeField] public float WalkSpeed = 5f;
    [SerializeField] public float RunSpeed = 7f;
    float CurrentSpeed;

    [SerializeField] public float JumpForce;
    [SerializeField] public float DoubleJumpForce;
    [SerializeField] public bool CanJump;


    public float SpeedSmoothTime = 0.1f;
    float SpeedSmoothVelocity;
    public float TurnSmoothTime = 0.2f;
    float TurnSmoothVelocity;

    public Animator Animator;
    public SpriteRenderer SR;
    public bool movingBackwards;

    public Transform CameraT;

    void Update()
    {

        //Roept de beweging en het springen aan
        PlayerJump();

        PlayerMovement();
        PlayerRotation();

        Vector3 targetPosition = new Vector3(camPivot.transform.position.x, transform.position.y, camPivot.transform.position.z);
        transform.LookAt(targetPosition);


    }

    void Start()
    {
        CameraT = Camera.main.transform;
    }

    void PlayerJump()
    {


        //Wanneer de grond wordt geraakt doet de dubble jump het weer
        if (Physics.CheckSphere(GroundCheck.position, 0.05f, FloorMask))
        {
            CanJump = true;
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }

        //Laat de speler springen
        

        if (Input.GetKeyDown(KeyCode.Space))
        {


            if (IsGrounded)
            {
                PlayerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse); 
            }
            else
            {
                if (CanJump)
                {
                    PlayerBody.AddForce(Vector3.up * (JumpForce - DoubleJumpForce), ForceMode.Impulse);
                    CanJump = false;

                    //Reset de gravity voor de dubbeljump
                    PlayerBody.velocity = Vector3.zero;
                    PlayerBody.angularVelocity = Vector3.zero;
                }
            }
        }
        Debug.Log(IsGrounded);

        Animator.SetBool("OnGround", IsGrounded);
        Animator.SetFloat("yVelocity", PlayerBody.velocity.y);
    }

    void PlayerMovement()
    {
        //// Vertical and Horizontal Movement

        float facing = Camera.main.transform.eulerAngles.y; // Getting the angle the camera is facing

        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");



        Vector3 myInputs = new Vector3(horizontalMovement, 0, verticalMovement);

        // we rotate them around Y, assuming your inputs are in X and Z in the myInputs vector
        Vector3 myTurnedInputs = Quaternion.Euler(0, facing, 0) * myInputs;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            PlayerBody.MovePosition(transform.position + myTurnedInputs.normalized * RunSpeed * Time.deltaTime);
        }
        else
        {
            PlayerBody.MovePosition(transform.position + myTurnedInputs.normalized * WalkSpeed * Time.deltaTime);
        }


        if (Physics.CheckSphere(GroundCheck.position, 0.1f, FloorMask))
        {
            Animator.SetFloat("MoveSpeedHorizontal", Mathf.Abs(horizontalMovement));
            Animator.SetFloat("MoveSpeedVertical", Mathf.Abs(verticalMovement));
        }

        //Flipped het karakter van links naar rechts
        if (!SR.flipX && myInputs.x < 0)
        {
            SR.flipX = true;
        }
        else if (SR.flipX && myInputs.x > 0)
        {
            SR.flipX = false;
        }

        //Zorgt er voor dat het karakter wisselt van front/back animaties

        if ((Vector3.zero + myTurnedInputs.normalized).sqrMagnitude > 0.2f)
        {
            movingBackwards = Vector3.Dot((transform.position - CameraT.position).normalized, Vector3.zero + myTurnedInputs.normalized) > 0.5f;
        }
        Animator.SetBool("MovingBackwards", movingBackwards);


        if (movingBackwards == true)
        {

            Debug.Log("bool staat aan");
        }
        
    }

    void PlayerRotation()
    {
        //Speler beweegt dankzij de richting van de camera
        float yPivotRotation = camPivot.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, yPivotRotation, 0);
    }


}
