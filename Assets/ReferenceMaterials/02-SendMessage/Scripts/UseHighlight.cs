using UnityEngine;

public class UseHighlight : MonoBehaviour {

    public GameObject highlight;
    public string useTag = "Use";

    void Awake() {
        // disable the highlight on awake
        highlight.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) {

        // enable the highlight if the other object has the "use" tag
        if (other.tag == useTag) {
            highlight.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other) {

        // disable the highlight if the other object has the "use" tag
        if (other.tag == useTag) {
            highlight.SetActive(false);
        }
    }
}
