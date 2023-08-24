using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    public Slider slider; // 슬라이더 변수 선언
    public TextMeshProUGUI timerText; // TextMeshProUGUI를 사용하려면 TextMeshProUGUI 변수 선언
    public float timeLeft = 60f; // 1분에 걸쳐서 줄어들게 설정
    public GameOverZone gameOverZone;
    void Start()
    {
        slider.maxValue = timeLeft; // 슬라이더의 최대 값을 설정
        slider.value = timeLeft; // 슬라이더의 현재 값을 설정
        UpdateTimerText(); // 타이머 텍스트 업데이트
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime; // 시간 감소
            slider.value = timeLeft; // 슬라이더 값을 업데이트
            UpdateTimerText(); // 타이머 텍스트 업데이트
        }
        else 
        {
            gameOverZone.TriggerGameOver();
            this.gameObject.SetActive(false);
        }
    }

    void UpdateTimerText()
    {
        int minutes = (int)(timeLeft / 60); // 분 계산
        int seconds = (int)(timeLeft % 60); // 초 계산
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // 텍스트 형식 지정
    }

    public void AddTime(float time)
    {
        this.timeLeft += time;
    }
}