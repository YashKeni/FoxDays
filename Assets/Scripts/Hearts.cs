using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    GameSession gameSession;

    bool hasCollected = false;

    void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !hasCollected)
        {
            hasCollected = true;
            gameSession.currentHearts++;
            gameSession.UpdateHearts();
            DestroyAfterPickup();
        }
    }

    public void DestroyAfterPickup()
    {
        Destroy(gameObject);
    }
}
