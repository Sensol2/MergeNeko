using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CatController : MonoBehaviour
{
    static public CatController instance;

    GameObject spawnedCat;
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
            spawnedCat = Instantiate(cats[Random.Range(0, maxLevel)], new Vector3(0, 4, 0), Quaternion.identity);
        }
        else
        {
            spawnedCat = Instantiate(cats[Random.Range(0, 4)], new Vector3(0, 4, 0), Quaternion.identity);
        }

        // 생성될때 사이즈 커지게
        var originScale = spawnedCat.transform.localScale;
        spawnedCat.transform.localScale = new Vector3(0, 0, 0);
        spawnedCat.transform.DOScale(originScale, scaleDelay);

        spawnedCat.GetComponent<CircleCollider2D>().enabled = false;
        spawnedCat.GetComponent<Rigidbody2D>().isKinematic = true;
    }

    void MoveCat() 
    {
        if (spawnedCat == null) 
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 newPos = new Vector3(mousePos.x, spawnedCat.transform.position.y, 0);
        spawnedCat.transform.position = newPos;
    }
    private void DropCat()
    {
        if (spawnedCat == null)
            return;

        //마우스버튼을 떼면 gravity scale에 값을 넣어 아래로 떨어지게 한다.
        spawnedCat.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -dropSpeed, 0);
        spawnedCat.GetComponent<CircleCollider2D>().enabled = true;
        spawnedCat.GetComponent<Rigidbody2D>().isKinematic = false;
        spawnedCat.GetComponent<Cat>().isNewCat = true;

        spawnedCat = null;

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
