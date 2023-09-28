using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 2f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathFling = new Vector2(0f, 20f);
    [SerializeField] float deathDelayTime = 2f;

    [Header("Audio Settings")]
    [SerializeField] AudioClip levelFinishSFX;
    [SerializeField] AudioClip jumpSFX;

    float gravScaleAtStart;
    bool isAlive = true;
    bool hasFinishedLevel = false;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    Animator animator;
    CapsuleCollider2D bodyCollider;
    BoxCollider2D footCollider;
    LevelManager levelManager;
    House house;
    AudioSource audioSource;
    GameSession gameSession;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        footCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();

        levelManager = FindObjectOfType<LevelManager>();
        house = FindObjectOfType<House>();
        gameSession = FindObjectOfType<GameSession>();

        gravScaleAtStart = myRigidBody.gravityScale;
    }

    void Update()
    {
        if (!isAlive) { return; }
        Run();
        FlipSideWays();
        ClimbLadder();
        Jumping();
        TakeDamage();
    }

    // ----------------------------------------------- User defined function ---------------------------------------------

    void Run()
    {
        Vector2 playerVelocity = new(moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        animator.SetBool("isRunning", hasHorizontalSpeed);
    }

    void FlipSideWays()
    {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (hasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }

    void ClimbLadder()
    {
        if (!footCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myRigidBody.gravityScale = gravScaleAtStart;
            animator.SetBool("isClimbing", false);
            return;
        }

        Vector2 climbVelocity = new(myRigidBody.velocity.x, moveInput.y * climbSpeed);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0;

        bool hasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        animator.SetBool("isClimbing", hasVerticalSpeed);
        animator.SetBool("isJumping", false);
    }

    void Jumping()
    {
        if (!footCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
        }
    }

    void TakeDamage()
    {
        if (bodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            StartCoroutine(EnemyAttackDelay());

            if (gameSession.currentHearts < 1)
            {
                Die();
            }
        }
    }

    void Die()
    {
        isAlive = false;
        animator.SetTrigger("die");
        myRigidBody.velocity = deathFling;

        myRigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
        myRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        bodyCollider.enabled = false;
        footCollider.enabled = false;

        StartCoroutine(DeathDelay());
    }

    // ---------------------------------------------------- Coroutines ---------------------------------------------------

    IEnumerator EnemyAttackDelay()
    {
        animator.SetTrigger("damage");
        gameSession.TakeLife();
        bodyCollider.enabled = false;
        yield return new WaitForSeconds(deathDelayTime);
        bodyCollider.enabled = true;
    }

    IEnumerator DeathDelay()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        yield return new WaitForSeconds(deathDelayTime);

        gameSession.ProcessPlayerDeath();
    }

    // ----------------------------------------------- Player Input Methods ----------------------------------------------

    void OnMove(InputValue value)
    {
        if (!isAlive) { return; }

        if (!hasFinishedLevel)
        {
            moveInput = value.Get<Vector2>();
        }
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }
        if (!footCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (value.isPressed)
        {
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);
            audioSource.clip = jumpSFX;
            audioSource.Play();
            animator.SetTrigger("jump");
        }
    }

    void OnExitLevel()
    {
        if (!isAlive) { return; }

        if (FindObjectOfType<House>().hasEntered)
        {
            hasFinishedLevel = true;
            house.exitLeveltext.enabled = false;
            audioSource.clip = levelFinishSFX;
            audioSource.Play();
            levelManager.LoadNextLevel();
        }
    }
}
