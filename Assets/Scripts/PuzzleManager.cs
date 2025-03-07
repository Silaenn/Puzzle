using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;
    public Transform parentKepingan;
    int benar = 0;

    private void Awake()
    {
        instance = this;
    }

    public void TambahBenar()
    {
        benar++;
        if (benar == parentKepingan.childCount)
        {
            Debug.Log("Semua Kepingan Benar!");
            // GameManager.instance.PuzzleCorrect();
        }
    }
}
