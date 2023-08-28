using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events; // 이 네임스페이스를 추가해야 합니다.

public class ImmediateButton : MonoBehaviour, IPointerDownHandler
{
    // UnityEvent 선언
    public UnityEvent onButtonDown;

    // IPointerDownHandler 인터페이스의 메서드
    public void OnPointerDown(PointerEventData eventData)
    {
        // 버튼이 눌렸을 때 UnityEvent 호출
        onButtonDown.Invoke();
    }
}
