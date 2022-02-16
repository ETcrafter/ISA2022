using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public int maxJumps = 1;
    private int jumpCount;

    public CharacterController controller;

    private Vector3  moveDirection;
    public float gravityScale;
    private bool lastGroundState = true;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float yStore = moveDirection.y;

        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;

        moveDirection.y = yStore;


        if (controller.isGrounded)
        {
            jumpCount = 0;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if(jumpCount < maxJumps)
            {
                moveDirection.y = jumpForce;
                jumpCount++;
            }
        }

        if (!controller.isGrounded)
        {
            moveDirection.y = moveDirection.y + (Physics.gravity.y * Time.deltaTime * gravityScale);
        }
        

        controller.Move(moveDirection * Time.deltaTime);

        if (controller.isGrounded != lastGroundState && !lastGroundState) 
        {
            moveDirection.y = 0f;
        }
        lastGroundState = controller.isGrounded;
    }
}
