using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverZone : MonoBehaviour
{
    private float timeInZone = 0f;
    private bool catInZone = false;

    public delegate void GameOverEventHandler();
    public static event GameOverEventHandler OnGameOver;

	void FixedUpdate()
    {
        if (catInZone)
        {
            if (ScoreManager.instance.isFeverTime)
                return;

            timeInZone += Time.deltaTime; // Increase timeInZone if cat is in the zone

            if (timeInZone >= 3f)
            {
                // 게임오버
                Debug.Log("Game Over");
                TriggerGameOver();
            }
        }
        else
        {
            // Reset timeInZone if cat is not in the zone
            timeInZone = 0f;
        }
    }

    public void TriggerGameOver()
    {
        OnGameOver?.Invoke();
        ResetGameOverEvent();
        //ScoreManager.instance.ResetFeverEvent();
    }

    public void ResetGameOverEvent()
    {
        OnGameOver = null;
    }


    private int catCount = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cat")
        {
            catCount++;
            catInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cat")
        {
            catCount--;

            if (catCount <= 0)
            {
                catInZone = false;
                catCount = 0; // 이 코드는 catCount가 음수가 되는 것을 방지합니다.
            }
        }
    }

}
