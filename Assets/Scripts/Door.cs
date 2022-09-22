using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : Collidable {
    public string[] sceneNames;

    protected override void OnCollide(Collider2D collider) {
        if (collider.name == "Player") {
            GameManager.instance.SaveState();

            // teleport player to random scene
            string scene = sceneNames[Random.Range(0, sceneNames.Length)];
            LevelManager.instance.LoadScene(scene);
            // prevent the door keeping being in use and making infinite loads
            Destroy(gameObject);
        }
    }
}
