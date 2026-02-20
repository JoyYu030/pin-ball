using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;

    [SerializeField] private TMP_Text scoreText;
    //[SerializeField] private TMP_Text restartText; 
    private int currentScore;     // real score
    private float displayScore;   // animated score

    [SerializeField] private float countSpeed = 800f; 

    void Awake()
    {
        Instance = this;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (displayScore < currentScore)
        {
            displayScore = Mathf.MoveTowards(
                displayScore,
                currentScore,
                countSpeed * Time.deltaTime
            );

            scoreText.text = $"Pain Indicator: {(int)displayScore}";
        }
    }

    public void AddToScore(int amt)
    {
        currentScore += amt;
    }
}
