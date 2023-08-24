using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public SceneTransition sceneTransition;


	public void MoveToTitle()
	{
		StartCoroutine(LoadSceneCoroutine("Title"));
	}

	public void MoveToIngame()
	{
		StartCoroutine(LoadSceneCoroutine("Ingame"));
	}

	public void MoveToIngameTimeLimit()
	{
		StartCoroutine(LoadSceneCoroutine("Ingame_TimeLimit"));
	}

	public void MoveToShop()
	{
		StartCoroutine(LoadSceneCoroutine("Shop"));
	}

	public void MoveToCollection()
	{
		StartCoroutine(LoadSceneCoroutine("Collection"));
	}

    public void MoveToInventory()
    {
        StartCoroutine(LoadSceneCoroutine("Inventory"));
    }
    
	private IEnumerator LoadSceneCoroutine(string sceneName)
	{
		// Fade out
		sceneTransition.FadeOut();

		// Wait for the fade out to complete
		yield return new WaitForSeconds(sceneTransition.transitionTime);

		// Load the new scene
		SceneManager.LoadScene(sceneName);

		// Fade in
		sceneTransition.FadeIn();
	}
}
