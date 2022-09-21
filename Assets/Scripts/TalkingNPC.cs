using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingNPC : Collidable {
    public string msg;
    private float lastTriggered;
    private static readonly float triggerCooldown = 0.1f;
    protected override void OnCollide(Collider2D collider) {
        // if the collider is a player, keep showing a message until they
        // leave
        if (collider.name == "Player" && collider.tag == "Fighter") {
            if (Time.time - lastTriggered > triggerCooldown) {
            lastTriggered = Time.time;
            GameManager.instance.ShowText(
                msg,            // txt to show
                16,             // fontsize
                Color.white,    // colour
                transform.position + // show above npc
                    new Vector3(
                        0, 2*GetComponent<BoxCollider2D>().bounds.extents.y, 0),
                Vector3.zero,   // no motion
                triggerCooldown
                );
            }
        }
    }
}
