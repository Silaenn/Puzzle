using System;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

            UnityEngine.UI.Image targetImage = target.GetComponent<UnityEngine.UI.Image>();
            UnityEngine.UI.Image currentImage = gameObject.GetComponent<UnityEngine.UI.Image>();


            if (targetImage != null && currentImage != null)
            {
                targetImage.sprite = currentImage.sprite;
                target.GetComponent<Outline>().enabled = false; ;
            }

            rectTransform.position = target.transform.position;
            rectTransform.rotation = target.transform.rotation;

            gameObject.SetActive(false);
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
