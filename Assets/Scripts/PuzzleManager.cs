using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;
    public Transform parentKepingan;
    public GameObject[] puzzles;
    public Transform parentCanvas;
    GameObject currentPuzzle;
    int indexPuzzle = 0;
    int benar = 0;
    int lastPuzzeIndex;

    void Awake()
    {
        instance = this;
        SpawnPuzzle();
    }

    public void TambahBenar()
    {
        benar++;

        if (benar == parentKepingan.childCount - 1)
        {
            TimerManager.instance.StopTimer();
            AudioManager.instance.PlayCorrectSound();


            GameObject eyeAndMouth = GameObject.FindWithTag("EyeAndMouth");
            eyeAndMouth.GetComponent<PuzzleFade>().ShowWithFade();

            Invoke("NextPuzzle", 2f);
        }
    }

    public void SpawnPuzzle()
    {
        if (indexPuzzle < puzzles.Length)
        {
            currentPuzzle = Instantiate(puzzles[indexPuzzle], parentCanvas);
            currentPuzzle.transform.SetAsFirstSibling();
            parentKepingan = currentPuzzle.transform.GetChild(1);

            PuzzleTransition transition = currentPuzzle.GetComponent<PuzzleTransition>();
            if (transition != null)
            {
                transition.FadeIn();
            }
            lastPuzzeIndex = indexPuzzle;
        }
        else
        {
            Debug.Log("Semua Puzzle Selesai!");
        }
    }

    public void NextPuzzle()
    {
        benar = 0;
        indexPuzzle++;

        PuzzleTransition transition = currentPuzzle.GetComponent<PuzzleTransition>();
        if (transition != null)
        {
            transition.FadeOut();
        }

        if (currentPuzzle != null)
        {
            Destroy(currentPuzzle, 1f);
        }

        Invoke("SpawnPuzzle", 1f);
        TimerManager.instance.ResetTimer();
    }

    public void OngameOver()
    {
        lastPuzzeIndex = indexPuzzle;
    }

    public void RestartFromLastPuzzle()
    {
        benar = 0;
        indexPuzzle = lastPuzzeIndex;

        if (currentPuzzle != null)
        {
            Destroy(currentPuzzle);
        }

        SpawnPuzzle();
        TimerManager.instance.RestartPuzzle();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
