using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    void Awake()
    {
        int numPersist = FindObjectsOfType<ScenePersist>().Length;
        if (numPersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetPersist()
    {
        Destroy(gameObject);
    }

}
