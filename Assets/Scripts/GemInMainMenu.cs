using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemInMainMenu : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetTrigger("collect");
        }
    }

    public void RegenGems()
    {
        StartCoroutine(RegenAfterTime());
    }

    IEnumerator RegenAfterTime()
    {
        yield return new WaitForSeconds(3f);
        animator.SetTrigger("regen");
    }

}
