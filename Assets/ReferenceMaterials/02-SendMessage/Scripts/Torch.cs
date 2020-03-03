using UnityEngine;

public class Torch : MonoBehaviour {
    Animator animator;

    void Awake() {
        // get reference to animator component
        animator = GetComponent<Animator>();
    }

    void OnSwitchChanged(bool isOn) {
        // set animator parameter		
        animator.SetBool("isOn", isOn);
    }

}
