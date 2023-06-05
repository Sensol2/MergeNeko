using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboManager : MonoBehaviour
{
    static public ComboManager instance;
	public GameObject comboText;
	public GameObject canvas;
	public ProgressBar progressBar;
	public int maxCombo = 0;

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	public void MakeCombo(int combo, Vector3 pos)
	{
		if (maxCombo < combo)
			maxCombo = combo;
		GenerateComboText(combo, pos);
	}

	public void GenerateComboText(int combo, Vector3 pos)
	{
		GameObject obj = Instantiate(comboText, pos, Quaternion.identity, canvas.transform);
		TMP_Text textMesh = obj.GetComponent<TMP_Text>();
		if (combo == 1)
		{
			textMesh.fontSize = 32;
			textMesh.text = "GOOD!";
			progressBar.AddTime(0.1f);
		}
		if (combo == 2)
		{
			textMesh.fontSize = 32;
			textMesh.text = "GREAT!";
			progressBar.AddTime(0.5f);
		}
		else if(combo == 3)
		{
			textMesh.fontSize = 42;
			textMesh.text = "AWESOME!";
			progressBar.AddTime(1.0f);
		}
		else if(combo == 4)
		{
			textMesh.fontSize = 52;
			textMesh.text = "EXCELLENT!";
			progressBar.AddTime(1.5f);
		}
		else if (combo >= 5)
		{
			textMesh.fontSize = 56;
			textMesh.text = "UNBELIEVABLE!";
			progressBar.AddTime(2.0f);
		}
	}

}
