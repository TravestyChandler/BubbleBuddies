using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIController : MonoBehaviour {
    public Text GameStartTimerText;
    public static UIController Instance;
    public Text GameTimer;
    public Text SuccessText;
    // Use this for initialization
    void Start() {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
	}

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameRunning)
        {
            GameTimer.text = GameManager.Instance.gameTimer.ToString("0");
        }
    }

    public void RunGameStartTimer()
    {
        StartCoroutine(GameStartTimer());
    }

    public void RunGameWin()
    {
        StartCoroutine(GameWinRoutine());
    }

    IEnumerator GameWinRoutine()
    {
        SuccessText.text = "Congratulations!";
        yield return null;
    }

    IEnumerator GameStartTimer()
    {
        float timer = 3f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            GameStartTimerText.text = timer.ToString("0");
            yield return null;
        }
        GameStartTimerText.text = "";
    }
}
