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
