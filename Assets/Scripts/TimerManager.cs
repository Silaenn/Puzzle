using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance;
    public float waktu = 30f;
    public TextMeshProUGUI timerText;
    public bool isGameOver = false;
    public GameObject gamePanelSettings;
    public Animator waktuHabisAnimator;
    public Animator buttonPanelAnimator;
    public GameObject gameOverPanel;
    public GameObject buttonPanel;

    void Awake()
    {
        instance = this;
        gamePanelSettings.transform.SetSiblingIndex(gamePanelSettings.transform.parent.childCount - 1);
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

            waktuHabisAnimator.gameObject.transform.localScale = Vector3.zero;
            waktuHabisAnimator.gameObject.SetActive(true);
            waktuHabisAnimator.SetTrigger("TimeOver");
            AudioManager.instance.PlayGameOverSound();
            Invoke("ShowButtonPanel", 3f);
        }

        PuzzleManager.instance.OngameOver();
    }

    public void RestartPuzzle()
    {
        ResetTimer();
        gameOverPanel.SetActive(false);

        waktuHabisAnimator.gameObject.transform.localScale = Vector3.zero;
        waktuHabisAnimator.gameObject.SetActive(false);

        buttonPanel.transform.localScale = Vector3.zero;
        buttonPanel.SetActive(false);
    }

    void ShowButtonPanel()
    {
        waktuHabisAnimator.gameObject.SetActive(false);
        waktuHabisAnimator.gameObject.transform.localScale = Vector3.zero;

        buttonPanel.transform.localScale = Vector3.zero;
        buttonPanel.SetActive(true);
        buttonPanelAnimator.SetTrigger("ShowButtons");
    }




}
