using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] public TMP_Text exitLeveltext;

    public bool hasEntered = false;

    void Start()
    {
        exitLeveltext.enabled = false;
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other is CapsuleCollider2D)
        {
            // levelManager.LoadNextLevel();
            exitLeveltext.enabled = true;
            hasEntered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other is CapsuleCollider2D)
        {
            exitLeveltext.enabled = false;
        }
    }
}
