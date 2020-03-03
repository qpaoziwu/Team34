using UnityEngine;

public class LerpColor : MonoBehaviour {
    
    public Color startColor;
    public Color endColor;

    SpriteRenderer spriteRendererComponent;

    public float lerpDuration;

    float startTimer;

    void Awake() {
        // get references to components
        spriteRendererComponent = GetComponent<SpriteRenderer>();

    }
    // Use this for initialization
    void Start() {
        // set the color of the sprite to be the start color
        spriteRendererComponent.color = startColor;

        ResetTimer();
    }

    void ResetTimer() {
        startTimer = Time.time;
    }

    // Update is called once per frame
    void Update() {
        // calculate the percent of the lerp duration that has elapsed
        float timeElapsed = Time.time - startTimer;
        float percent = timeElapsed / lerpDuration;

        // if time has elapsed, reset the timer
        if (percent > 1) {
            ResetTimer();
        }
        // set the color somewhere between the start color and the end color, according to the percent
        spriteRendererComponent.color = Color.Lerp(startColor, endColor, percent);
    }
}
