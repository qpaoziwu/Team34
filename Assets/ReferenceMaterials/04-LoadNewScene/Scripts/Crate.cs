using UnityEngine;

public class Crate : MonoBehaviour {

    Rigidbody2D rigidbody2DComponent;

    void Awake() {
        rigidbody2DComponent = GetComponent<Rigidbody2D>();
    }

    void OnTimerFinished() {
        // enable physics 
        rigidbody2DComponent.isKinematic = false;
        // give the crate a bit of a push
        rigidbody2DComponent.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
        rigidbody2DComponent.AddTorque(-10f);
    }
}
