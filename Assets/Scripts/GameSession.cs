using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] int maxHearts = 3;
    [SerializeField] int score = 0;

    [Header("GameObjects")]
    [SerializeField] GameObject heartPrefab;
    [SerializeField] Transform heartContainer;
    [SerializeField] float heartSpacing = 30f;

    [Header("UI")]
    [SerializeField] TMP_Text scoreText;

    int currentHearts;

    void Awake()
    {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = "Score: " + score.ToString();

        currentHearts = maxHearts;
        UpdateHearts();
    }

    public void ProcessPlayerDeath()
    {
        if (currentHearts > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = "Score: " + score.ToString();
    }

    void TakeLife()
    {
        currentHearts--;
        currentHearts = Mathf.Clamp(currentHearts, 0, maxHearts);
        UpdateHearts();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void UpdateHearts()
    {
        foreach (Transform child in heartContainer)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentHearts; i++)
        {
            Vector3 heartPosition = new Vector3(i * heartSpacing, 0, 0);
            Instantiate(heartPrefab, heartContainer).transform.localPosition = heartPosition;
        }
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetPersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

}
