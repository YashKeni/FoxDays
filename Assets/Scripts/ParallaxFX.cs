using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxFX : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField] float relativeMove = .3f;
    [SerializeField] bool lockY = false;

    void Update()
    {
        if (lockY)
        {
            transform.position = new Vector2(playerCamera.position.x * relativeMove, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(playerCamera.position.x * relativeMove, playerCamera.position.y * relativeMove);
        }
    }
}
