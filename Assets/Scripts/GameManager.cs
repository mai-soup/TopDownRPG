using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public static readonly string SAVE_PESOS = "Pesos";
    public static readonly string SAVE_SKIN_PREF = "Skin";
    public static readonly string SAVE_XP = "XP";
    public static readonly string SAVE_WEAPON_LVL = "WeaponLvl";
    public static readonly string SAVE_EXISTS = "Saved";

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
        // save state to player prefs
        PlayerPrefs.SetInt(SAVE_SKIN_PREF, 0);
        PlayerPrefs.SetInt(SAVE_PESOS, pesos);
        PlayerPrefs.SetInt(SAVE_XP, xp);
        PlayerPrefs.SetInt(SAVE_WEAPON_LVL, 0);
        // extra key to assert that some data is saved
        PlayerPrefs.SetInt(SAVE_EXISTS, 1);
    }

    public void LoadState(Scene scene, LoadSceneMode mode) {
        // if no save data exists, theres nothing to load
        if (!PlayerPrefs.HasKey(SAVE_EXISTS))
            return;

        // skin = prefs load
        pesos = PlayerPrefs.GetInt(SAVE_PESOS);
        xp = PlayerPrefs.GetInt(SAVE_XP);
        // weapon = prefs load
    }
}
