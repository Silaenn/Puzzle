using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    RectTransform rectTransform;
    Vector3 startPosition;
    bool isCorrect = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startPosition = rectTransform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isCorrect) return;
        AudioManager.instance.PlayDragSound();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isCorrect) return;
        rectTransform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isCorrect) return;

        GameObject target = GameObject.Find(gameObject.name + "_Target");
        if (target != null && Vector3.Distance(rectTransform.position, target.transform.position) < 50f)
        {
            AudioManager.instance.PlayDropSound();
            rectTransform.position = target.transform.position;
            rectTransform.rotation = target.transform.rotation;
            target.SetActive(false);
            isCorrect = true;

            PuzzleManager.instance.TambahBenar();
        }
        else
        {
            LeanTween.moveX(gameObject, startPosition.x + 10f, 0.1f)
            .setLoopPingPong(2)
            .setOnComplete(() => rectTransform.position = startPosition);
            AudioManager.instance.PlayWrongSound();
            rectTransform.position = startPosition;
        }
    }
}
