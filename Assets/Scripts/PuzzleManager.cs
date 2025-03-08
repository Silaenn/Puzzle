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

    void Awake()
    {
        instance = this;
        SpawnPuzzle();
    }

    public void TambahBenar()
    {
        benar++;
        Debug.Log("Benar: " + benar);

        if (benar == parentKepingan.childCount - 1)
        {
            Debug.Log("Semua Kepingan Benar!");
            GameObject eyeAndMouth = GameObject.FindWithTag("EyeAndMouth");
            eyeAndMouth.GetComponent<PuzzleFade>().ShowWithFade();

            Invoke("NextPuzzle", 1f);
        }
    }

    void SpawnPuzzle()
    {
        if (indexPuzzle < puzzles.Length)
        {
            currentPuzzle = Instantiate(puzzles[indexPuzzle], parentCanvas);
            parentKepingan = currentPuzzle.transform.GetChild(1);
        }
        else
        {
            Debug.Log("Semua Puzzle Selesai!");
        }
    }

    void NextPuzzle()
    {
        benar = 0;
        indexPuzzle++;

        if (currentPuzzle != null)
        {
            Destroy(currentPuzzle);
        }

        SpawnPuzzle();
    }
}
