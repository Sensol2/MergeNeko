using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CatController : MonoBehaviour
{
    static public CatController instance;

    GameObject newCat;
    public GameObject[] cats;
    public float dropSpeed;
    public float GravityScale;
    public float generateDelay;
    public float scaleDelay;

	private void Awake()
	{
        if (instance == null)
            instance = this;
	}

	void Start()
    {
        GenerateCat();
        GameOverZone.OnGameOver += OnDisableWhenGameOver;
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            MoveCat();
        }
        if (Input.GetMouseButtonUp(0))
        {
            DropCat();
        }
    }

    void GenerateCat()
    {
        int maxLevel = LevelManager.instance.GetLevel();

        if (maxLevel <= 4)
        {
            newCat = Instantiate(cats[Random.Range(0, maxLevel)], new Vector3(0, 4, 0), Quaternion.identity);
        }
        else
        {
            newCat = Instantiate(cats[Random.Range(0, 4)], new Vector3(0, 4, 0), Quaternion.identity);
        }

        // 생성될때 사이즈 커지게
        var originScale = newCat.transform.localScale;
        newCat.transform.localScale = new Vector3(0, 0, 0);
        newCat.transform.DOScale(originScale, scaleDelay);

        newCat.GetComponent<CircleCollider2D>().enabled = false;
        newCat.GetComponent<Rigidbody2D>().isKinematic = true;

        // 고양이가 생성될 때 호출되는 코드
        Rigidbody2D catRigidbody = newCat.GetComponent<Rigidbody2D>();
        PhysicsController.instance.AppendCat(catRigidbody);
    }

    void MoveCat() 
    {
        if (newCat == null) 
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 newPos = new Vector3(mousePos.x, newCat.transform.position.y, 0);
        newCat.transform.position = newPos;
    }
    private void DropCat()
    {
        if (newCat == null)
            return;

        //마우스버튼을 떼면 gravity scale에 값을 넣어 아래로 떨어지게 한다.
        newCat.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -dropSpeed, 0);
        newCat.GetComponent<CircleCollider2D>().enabled = true;
        newCat.GetComponent<Rigidbody2D>().isKinematic = false;
        newCat.GetComponent<Cat>().isNewCat = true;

        newCat = null;

        // 1초 후에 GenerateCat() 코루틴을 호출합니다.
        StartCoroutine(GenerateCatWithDelay(generateDelay));
    }

    IEnumerator GenerateCatWithDelay(float delay)
    {
        // 지정된 시간 동안 대기합니다.
        yield return new WaitForSeconds(delay);

        // 대기 시간이 끝난 후에 GenerateCat() 메서드를 호출합니다.
        GenerateCat();
    }

    public void OnDisableWhenGameOver()
    {
        GameOverZone.OnGameOver -= OnDisableWhenGameOver;
        gameObject.SetActive(false);
    }
}
