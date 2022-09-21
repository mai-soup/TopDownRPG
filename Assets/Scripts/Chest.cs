using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable {
    // TODO: give chests a chance to be mimics.
    public Sprite emptyChestSprite;
    public int pesosAmount;

    protected override void Start() {
        base.Start();
        // pesos amount is random between 5 and 10 inclusive
        pesosAmount = Random.Range(5, 11);
    }

    protected override void OnCollect() {
        if (!isCollected) {
            base.OnCollect();
            GameManager.instance.pesos += pesosAmount;
            GetComponent<SpriteRenderer>().sprite = emptyChestSprite;
            GameManager.instance.ShowText(pesosAmount + " pesos",
                16,                 // size
                Color.yellow,
                transform.position, // position - of the chest in question
                Vector3.up * 24,    // direction - up, 48 screen px/s
                1.25f                // duration - 3.0 seconds
                );
        }
    }
}
