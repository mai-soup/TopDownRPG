using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable {
    // dmg struct
    public static readonly int[] damagePoints =
        { 1,    2,    3,    4,    5,    6,    7,    10,   12 };
    public static readonly float[] pushForce = 
        { 2.0f, 2.1f, 2.2f, 2.4f, 2.6f, 3.0f, 3.3f, 3.6f, 4.0f };
    public static readonly int[] prices =
        { 50,   100,  160,  250,  375,  500,  750,  1000, -1 };

    // upgrades
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    // swing
    private float cooldown = 0.5f;
    private float lastSwing;
    private Animator animator;

    protected void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start() {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void Update() {
        base.Update();

        if (GameManager.instance.isPaused) return;

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
            // create a damage struct to send to the hit fighter
            Damage dmg = new Damage {
                damage = damagePoints[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            collider.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing() {
        animator.SetTrigger("Swing");
    }

    public void UpgradeWeapon() {
        weaponLevel++;
        spriteRenderer.sprite =
            GameManager.instance.weaponSprites[weaponLevel];
    }

    public void SetWeaponLevel(int level) {
        weaponLevel = level;
        spriteRenderer.sprite =
            GameManager.instance.weaponSprites[weaponLevel];
    }
}
