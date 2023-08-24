using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    public Slider slider; // �����̴� ���� ����
    public TextMeshProUGUI timerText; // TextMeshProUGUI�� ����Ϸ��� TextMeshProUGUI ���� ����
    public float timeLeft = 60f; // 1�п� ���ļ� �پ��� ����
    public GameOverZone gameOverZone;
    void Start()
    {
        slider.maxValue = timeLeft; // �����̴��� �ִ� ���� ����
        slider.value = timeLeft; // �����̴��� ���� ���� ����
        UpdateTimerText(); // Ÿ�̸� �ؽ�Ʈ ������Ʈ
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime; // �ð� ����
            slider.value = timeLeft; // �����̴� ���� ������Ʈ
            UpdateTimerText(); // Ÿ�̸� �ؽ�Ʈ ������Ʈ
        }
        else 
        {
            gameOverZone.TriggerGameOver();
            this.gameObject.SetActive(false);
        }
    }

    void UpdateTimerText()
    {
        int minutes = (int)(timeLeft / 60); // �� ���
        int seconds = (int)(timeLeft % 60); // �� ���
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); // �ؽ�Ʈ ���� ����
    }

    public void AddTime(float time)
    {
        this.timeLeft += time;
    }
}