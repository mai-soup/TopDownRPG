using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable {
    protected bool isCollected;

    protected override void OnCollide(Collider2D collider) {
        if (collider.name == "Player") {
            // if the collider is the player, collect this item
            OnCollect();
        }
    }

    protected virtual void OnCollect() {
        isCollected = true;
    }
}
