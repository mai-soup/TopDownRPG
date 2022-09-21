using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover {
    public static Player instance;

    protected void Awake() {
        // persist object between scenes but without duplicates
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate() {
        // get delta x and y
        float dx = Input.GetAxisRaw("Horizontal");
        float dy = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(dx, dy, 0));
    }

    public void ChangeSprite(Sprite newSprite) {
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    public void OnLevelUp() {
        // TODO: add some indicator for the level up
        maxHp += 3;
        currentHp = maxHp;
    }

    public void SetLevel(int lvl) {
        for (int i = 0; i < lvl - 1; i++) {
            OnLevelUp();
        }
    }

    public void Heal(int amount) {
        if (amount <= 0) return;

        // heal but not over max xp
        if (currentHp + amount > maxHp) {
            currentHp = maxHp;
        } else {
            currentHp += amount;
        }

        GameManager.instance.OnHpChange();
    }

    public override void ReduceHp(int dmg) {
        base.ReduceHp(dmg);
        GameManager.instance.OnHpChange();
    }
}
