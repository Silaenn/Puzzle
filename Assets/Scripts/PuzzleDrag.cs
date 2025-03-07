using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    RectTransform rectTransform;
    Vector3 startPosition;
    CanvasGroup canvasGroup;
    [SerializeField] GameObject eyeAndMounth;
    bool isCorrect = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.position;
        canvasGroup = eyeAndMounth.GetComponent<CanvasGroup>();
        eyeAndMounth.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isCorrect) return;
        Debug.Log("Mulai Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isCorrect) return;
        rectTransform.position = Input.mousePosition; // Puzzle mengikuti mouse
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isCorrect) return;

        GameObject target = GameObject.Find(gameObject.name + "_Target");

        if (target != null && Vector3.Distance(rectTransform.position, target.transform.position) < 50f)
        {
            rectTransform.position = target.transform.position;
            rectTransform.rotation = target.transform.rotation;
            target.SetActive(false);
            isCorrect = true;

            PuzzleManager.instance.TambahBenar();
            Debug.Log("Benar!");
        }
        else
        {
            rectTransform.position = startPosition; // Balik ke posisi awal
            Debug.Log("Salah!");
        }
    }

    void ShowWithFade()
    {
        eyeAndMounth.SetActive(true);
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
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsed / duration);
            yield return null;
        }

        canvasGroup.alpha = 1;
    }
}
