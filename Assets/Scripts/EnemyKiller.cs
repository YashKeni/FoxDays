using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKiller : MonoBehaviour
{
    [SerializeField] Animator animator;

    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
        animator = GetComponentInParent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.IsTouchingLayers(LayerMask.GetMask("EnemyKiller")))
        {
            animator.SetTrigger("die");
        }
    }
}
