using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject killingElement;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] int scorePoints = 500;

    Rigidbody2D myRigidBody;
    AudioSource audioSource;
    GameSession gameSession;

    bool isDead = false;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        gameSession = FindObjectOfType<GameSession>();
    }

    void Update()
    {
        if (!isDead)
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipSprite();
    }

    void FlipSprite()
    {
        transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
    }

    public void RemoveColliders()
    {
        gameSession.AddToScore(scorePoints);
        audioSource.clip = deathSFX;
        audioSource.Play();
        isDead = true;
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        killingElement.SetActive(false);
        myRigidBody.velocity = new Vector2(0f, 0f);
    }

    public void DestroyAfterAnim()
    {
        Destroy(gameObject);
    }
}
