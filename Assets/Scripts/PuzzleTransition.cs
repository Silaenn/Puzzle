using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PuzzleTransition : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    public void FadeIn()
    {
        StartCoroutine(FadeCanvas(0, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvas(1, 0));
    }

    IEnumerator FadeCanvas(float start, float end)
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, time);
            yield return null;
        }
    }
}
