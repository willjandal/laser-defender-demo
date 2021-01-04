using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    [SerializeField] int score = 0;

    
    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {

        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            //gameObject.SetActive(false);
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(gameObject);
        }

    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

}
