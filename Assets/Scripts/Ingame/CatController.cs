using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum DropType
{
    CAT,
    BOMB,
    TUNABITZ,
}


public class CatController : MonoBehaviour
{
    static public CatController instance;

    GameObject newObject;
    public GameObject[] cats;
    public float dropSpeed;
    public float GravityScale;
    public float generateDelay;
    public float scaleDelay;

    //���� ������
    public GameItem bombItem;
    public GameItem tunaBitzItem;
    public GameObject bomb;
    public GameObject tuna;

    int bombSpawnRate;
    int tunaBitzSpawnRate;
    bool hasBombItem;
    bool hasTunaBitzItem;

    private void Awake()
	{
        if (instance == null)
            instance = this;
	}

	void Start()
    {
        InitItemValue();
        GenerateObject();
        GameOverZone.OnGameOver += OnDisableWhenGameOver;
    }
    
    void Update()
    {
        if (GameManager.instance.isOnSetting)
            return;
        

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            MoveObject();
        }
        if (Input.GetMouseButtonUp(0))
        {
            DropObject();
        }
    }

    void InitItemValue() //���� Ȯ�� �����Ϳ��� �޾ƿ���
    {
        bombSpawnRate = DataManager.instance.GetItemLevel(bombItem.KEY);
        tunaBitzSpawnRate = DataManager.instance.GetItemLevel(tunaBitzItem.KEY);

        hasBombItem = DataManager.instance.HasItem(bombItem.KEY);
        hasTunaBitzItem = DataManager.instance.HasItem(tunaBitzItem.KEY);
    }

    void GenerateObject() //���� Ȯ���� ���� ����
    {
        bombSpawnRate = hasBombItem ? bombSpawnRate : 0;
        tunaBitzSpawnRate = hasTunaBitzItem ? tunaBitzSpawnRate : 0;

        int randomValue = UnityEngine.Random.Range(0, 100);

        if (randomValue < bombSpawnRate && hasBombItem)
        {
            Generate(DropType.BOMB);
        }
        else if (randomValue < bombSpawnRate + tunaBitzSpawnRate && hasTunaBitzItem)
        {
            Debug.Log("TUNABITZ ����");
            Generate(DropType.TUNABITZ);
        }
        else
        {
            Generate(DropType.CAT);
        }
    }

    void Generate(DropType dropType)
    {
        switch (dropType)
        {
            case DropType.CAT:
                int maxLevel = LevelManager.instance.GetLevel();

                if (maxLevel <= 4)
                    newObject = Instantiate(cats[Random.Range(0, maxLevel)], new Vector3(0, 4, 0), Quaternion.identity);
                else
                    newObject = Instantiate(cats[Random.Range(0, 4)], new Vector3(0, 4, 0), Quaternion.identity);

                Rigidbody2D catRigidbody = newObject.GetComponent<Rigidbody2D>();
                PhysicsController.instance.AppendCat(catRigidbody);
                break;
            case DropType.BOMB:
                newObject = Instantiate(bomb, new Vector3(0, 4, 0), Quaternion.identity);
                break;
            case DropType.TUNABITZ:
                newObject = Instantiate(tuna, new Vector3(0, 4, 0), Quaternion.identity);
                break;
        }


        // �����ɶ� ������ Ŀ����
        var originScale = newObject.transform.localScale;
        newObject.transform.localScale = new Vector3(0, 0, 0);
        newObject.transform.DOScale(originScale, scaleDelay);

        // ���� OFF, ���������Ȱ� �ƴϹǷ�
        newObject.GetComponent<CircleCollider2D>().enabled = false;
        newObject.GetComponent<Rigidbody2D>().isKinematic = true;


    }

    void MoveObject() 
    {
        if (newObject == null) 
            return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 newPos = new Vector3(mousePos.x, newObject.transform.position.y, 0);
        newObject.transform.position = newPos;
    }

    private void DropObject()
    {
        if (newObject == null)
            return;

        //���콺��ư�� ���� gravity scale�� ���� �־� �Ʒ��� �������� �Ѵ�.
        newObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -dropSpeed, 0);
        newObject.GetComponent<CircleCollider2D>().enabled = true;
        newObject.GetComponent<Rigidbody2D>().isKinematic = false;

        //newObject.GetComponent<Cat>().isNewCat = true;

        newObject = null;

        // 1�� �Ŀ� GenerateCat() �ڷ�ƾ�� ȣ���մϴ�.
        StartCoroutine(GenerateCatWithDelay(generateDelay));
    }

    IEnumerator GenerateCatWithDelay(float delay)
    {
        // ������ �ð� ���� ����մϴ�.
        yield return new WaitForSeconds(delay);

        // ��� �ð��� ���� �Ŀ� GenerateObject() �޼��带 ȣ���մϴ�.
        GenerateObject();
    }

    public void OnDisableWhenGameOver()
    {
        GameOverZone.OnGameOver -= OnDisableWhenGameOver;
        gameObject.SetActive(false);
    }
}
