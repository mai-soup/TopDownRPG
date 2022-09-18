using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour {
    public int currentHp = 10;
    public int maxHp = 10;
    public float pushRecoverySpeed = 0.2f;  // recovery time after being pushed
    protected float immuneTime = 1.0f;      // so hits cant just be spammed
    protected float lastImmune;
    protected Vector3 pushVector;           // direction and amount to push

    protected virtual void ReceiveDamage(Damage dmg) {
        // if not currently immune - get rekt
        if (Time.time - lastImmune > immuneTime) {
            lastImmune = Time.time;     // immune for the immunetime
            currentHp -= dmg.damage;    // reduce hp
            pushVector =                // calculate push direction, add force
                (transform.position - dmg.origin).normalized *
                dmg.pushForce;

            // show text with damage amount on the hit fighter
            GameManager.instance.ShowText(
                dmg.damage.ToString(),
                16,
                Color.red,
                transform.position,
                Vector3.zero,
                0.5f
                );

            if (currentHp <= 0) {
                currentHp = 0;
                Die();
            }
        }
    }

    protected virtual void Die() {

    }
}
