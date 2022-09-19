using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover {
    public int xpValue = 1;
    public float triggerLength = 0.64f; // distance to notice player
    public float chaseLength = 1f;      // max distance to player to keep
                                        // chasing

    private bool isChasing = false;
    private bool isCollidingWithPlayer = false;
    private Transform playerTransform;
    private Vector3 startingPos;

    // enemys "weapon" hitbox
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];
    private ContactFilter2D filter;

    protected override void Start() {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPos = transform.position;   // set starting position to the
                                            // initial pos the enemy is put
                                            // on the map in the editor
        // first child of enemy is the hitbox sprite
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        // if not already chasing the player, check if 
        // a chase should be triggered
        if (!isChasing) {
            if (distance < triggerLength && !isCollidingWithPlayer) {
                // player's in trigger range but not colliding - move toward
                UpdateMotor((playerTransform.position - transform.position).normalized);
                isChasing = true;
            } else {
                // not in trigger range, go back to spawn point
                UpdateMotor((startingPos - transform.position).normalized);
            }
        } else {
            // is the player still within chasing range?
            if (distance < chaseLength) {
                // we aren't colliding with the player already, are we?
                if (!isCollidingWithPlayer) {
                // yes, move towards player
                UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            } else {
                // player escaped, go back to starting point
                UpdateMotor((startingPos - transform.position).normalized);
            }
        }

        // after all the movement, check for overlaps
        isCollidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++) {
            if (hits[i] == null) { continue; }

            if (hits[i].tag == "Fighter" && hits[i].name == "Player") {
                isCollidingWithPlayer = true;
                hits[i] = null;
                break;
            }

            hits[i] = null;
        }
    }

    protected override void Die() {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText(
            xpValue + "XP",
            16,
            Color.white,
            transform.position,
            Vector3.up * 32,
            1.0f
            );
    }
}
