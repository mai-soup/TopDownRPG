using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Collectable {
    private int healingAmount = 5;

    protected override void OnCollect() {
        if (!isCollected) {
            base.OnCollect();
            GameManager.instance.player.Heal(healingAmount);
            GameManager.instance.ShowText("+" + healingAmount + " HP",
                16,                 // size
                Color.green,
                transform.position, // position - of the fountain in question
                Vector3.up * 24,    // direction - up, 48 screen px/s
                1.25f                // duration - 3.0 seconds
                );

            GetComponent<Animator>().SetTrigger("Empty");
        }
        // TODO: restore after some time?
    }
}
