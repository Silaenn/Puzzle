using System.Collections;
using UnityEngine;

public class PuzzleFade : MonoBehaviour
{
    CanvasGroup canvasGroup;

    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowWithFade()
    {
        gameObject.SetActive(true);
        canvasGroup.alpha = 0;
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / duration); // Peralihan mulus
            yield return null;
        }

        canvasGroup.alpha = 1;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
