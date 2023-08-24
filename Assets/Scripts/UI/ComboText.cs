using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class ComboText : MonoBehaviour
{
    private TMP_Text textMesh;

    public float growDuration = 0.5f; // ���忡 �ɸ��� �ð�
    public float displayDuration = 1.0f; // �ؽ�Ʈ �����ð�
    public float fadeDuration = 1.0f; // ���̵�ƿ��� �ɸ��� �ð�

    public Vector3 endScale = new Vector3(1, 1, 1); // �ؽ�Ʈ�� ���� ũ��


    private void Awake()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        // ó������ �ؽ�Ʈ�� �۰� �����մϴ�.
        transform.localScale = Vector3.zero;

        // �ؽ�Ʈ�� Ŀ���� ����ϴ�.
        transform.DOScale(endScale, growDuration).SetEase(Ease.OutBounce);

        // �ؽ�Ʈ�� ���̵�ƿ� ��ŵ�ϴ�.
        textMesh.DOFade(0, fadeDuration).SetDelay(growDuration).SetDelay(displayDuration).OnComplete(()=> 
        {
            // ���̵�ƿ��� ������ ������Ʈ�� �����մϴ�.
            Destroy(gameObject, growDuration + fadeDuration);
        });

    }
}
