using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    [SerializeField] private GameObject loaderCanvas;
    [SerializeField] private RectTransform progressBar;
    public static LevelManager instance;
    private float target;

    private void Awake() {
        // keep single public instance
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName) {
        await Task.Delay(50);
        // reset progress bar
        progressBar.localScale = new Vector3(0, 1, 1);

        Debug.Log("LOADING " + sceneName);
        // load scene and make sure it doesnt immediately activate
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        // show loader
        loaderCanvas.SetActive(true);

        // keep checking for load progress.
        // when disallowing scene activation in unity the scene is loaded at 90%
        // not 100, so thats the end condition
        do {
            await Task.Delay(50);
            target = scene.progress;
        } while (scene.progress < 0.9f);

        // so the loading screen doesnt just flicker if the load time is very short
        await Task.Delay(500);

        // activate scene and disable the canvas
        scene.allowSceneActivation = true;
        // delay to skip the last frame of last scene
        await Task.Delay(Mathf.CeilToInt(Time.deltaTime * 1000));
        loaderCanvas.SetActive(false);
    }

    private void Update() {
        if (loaderCanvas.activeSelf) {
            progressBar.localScale = new Vector3(
            Mathf.MoveTowards(progressBar.localScale.x, target, 3 * Time.deltaTime),
            1, 1);
        }
    }
}
