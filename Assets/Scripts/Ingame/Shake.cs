//Shake.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public float shakeForce = 10f;
    public float shakeThreshold = 1f;

    private Rigidbody2D rb;
    private Vector3 acceleration;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // �̺�Ʈ ������ ���
        ScoreManager.onFeverTimeStart += EnterFeverTime;
        ScoreManager.onFeverTimeEnd += ExitFeverTime;
    }

    void Update()
    {
        if (ScoreManager.instance.isFeverTime)
        {
            // �ڵ����� ���ӵ� ���� ������ ��������
            acceleration = Input.acceleration;

            // ���ӵ� ������ ��鸲 �Ӱ谪�� ���Ͽ� Ȯ��
            if (acceleration.sqrMagnitude >= shakeThreshold * shakeThreshold)
            {
                // ��鸲�� �����Ǹ� ������ ����
                Vector2 force = new Vector2(acceleration.x, acceleration.y) * shakeForce;
                rb.AddForce(force);
            }
        }
    }

    // �ǹ�Ÿ�� ���� �� �߷� ����
    void EnterFeverTime()
    {
        Debug.Log("�ǹ�Ÿ�� ����!");
        rb.gravityScale = 0f;
        rb.AddForce(new Vector2(Random.Range(-5f, 5f), Random.Range(10f, 20f)));
    }

    // �ǹ�Ÿ�� ���� �� �߷� ����
    void ExitFeverTime()
    {
        rb.gravityScale = CatController.instance.GravityScale;
    }

    void OnDestroy()
    {
        // �̺�Ʈ ������ ����
        ScoreManager.onFeverTimeStart -= EnterFeverTime;
        ScoreManager.onFeverTimeEnd -= ExitFeverTime;
    }
}
