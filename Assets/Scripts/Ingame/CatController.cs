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

        // �����ɶ� ������ Ŀ����
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

        //���콺��ư�� ���� gravity scale�� ���� �־� �Ʒ��� �������� �Ѵ�.
        spawnedCat.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -dropSpeed, 0);
        spawnedCat.GetComponent<CircleCollider2D>().enabled = true;
        spawnedCat.GetComponent<Rigidbody2D>().isKinematic = false;
        spawnedCat.GetComponent<Cat>().isNewCat = true;

        spawnedCat = null;

        // 1�� �Ŀ� GenerateCat() �ڷ�ƾ�� ȣ���մϴ�.
        StartCoroutine(GenerateCatWithDelay(generateDelay));
    }

    IEnumerator GenerateCatWithDelay(float delay)
    {
        // ������ �ð� ���� ����մϴ�.
        yield return new WaitForSeconds(delay);

        // ��� �ð��� ���� �Ŀ� GenerateCat() �޼��带 ȣ���մϴ�.
        GenerateCat();
    }

    public void OnDisableWhenGameOver()
    {
        GameOverZone.OnGameOver -= OnDisableWhenGameOver;
        gameObject.SetActive(false);
    }
}
