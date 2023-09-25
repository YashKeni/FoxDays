using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : MonoBehaviour
{
    [SerializeField] AudioClip gemPickupSFX;
    [SerializeField] int gemValue = 100;

    bool hasCollected = false;

    AudioSource gemSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !hasCollected)
        {
            hasCollected = true;

            FindObjectOfType<GameSession>().AddToScore(gemValue);
            gemSound = GameObject.Find("SFX AudioSource").GetComponent<AudioSource>();
            gemSound.PlayOneShot(gemPickupSFX, 1f);

            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
