using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Collidable {
    public string[] sceneNames;
    protected override void OnCollide(Collider2D collider) {
        if (collider.name == "Player") {
            // teleport player to random scene
            string scene = sceneNames[Random.Range(0, sceneNames.Length)];
            SceneManager.LoadScene(scene);
        }
    }
}
