using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    bool hasEntered = false;

    LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other is CapsuleCollider2D && !hasEntered)
        {
            levelManager.LoadNextLevel();
            hasEntered = true;
        }
    }
}
