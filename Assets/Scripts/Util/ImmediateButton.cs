using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events; // �� ���ӽ����̽��� �߰��ؾ� �մϴ�.

public class ImmediateButton : MonoBehaviour, IPointerDownHandler
{
    // UnityEvent ����
    public UnityEvent onButtonDown;

    // IPointerDownHandler �������̽��� �޼���
    public void OnPointerDown(PointerEventData eventData)
    {
        // ��ư�� ������ �� UnityEvent ȣ��
        onButtonDown.Invoke();
    }
}
