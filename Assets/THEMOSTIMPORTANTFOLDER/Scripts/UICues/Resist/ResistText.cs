using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Random = UnityEngine.Random;

public class ResistText : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 3f;
    [SerializeField] private GameObject[] doorPoints;
    private void Start()
    {
        DOTween.Init();
        doorPoints[Random.Range(0, doorPoints.Length - 1)].SetActive(true);
    }

    public void Create(Transform parent)
    {
        GameObject resistPrefab = Resources.Load<GameObject>("ResistCanvas");
        GameObject resistText = Instantiate(resistPrefab, parent.position, quaternion.identity);

        ((RectTransform)resistText.transform).localPosition = Camera.main.WorldToScreenPoint(parent.position);
        Debug.Log(((RectTransform)resistText.transform).localPosition);
        Image textObject = resistText.GetComponentInChildren<Image>();
        textObject.DOFade(0f, fadeDuration).onComplete =
            () =>
            {
                Destroy(resistText);
            };
    }
}