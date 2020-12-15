using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameLoader : MonoBehaviour
{
    public GameObject loadingPanel;
    public Slider slider;

    public void StartGame()
    {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(2);
        loadingPanel.SetActive(true);

        while (!operation.isDone)
        {
            slider.value = operation.progress;

            yield return null;
        }
    }
}
