using System;
using System.Collections;
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

        string targetName = gameObject.name + "_Target";
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        GameObject closestTarget = null;
        float minDistance = 50f;

        foreach (GameObject obj in allObjects)
        {
            if (obj.name == targetName)
            {
                float distance = Vector3.Distance(rectTransform.position, obj.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTarget = obj;
                }
            }
        }

        if (closestTarget != null)
        {
            PlaceOnTarget(closestTarget);
        }
        else
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        LeanTween.moveX(gameObject, startPosition.x + 10f, 0.1f)
            .setLoopPingPong(2)
            .setOnComplete(() => rectTransform.position = startPosition);
        AudioManager.instance.PlayWrongSound();
        rectTransform.position = startPosition;
    }

    private void PlaceOnTarget(GameObject closestTarget)
    {
        AudioManager.instance.PlayDropSound();

        UnityEngine.UI.Image targetImage = closestTarget.GetComponent<UnityEngine.UI.Image>();
        UnityEngine.UI.Image currentImage = gameObject.GetComponent<UnityEngine.UI.Image>();

        if (targetImage != null && currentImage != null)
        {
            targetImage.sprite = currentImage.sprite;
            closestTarget.GetComponent<Outline>().enabled = false;
        }

        rectTransform.position = closestTarget.transform.position;
        rectTransform.rotation = closestTarget.transform.rotation;

        gameObject.SetActive(false);
        isCorrect = true;

        PuzzleManager.instance.TambahBenar();
    }
}