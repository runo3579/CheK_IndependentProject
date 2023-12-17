using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Button buttonToAnimate; 
    public float scaleFactor = 1.2f; 
    public float animationDuration = 0.5f;
    private Vector3 originalScale;
    public string sceneToLoad = "GamePlay"; 

    void Start()
    {
        Time.timeScale = 1;
        originalScale = buttonToAnimate.transform.localScale;
        StartCoroutine(LoopedScaleAnimation());

    }

    private System.Collections.IEnumerator LoopedScaleAnimation()
    {
        while (true)
        {
            yield return ScaleButton(scaleFactor, animationDuration);
            yield return ScaleButton(1.0f, animationDuration);
        }
    }
    private System.Collections.IEnumerator ScaleButton(float targetScaleFactor, float speed)
    {
        float elapsedTime = 0f;
        Vector3 targetScale = originalScale + (originalScale * targetScaleFactor);

        while (elapsedTime < speed)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Sin(Mathf.PI * (elapsedTime / speed));

            buttonToAnimate.transform.localScale = Vector3.Lerp(originalScale, targetScale, t);

            yield return null;
        }

        buttonToAnimate.transform.localScale = targetScale;
    }


    public void LoadAnotherScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
