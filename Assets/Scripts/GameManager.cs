using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public enum GameStatus
    {
        TimeFailure,
        Success,
        Playing,
        DeathFailure,
        Starting,
        Invalid
    }

    public float gameTimer = 120f;
    public bool gameRunning = false;
    public static GameManager Instance;
    public List<PlayerController> players;

    // Use this for initialization
    void Start () {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
	    StartCoroutine(GameStartRoutine());
	}
	
    public IEnumerator GameStartRoutine()
    {
        players = new List<PlayerController>(FindObjectsOfType<PlayerController>());
        UIController.Instance.RunGameStartTimer();
        yield return new WaitForSeconds(3f);
        gameRunning = true;
    }

	// Update is called once per frame
	void Update () {
        if (gameRunning)
        {
            gameTimer -= Time.deltaTime;
            GameStatus stat = CheckStatus();
            if (stat != GameStatus.Playing && stat != GameStatus.Starting)
            {
                gameRunning = false;
                Debug.Log("Game over");
                if (stat == GameStatus.Success)
                {
                    UIController.Instance.RunGameWin();
                }
                else if(stat == GameStatus.DeathFailure || stat == GameStatus.TimeFailure) {
                    UIController.Instance.RunGameLoss();
                }
            }
        }
       
        
	}

    public GameStatus CheckStatus()
    {
        if (gameRunning)
        {
            int playersAboveSurface = 0;
            foreach (PlayerController p in players)
            {
                if (p.aboveWater == true)
                {
                    playersAboveSurface++;
                }
                if (!p.alive)
                {
                    return GameStatus.DeathFailure;
                }
            }
            if (gameTimer <= 0f)
            {
                return GameStatus.TimeFailure;
            }

            else if (playersAboveSurface == players.Count)
            {
                return GameStatus.Success;
            }
            else
            {
                return GameStatus.Playing;
            }
        }
        else
        {
            return GameStatus.Starting;
        }
    }
}
