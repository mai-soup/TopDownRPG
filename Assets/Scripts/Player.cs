using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover {
    private void FixedUpdate() {
        // get delta x and y
        float dx = Input.GetAxisRaw("Horizontal");
        float dy = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(dx, dy, 0));
    }

    public void ChangeSprite(Sprite newSprite) {
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}
