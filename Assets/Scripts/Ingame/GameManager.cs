using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public GameObject settingPanel;
    public bool isOnSetting;

	public float spentTime = 0f;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        // 프레임 30프레임 고정
        Application.targetFrameRate = 60;
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

    public void OpenSettingPanel()
    {
        settingPanel.SetActive(true);
        isOnSetting = true;
    }

    public void CloseSettingPanel()
    {
        StartCoroutine(DelayedCloseSettingPanel());
    }

    private IEnumerator DelayedCloseSettingPanel()
    {
        settingPanel.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        isOnSetting = false;
    }
}
