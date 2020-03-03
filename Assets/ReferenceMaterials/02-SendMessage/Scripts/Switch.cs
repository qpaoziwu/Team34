using UnityEngine;

public class Switch : MonoBehaviour {

    public bool isOn;
    Animator animatorComponent;
    public GameObject target;
    AudioSource audioSourceComponent;

    void Awake() {
        // get references to components
        animatorComponent = GetComponent<Animator>();
        audioSourceComponent = GetComponent<AudioSource>();
    }

    void OnUse() {
        // toggle activation
        isOn = !isOn;
        // toggle animator parameter
        animatorComponent.SetBool("isOn", isOn);
        // call the Activate method on the target GameObject and pass the activated boolean
        target.SendMessage("OnSwitchChanged", isOn);
        // play the switch sound
        audioSourceComponent.Play();
    }

}
