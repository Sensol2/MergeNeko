using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FeverTimeUI : MonoBehaviour
{
    [SerializeField] private float duration = 1f; // Animation duration
    [SerializeField] private Vector3 targetPosition; // The position where the banner will move to
    [SerializeField] private float delay = 2f; // Delay before the banner starts moving back

    private Vector3 initialPosition; // Initial position of the banner

    void Start()
    {
        initialPosition = transform.position;
        ScoreManager.onFeverTimeStart += FeverAnimation;
    }

    public void FeverAnimation()
    {
        transform.DOMove(targetPosition, duration).SetEase(Ease.OutElastic).
            OnComplete(() => transform.DOMove(initialPosition, duration).SetEase(Ease.InOutQuint).SetDelay(delay));
    }

}