using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShakeManager : MonoBehaviour
{
    public float shakeForce = 10f;
    public float shakeThreshold = 1f;

    public GameObject backgroundImage; // ��� �̹���
    public float shakeDuration = 1f; // ��鸲 ȿ�� ���� �ð�
    public float shakeStrength = 1f; // ��鸲 ȿ�� ����
    public int shakeVibrato = 10; // ��鸲 ȿ���� ���� Ƚ��
    public float shakeRandomness = 90; // ��鸲 ȿ���� ��������

    private Rigidbody2D rb;
    private Vector3 acceleration;

    void Update()
    {
        if (ScoreManager.instance.isFeverTime)
        {
            // �ڵ����� ���ӵ� ���� ������ ��������
            acceleration = Input.acceleration;

            // ���ӵ� ������ ��鸲 �Ӱ谪�� ���Ͽ� Ȯ��
            if (acceleration.sqrMagnitude >= shakeThreshold * shakeThreshold)
            {
                // ��鸲 ȿ�� ����
                backgroundImage.transform.DOShakePosition(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness);
            }
        }
    }
}
