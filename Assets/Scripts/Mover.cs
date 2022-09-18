using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter {
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;

    protected virtual void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input) {
        // reset move delta
        moveDelta = input;

        // change sprite direction depending on move direction
        if (moveDelta.x > 0) {
            transform.localScale = Vector3.one;
        } else if (moveDelta.x < 0) {
            transform.localScale = new Vector3(-1, 1, 1);
        } // else not moving on x axis, don't want to change direction

        hit = Physics2D.BoxCast(
            transform.position,                         // in our current pos
            boxCollider.size,                           // with the collider size
            0,                                          // rotation is 0, we don't rotate
            new Vector2(0, moveDelta.y),                // direction - one axis at a time, y first
            Mathf.Abs(moveDelta.y * Time.deltaTime),    // distance
            LayerMask.GetMask("Character", "Blocking")  // which layers we test this against
            );

        // didn't collide with anything vertically, move
        if (hit.collider == null) {
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(
            transform.position,                         // in our current pos
            boxCollider.size,                           // with the collider size
            0,                                          // rotation is 0, we don't rotate
            new Vector2(moveDelta.x, 0),                // direction - x axis
            Mathf.Abs(moveDelta.x * Time.deltaTime),    // distance
            LayerMask.GetMask("Character", "Blocking")  // which layers we test this against
            );

        // didn't collide with anything horizontally, move
        if (hit.collider == null) {
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
