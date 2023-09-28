using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Void : MonoBehaviour
{
    GameSession gameSession;

    public bool fallenInVoid = false;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.tag == "Player" && other is BoxCollider2D)
        {
            fallenInVoid = true;
            gameSession.ProcessPlayerDeath();
        }
    }
}
