using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ComboText : MonoBehaviour
{
    private TMP_Text textMesh;

    public float growDuration = 0.5f; // 성장에 걸리는 시간
    public float displayDuration = 1.0f; // 텍스트 유지시간
    public float fadeDuration = 1.0f; // 페이드아웃에 걸리는 시간

    public Vector3 endScale = new Vector3(1, 1, 1); // 텍스트의 최종 크기


    private void Awake()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        // 처음에는 텍스트를 작게 시작합니다.
        transform.localScale = Vector3.zero;

        // 텍스트를 커지게 만듭니다.
        transform.DOScale(endScale, growDuration).SetEase(Ease.OutBounce);

        // 텍스트를 페이드아웃 시킵니다.
        textMesh.DOFade(0, fadeDuration).SetDelay(growDuration).SetDelay(displayDuration).OnComplete(()=> 
        {
            // 페이드아웃이 끝나면 오브젝트를 삭제합니다.
            Destroy(gameObject, growDuration + fadeDuration);
        });

    }
}
