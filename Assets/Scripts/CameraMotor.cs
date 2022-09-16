using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {
    public Transform lookAt;
    public float boundX = 0.3f;
    public float boundY = 0.15f;

    private void LateUpdate() {
        Vector3 delta = Vector3.zero;

        // get distance between current focus point and player
        float dx = lookAt.position.x - transform.position.x;
        float dy = lookAt.position.y - transform.position.y;

        if (dx > boundX || dx < -boundX) {
            // outside bounds on x - check direction and adjust
            if (transform.position.x < lookAt.position.x) {
                delta.x = dx - boundX;
            } else {
                delta.x = dx + boundX;
            }
        }

        if (dy > boundY || dy < -boundY) {
            // outside bounds on y - check direction and adjust
            if (transform.position.y < lookAt.position.y) {
                delta.y = dy - boundY;
            } else {
                delta.y = dy + boundY;
            }
        }

        transform.position += delta;
    }
}
