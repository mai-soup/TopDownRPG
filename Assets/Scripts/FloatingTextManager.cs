using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour {
    public GameObject textContainer;
    public GameObject textPrefab;
    private List<FloatingText> floatingTexts = new List<FloatingText>();

    public void Show(string msg, int fontSize, Color color, Vector3 pos,
        Vector3 motion, float duration) {
        FloatingText ft = GetFloatingText();

        ft.text.text = msg;
        ft.text.fontSize = fontSize;
        ft.text.color = color;
        // transfer world space to screen space - ui text uses screen space!
        ft.go.transform.position = Camera.main.WorldToScreenPoint(pos);
        ft.motion = motion;
        ft.duration = duration;

        ft.Show();
    }

    private void Update() {
        foreach (FloatingText text in floatingTexts) {
            text.UpdateFloatingText();
        }
    }

    private FloatingText GetFloatingText() {
        // find an inactive floatingtext
        FloatingText ft = floatingTexts.Find(t => !t.isActive);

        if (ft == null) {
            // no inactive text found - create one!
            ft = new FloatingText();
            ft.go = Instantiate(textPrefab);
            ft.go.transform.SetParent(textContainer.transform);
            ft.text = ft.go.GetComponent<Text>();

            // add the new floatingtext to the pool
            floatingTexts.Add(ft);
        }

        return ft;
    }
}
