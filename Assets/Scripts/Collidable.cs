using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour {
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10];

    protected virtual void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update() {
        // collision work

        // looks for something in collision with the current object,
        // puts it in the result array
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++) {
            if (hits[i] == null) { continue; }

            OnCollide(hits[i]);

            // clean the array up ourselves as it's not done automatically
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D collider) {
        // log what the object collided with
        Debug.Log(collider.name);
    }
}
