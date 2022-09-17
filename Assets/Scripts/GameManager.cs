using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    private void Awake() {
        // if instange of game manager already exists, destroy the new
        // one before returning
        if (instance != null) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        // load state when scene loaded
        SceneManager.sceneLoaded += LoadState;
        // persist game manager between scenes
        DontDestroyOnLoad(gameObject);
    }

    // game resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int>    weaponPrices;
    public List<int>    xpTable;

    // refs
    public Player player;
    // public Weapon weapon etc

    // trackers
    public int pesos;
    public int xp;

    // save state
    public void SaveState() {
        Debug.Log("Save State");
    }

    public void LoadState(Scene scene, LoadSceneMode mode) {
        Debug.Log("Load State");
    }
}
