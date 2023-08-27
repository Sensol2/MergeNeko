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

        // 이벤트 리스너 등록
        ScoreManager.onFeverTimeStart += EnterFeverTime;
        ScoreManager.onFeverTimeEnd += ExitFeverTime;
    }

    void Update()
    {
        if (ScoreManager.instance.isFeverTime)
        {
            // 핸드폰의 가속도 센서 데이터 가져오기
            acceleration = Input.acceleration;

            // 가속도 변경을 흔들림 임계값과 비교하여 확인
            if (acceleration.sqrMagnitude >= shakeThreshold * shakeThreshold)
            {
                // 흔들림이 감지되면 움직임 적용
                Vector2 force = new Vector2(acceleration.x, acceleration.y) * shakeForce;
                rb.AddForce(force);
            }
        }
    }

    // 피버타임 진입 시 중력 제거
    void EnterFeverTime()
    {
        Debug.Log("피버타입 돌입!");
        rb.gravityScale = 0f;
        rb.AddForce(new Vector2(Random.Range(-5f, 5f), Random.Range(10f, 20f)));
    }

    // 피버타임 종료 시 중력 복구
    void ExitFeverTime()
    {
        rb.gravityScale = CatController.instance.GravityScale;
    }

    void OnDestroy()
    {
        // 이벤트 리스너 해제
        ScoreManager.onFeverTimeStart -= EnterFeverTime;
        ScoreManager.onFeverTimeEnd -= ExitFeverTime;
    }
}
