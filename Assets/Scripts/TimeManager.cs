using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    static public TimeManager instance;
	public float spentTime = 0f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Update()
    {
        spentTime += Time.deltaTime; // 시간 증가
    }

    public string GetTimerText()
    {
        int minutes = (int)(spentTime / 60); // 분 계산
        int seconds = (int)(spentTime % 60); // 초 계산
        return string.Format("{0:00}:{1:00}", minutes, seconds); // 텍스트 형식 지정
    }

}
