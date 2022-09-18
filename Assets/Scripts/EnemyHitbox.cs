using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable {
    public int damage = 1;
    public float pushForce = 5.0f;

    protected override void OnCollide(Collider2D collider) {
        if (collider.tag == "Fighter" && collider.name == "Player") {
            // create dmg struct to send to player
            Damage dmg = new Damage {
                damage = damage,
                origin = transform.position,
                pushForce = pushForce
            };

            collider.SendMessage("ReceiveDamage", dmg);
        }
    }
}
