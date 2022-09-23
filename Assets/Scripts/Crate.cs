using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Fighter {
    [SerializeField] private GameObject singleCrate;
    [SerializeField] private bool isDouble;

    protected override void Die() {
        if (isDouble) {
            Instantiate(singleCrate, this.transform.position, this.transform.rotation);
        }
        Destroy(gameObject);
    }
}
