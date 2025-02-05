using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIFeedBack : MonoBehaviour
{
    [SerializeField] private string DisplayText;
    private TextMeshProUGUI textObject;
    [SerializeField] private float fadeDuration = 2f;
    private void Start()
    {
        textObject = GetComponent<TextMeshProUGUI>();
        textObject.text = DisplayText;
    }

    public void Feedback()
    {
        DOTween.ToAlpha(() => textObject.color, (inColor) => textObject.color = inColor, 0, fadeDuration);
        
        DOTween.To(() => transform.localPosition, (inPosition) => transform.localPosition = inPosition, Vector3.up * 10f, fadeDuration).onComplete =
            () =>
            {
                transform.localPosition = Vector3.zero;
            };
    }
}
