using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManageAnalytics : MonoBehaviour
{
    public GameAnalytics analyticsObj;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GameAnalytics.Initialize();
        SceneManager.LoadScene("Title");
    }
}
