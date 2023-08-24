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
        spentTime += Time.deltaTime; // �ð� ����
    }

    public string GetTimerText()
    {
        int minutes = (int)(spentTime / 60); // �� ���
        int seconds = (int)(spentTime % 60); // �� ���
        return string.Format("{0:00}:{1:00}", minutes, seconds); // �ؽ�Ʈ ���� ����
    }

}
