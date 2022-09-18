using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable {
    // dmg struct
    public int damagePoints = 1;
    public float pushForce = 2.0f;

    // upgrades
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    // swing
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start() {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update() {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space)) {
            if (Time.time - lastSwing > cooldown) {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D collider) {
        // if the weapon hits a fighter thats not the player itself
        if (collider.tag == "Fighter" && collider.name != "Player") {
            // create a damage object to send to the hit fighter
            Damage dmg = new Damage {
                damage = damagePoints,
                origin = transform.position,
                pushForce = pushForce
            };

            collider.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing() {
        Debug.Log("Swoosh!");
    }
}
