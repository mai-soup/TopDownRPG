using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable {
    protected override void OnCollect() {
        if (!isCollected) {
            base.OnCollect();
            Debug.Log("Give pesos");
        }
    }
}
