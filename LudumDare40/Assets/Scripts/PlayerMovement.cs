 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour {

    private float speedX = 10f;
    private float jumpSpeed = 15;
    private float jumpDecreaseRate = 60;
    private Vector2 movementVector;

    private float boxColliderHeight;

    [SerializeField]
    private Transform graphicsTransform;

    private Rigidbody2D rb;
    private Animator animator;

    //Strings used by the animator:
    private string animatorWalking = "Walking";



	// Use this for initialization
	private void Start () {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	private void Update () {
        float movementX = Input.GetAxis("Horizontal") * speedX;
        float movementY = Mathf.Max(Physics2D.gravity.y, movementVector.y - (jumpDecreaseRate * Time.deltaTime));
        bool jumpPressed = Input.GetButton("Jump");


        bool walkingAnim = false;

        bool isGrounded = IsGrounded();
        //If the player is not on the ground, then the player should not walk/run
        if (movementX != 0)
        {
            if (!isGrounded)
            {
                walkingAnim = false;
            }
            else
            {
                walkingAnim = true;
            }

            //Make the player face the direction they are moving in
            if(movementX < 0)
            {
                FlipDirection(graphicsTransform, true);
            }
            else
            {
                FlipDirection(graphicsTransform, false);
            }
        }

        if (jumpPressed)
        {
            if (isGrounded)
            {
                movementY = jumpSpeed;
            }
        }
        
        animator.SetBool(animatorWalking, walkingAnim);

        //Compute the movement vector to be used during FixedUpdate()
        movementVector = new Vector2(movementX, movementY);
	}

    //FixedUpdate is called once per physics frame
    private void FixedUpdate()
    {
        rb.velocity = movementVector;
    }

    private bool IsGrounded()
    {
        float maxDistanceToGround = 0.05f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, maxDistanceToGround);
        if(hit.transform != null)
        {
            Debug.Log("Hit: " + hit.transform.name);
            return true;
        }
        return false;
    }

    private void FlipDirection(Transform parent, bool faceLeft)
    {
        if (faceLeft)
        {
            graphicsTransform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            graphicsTransform.localScale = new Vector3(1, 1, 1);
        }
    }
}
