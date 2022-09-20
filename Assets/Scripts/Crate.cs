using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : Fighter {
    // TODO: make double crates into single when half their HP is out
    protected override void Die() {
        Destroy(gameObject);
    }
}
