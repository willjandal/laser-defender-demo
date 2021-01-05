using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BestScore : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI bestScore;
    int currentScore; 
    GameSession gameSession; 

    // Start is called before the first frame update
    void Start()
    {
        bestScore = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();

        bestScore.text = PlayerPrefs.GetInt("Best", 0).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        currentScore = gameSession.GetScore();

        if (currentScore > PlayerPrefs.GetInt("Best", 0))
        {
            PlayerPrefs.SetInt("Best", currentScore);
            //bestScore.text = gameSession.GetScore().ToString();
            bestScore.text = currentScore.ToString();
        }
    }
}
