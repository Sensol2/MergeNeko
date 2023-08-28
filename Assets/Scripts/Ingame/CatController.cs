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

        // �����ɶ� ������ Ŀ����
        var originScale = newCat.transform.localScale;
        newCat.transform.localScale = new Vector3(0, 0, 0);
        newCat.transform.DOScale(originScale, scaleDelay);

        newCat.GetComponent<CircleCollider2D>().enabled = false;
        newCat.GetComponent<Rigidbody2D>().isKinematic = true;

        // ����̰� ������ �� ȣ��Ǵ� �ڵ�
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

        //���콺��ư�� ���� gravity scale�� ���� �־� �Ʒ��� �������� �Ѵ�.
        newCat.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -dropSpeed, 0);
        newCat.GetComponent<CircleCollider2D>().enabled = true;
        newCat.GetComponent<Rigidbody2D>().isKinematic = false;
        newCat.GetComponent<Cat>().isNewCat = true;

        newCat = null;

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
