using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public Text levelText, hpText, pesosText, upgradeCostText, xpText;
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
        charSelectionSprite.sprite =
            GameManager.instance.playerSprites[currentCharSelection];
    }

    // weapon upgrade
    public void OnWpnUpgradeClick() {
        // TODO: implement
    }

    // update chara info
    public void UpdateMenu() {
        // TODO: weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[0];
        upgradeCostText.text = "IMPLEMENT THIS";

        // TODO: implement level
        levelText.text = "IMPLEMENT THIS";
        hpText.text = GameManager.instance.player.currentHp.ToString() +
            "/" + GameManager.instance.player.maxHp.ToString();
        // TODO: implement pesos
        pesosText.text = GameManager.instance.pesos.ToString();

        // TODO: xp bar
        xpText.text = "IMPLEMENT THIS";
        xpBar.localScale = new Vector3(0.5f, 1, 1);

    }
}
