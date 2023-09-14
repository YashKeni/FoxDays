using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 2f;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D footCollider;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        footCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        FlipSideWays();
    }


    void Run()
    {
        Vector2 playerVelo = new Vector2(moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelo;

        bool hasHorizSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", hasHorizSpeed);
    }

    void FlipSideWays()
    {
        bool hasHorizSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (hasHorizSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    // Player Input Methods
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!footCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (value.isPressed)
        {
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }
    }
}
