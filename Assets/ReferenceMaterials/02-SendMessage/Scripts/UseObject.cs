using UnityEngine;
using System.Collections.Generic;

public class UseObject : MonoBehaviour {

    public List<GameObject> items;
    public List<string> useableTags;

    public string useButtonName;

    void OnTriggerEnter2D(Collider2D other) {
        // if not a useable object, then exit function early
        if (!useableTags.Contains(other.tag)) return;
        // if a useable object, then add object to the items list
        if (!items.Contains(other.gameObject)) {
            items.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        // if the item is currently in the list, remove it
        if (items.Contains(other.gameObject)) {
            items.Remove(other.gameObject);
        }
    }

    void Update() {
        // if the item in the list, and the use button is pressed, send it a message
        if (Input.GetButtonDown(useButtonName)) {
            foreach (GameObject item in items) {
                item.SendMessage("OnUse");
            }
        }
    }
}
