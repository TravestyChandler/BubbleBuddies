using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour {
    public Text GameStartTimerText;
    public static UIController Instance;
    public Text GameTimer;
    public Text SuccessText;
    public Text GameFailureText;
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
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(0);
        }
    }

    public void RunGameStartTimer()
    {
        StartCoroutine(GameStartTimer());
    }
    public void RunGameLoss() {
        StartCoroutine(GameLossRoutine());
    }

    public IEnumerator GameLossRoutine() {
        GameFailureText.transform.localScale = new Vector3(0f, 0f, 0f);
        GameFailureText.text = "You Lose!";

        float timer = 0f;
        float totalTime = 0.5f;
        while (timer < totalTime) {
            timer += Time.deltaTime;
            GameFailureText.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timer / totalTime);
            yield return null;
        }
        yield return null;
    }
    public void RunGameWin()
    {
        StartCoroutine(GameWinRoutine());
    }

    IEnumerator GameWinRoutine()
    {
        SuccessText.transform.localScale = new Vector3(0f, 0f, 0f);
        SuccessText.text = "Congratulations!";

        float timer = 0f;
        float totalTime = 0.5f;
        while (timer < totalTime) {
            timer += Time.deltaTime;
            SuccessText.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timer / totalTime);
            yield return null;
        }
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
