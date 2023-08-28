using System.Collections.Generic;
using UnityEngine;

public class PhysicsController : MonoBehaviour
{
    static public PhysicsController instance;
    public float ShakeForceMultiplier;
    public List<Rigidbody2D> rigids = new List<Rigidbody2D>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        // 이벤트 리스너 등록
        ScoreManager.onFeverTimeStart += EnterFeverTime;
        ScoreManager.onFeverTimeEnd += ExitFeverTime;
    }

    public void AppendCat(Rigidbody2D rigid)
    {
        rigids.Add(rigid);
    }

    public void RemoveCat(Rigidbody2D rigid)
    {
        rigids.Remove(rigid);
    }

    public void ShakeRigidbodies(Vector3 deviceAcceleration)
    {
        foreach (var rigidbody in rigids)
        {
            if (rigidbody == null) //이거 어떻게 해결 못하나..?
                continue;
            rigidbody.AddForce(deviceAcceleration * ShakeForceMultiplier, ForceMode2D.Impulse);
        }
    }

    void EnterFeverTime()
    {
        Debug.Log("피버타입 돌입!" + rigids.Count);
        foreach (var rigidbody in rigids)
        {
            if (rigidbody == null)
                continue;
            rigidbody.gravityScale = 0f;
            rigidbody.AddForce(new Vector2(Random.Range(-5f, 5f), Random.Range(10f, 20f)));
        }
    }

    // 피버타임 종료 시 중력 복구
    void ExitFeverTime()
    {
        foreach (var rigidbody in rigids)
        {
            if (rigidbody == null)
                continue;
            rigidbody.gravityScale = CatController.instance.GravityScale;
        }
    }

    void OnDestroy()
    {
        // 이벤트 리스너 해제
        ScoreManager.onFeverTimeStart -= EnterFeverTime;
        ScoreManager.onFeverTimeEnd -= ExitFeverTime;
    }
}
