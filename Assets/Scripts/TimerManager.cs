using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance;
    public float waktu = 30f;
    public TextMeshProUGUI timerText;
    public bool isGameOver = false;
    public Animator waktuHabisAnimator;
    public Animator buttonPanelAnimator;
    public GameObject gameOverPanel;
    public GameObject buttonPanel;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!isGameOver)
        {
            waktu -= Time.deltaTime;
            timerText.text = waktu.ToString("F0");

            if (waktu <= 0)
            {
                waktu = 0;
                isGameOver = true;
                GameOver();
            }
        }
    }

    public void ResetTimer()
    {
        waktu = 30f;
        isGameOver = false;
    }

    public void StopTimer()
    {
        isGameOver = true;
    }

    void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            gameOverPanel.transform.SetAsLastSibling();
            waktuHabisAnimator.gameObject.SetActive(true);
            waktuHabisAnimator.SetTrigger("TimeOver");
            Invoke("ShowButtonPanel", 3f);
        }

        PuzzleManager.instance.OngameOver();
        // Time.timeScale = 0f;
    }

    public void RestartPuzzle()
    {
        ResetTimer();
        gameOverPanel.SetActive(false);
        buttonPanel.SetActive(false);
        // Time.timeScale = 1f;
    }

    void ShowButtonPanel()
    {
        waktuHabisAnimator.gameObject.SetActive(false);
        buttonPanel.SetActive(true);
        buttonPanelAnimator.SetTrigger("ShowButtons");
    }




}
