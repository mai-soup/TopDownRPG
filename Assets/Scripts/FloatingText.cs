using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText {
    public bool isActive;
    public GameObject go;
    public Text text;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show() {
        isActive = true;
        lastShown = Time.time;
        go.SetActive(true);
    }

    public void Hide() {
        isActive = false;
        go.SetActive(false);
    }

    public void UpdateFloatingText() {
        if (!isActive)
            return;

        // if shown for longer than duration, hide
        if (Time.time - lastShown > duration) {
            Hide();
        }

        // if active and not hidden, move up for frame
        go.transform.position += motion * Time.deltaTime;
    }
}
