using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShakeManager : MonoBehaviour
{
    public float shakeForce = 10f;
    public float shakeThreshold = 1f;

    public GameObject backgroundImage; // 배경 이미지
    public float shakeDuration = 1f; // 흔들림 효과 지속 시간
    public float shakeStrength = 1f; // 흔들림 효과 강도
    public int shakeVibrato = 10; // 흔들림 효과의 진동 횟수
    public float shakeRandomness = 90; // 흔들림 효과의 무작위성

    private Rigidbody2D rb;
    private Vector3 acceleration;

    void Update()
    {
        if (ScoreManager.instance.isFeverTime)
        {
            // 핸드폰의 가속도 센서 데이터 가져오기
            acceleration = Input.acceleration;

            // 가속도 변경을 흔들림 임계값과 비교하여 확인
            if (acceleration.sqrMagnitude >= shakeThreshold * shakeThreshold)
            {
                // 흔들림 효과 적용
                backgroundImage.transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
            }
        }
    }
}
