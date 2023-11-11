using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneTransition : MonoBehaviour
{
    public float transitionTime = 1f; // 페이드 인/아웃에 걸리는 시간

    public Image image;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void FadeIn()
    {
        image.DOFade(0, transitionTime).OnComplete(() => { Destroy(gameObject); });
    }

    public void FadeOut()
    {
        image.DOFade(1, transitionTime);
    }
}
