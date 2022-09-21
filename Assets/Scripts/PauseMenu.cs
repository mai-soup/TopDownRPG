using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    // TODO: also open upon hitting ESC
    public Text levelText, hpText, pesosText, upgradeCostText, xpText;
    public Button upgradeCostBtn;
    private int currentCharSelection = 0;
    public Image charSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    // chara selection
    public void OnArrowClick(bool isRightArrow) {
        if (isRightArrow) {
            currentCharSelection++;

            // end of array
            if (currentCharSelection ==
                GameManager.instance.playerSprites.Count) {
                currentCharSelection = 0;
            }

            OnSelectionChange();
        } else {
            currentCharSelection--;

            // end of array
            if (currentCharSelection < 0) {
                currentCharSelection =
                    GameManager.instance.playerSprites.Count - 1;
            }

            OnSelectionChange();
        }
    }

    private void OnSelectionChange() {
        // change sprite in menu
        Sprite newSprite = GameManager.instance.playerSprites[currentCharSelection];
        charSelectionSprite.sprite = newSprite;
        // change player sprite
        GameManager.instance.player.ChangeSprite(newSprite);
    }

    // weapon upgrade
    public void OnWpnUpgradeClick() {
        // if weapon upgrade can be done successfully, update menu
        if(GameManager.instance.TryUpgradeWeapon()) {
            UpdateMenu();
        }
    }

    // update chara info
    public void UpdateMenu() {
        weaponSprite.sprite = GameManager.instance.weaponSprites[
            GameManager.instance.weapon.weaponLevel];
        int nextPrice = Weapon.prices[
            GameManager.instance.weapon.weaponLevel];
        upgradeCostText.text = (nextPrice > 0) ? nextPrice.ToString() : "MAX";
        upgradeCostBtn.interactable = (nextPrice > 0);

        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hpText.text = GameManager.instance.player.GetCurrentHp().ToString() +
            "/" + GameManager.instance.player.maxHp.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();


        // if we are max level, display total xp
        int currentLevel = GameManager.instance.GetCurrentLevel();
        int totalXp = GameManager.instance.GetXp();
        int xpGoal = GameManager.instance.XpDiffNextLevel(currentLevel);
        int currentXp = totalXp - GameManager.instance.XpTotalByLevel(currentLevel - 1);
        if (currentLevel == GameManager.MAX_LEVEL) {
            xpText.text = totalXp.ToString() + 
                " XP";
            xpBar.localScale = Vector3.one;
        } else {
            // if we are not max level, show xp we have and xp to next lvl
            xpText.text = currentXp.ToString() + "/" + xpGoal + " XP";
            xpBar.localScale = new Vector3((float)currentXp / xpGoal, 1, 1);
        }

    }
}
