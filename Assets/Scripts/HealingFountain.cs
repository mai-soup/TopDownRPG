using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Collectable {
    private int healingAmount = 5;
    public Sprite emptyFountainSprite;
    // TODO: needs animations!

    protected override void OnCollect() {
        if (!isCollected) {
            base.OnCollect();
            GameManager.instance.player.Heal(healingAmount);
            this.transform.Find("FountainBody").
                GetComponent<SpriteRenderer>().sprite = emptyFountainSprite;
            GameManager.instance.ShowText("+" + healingAmount + " HP",
                16,                 // size
                Color.green,
                transform.position, // position - of the fountain in question
                Vector3.up * 24,    // direction - up, 48 screen px/s
                1.25f                // duration - 3.0 seconds
                );
        }
    }
}
