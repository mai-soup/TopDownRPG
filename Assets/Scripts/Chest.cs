using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable {
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
            GetComponent<SpriteRenderer>().sprite = emptyChestSprite;
            Debug.Log("Give " + pesosAmount + " pesos");
        }
    }
}
