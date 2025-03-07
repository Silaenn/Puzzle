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
        Debug.Log(benar);
        if (benar == parentKepingan.childCount - 1)
        {
            GameObject eyeAndMouth = GameObject.FindWithTag("EyeAndMouth");
            Debug.Log("Semua Kepingan Benar!");

            eyeAndMouth.GetComponent<PuzzleFade>().ShowWithFade();
        }
    }
}
