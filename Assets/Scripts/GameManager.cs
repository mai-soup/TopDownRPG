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

    protected static readonly float xpIncreaseRate = 2.4f;
    protected static readonly float xpConstant = 0.5f;
    // TODO: edit depending on the max level you decided on
    public static readonly int MAX_LEVEL = 10;

    private void Awake() {
        // if instange of game manager already exists, destroy the new
        // one before returning. we want a singleton to be able to access
        // anywhere in code.
        if (instance != null) {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextMgr.gameObject);
            Destroy(HUD.gameObject);
            return;
        }

        instance = this;
        // load state when scene loaded
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // game resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;

    // refs
    public Player player;
    public FloatingTextManager floatingTextMgr;
    public Weapon weapon;
    public RectTransform currentHpBar;
    public Canvas HUD;

    // logic
    public int pesos;
    [SerializeField] private int xp;

    public int GetXp() {
        return xp;
    }

    public void GrantXp(int xpToAdd) {
        int prevLevel = GetCurrentLevel();
        xp += xpToAdd;
        if (prevLevel < GetCurrentLevel()) {
            OnLevelUp();
        }
    }

    protected void OnLevelUp() {
        player.OnLevelUp();
    }

    public void OnHpChange() {
        float ratio = (float)player.GetCurrentHp() / (float)player.maxHp;
        currentHpBar.localScale = new Vector3(ratio, 1, 1);
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        MovePlayerToSpawn();
    }

    public void MovePlayerToSpawn() {
        // put player on spawn point
        player.transform.position =
            GameObject.Find("PlayerSpawnPoint").transform.position;
    }

    // we basically call the same function here so that it's available
    // everywhere via the game manager
    public void ShowText(string msg, int fontSize, Color color, Vector3 pos,
        Vector3 motion, float duration) {
        floatingTextMgr.Show(msg, fontSize, color, pos, motion, duration);
    }

    // weapon upgrade
    public bool TryUpgradeWeapon() {
        // is weapon already maxed?
        if (Weapon.prices.Length <= weapon.weaponLevel + 1) {
            // can't upgrade weapon, already max
            return false;
        }

        // do we have enough pesos to upgrade?
        if (pesos >= Weapon.prices[weapon.weaponLevel]) {
            pesos -= Weapon.prices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }

    // save state
    public void SaveState() {
        // save state to player prefs
        PlayerPrefs.SetInt(SAVE_SKIN_PREF, 0);
        PlayerPrefs.SetInt(SAVE_PESOS, pesos);
        PlayerPrefs.SetInt(SAVE_XP, xp);
        PlayerPrefs.SetInt(SAVE_WEAPON_LVL, weapon.weaponLevel);
        // extra key to assert that some data is saved
        PlayerPrefs.SetInt(SAVE_EXISTS, 1);
    }

    public void LoadState(Scene scene, LoadSceneMode mode) {
        // dont load data every time we switch scenes,
        // we just needed it for the first one
        SceneManager.sceneLoaded += LoadState;

        // if no save data exists, theres nothing to load
        if (!PlayerPrefs.HasKey(SAVE_EXISTS))
            return;

        pesos = PlayerPrefs.GetInt(SAVE_PESOS);
        xp = PlayerPrefs.GetInt(SAVE_XP);
        weapon.SetWeaponLevel(PlayerPrefs.GetInt(SAVE_WEAPON_LVL));

        // level up player correctly
        player.SetLevel(GetCurrentLevel());

        MovePlayerToSpawn();

        // update the hp bar
        OnHpChange();
    }

    public int XpDiffNextLevel(int currentLevel) {
        // TODO: account for max lvl
        float totalForNextLevel =
            Mathf.Pow((currentLevel / xpConstant), xpIncreaseRate);
        float totalForCurrentLevel =
            Mathf.Pow(((currentLevel - 1) / xpConstant), xpIncreaseRate);

        if (currentLevel == 1) totalForCurrentLevel = 0;

        return Mathf.FloorToInt(totalForNextLevel - totalForCurrentLevel);
    }

    public int XpTotalByLevel(int level) {
        // TODO: account for max lvl
        float totalForLevel =
            Mathf.Pow((level / xpConstant), xpIncreaseRate);

        return Mathf.FloorToInt(totalForLevel);
    }

    public int LevelFromXp(int currentXp) {
        // TODO: account for max lvl
        return Mathf.CeilToInt(xpConstant * 
                        Mathf.Pow(currentXp + 1, 1 / xpIncreaseRate));
    }

    public int GetCurrentLevel() {
        // TODO: account for max lvl
        return LevelFromXp(xp);
    }
}
