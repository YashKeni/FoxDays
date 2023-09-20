using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gems : MonoBehaviour
{
    [SerializeField] AudioClip gemPickupSFX;
    AudioSource gemSound;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gemSound = GameObject.Find("SFX AudioSource").GetComponent<AudioSource>();
            gemSound.PlayOneShot(gemPickupSFX, 1f);
            Destroy(gameObject);
        }
    }
}
