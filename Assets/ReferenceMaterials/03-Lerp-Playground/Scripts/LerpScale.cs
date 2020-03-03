using UnityEngine;

public class LerpScale : MonoBehaviour {

    public float maxSize;
    public float minSize;

    Vector3 maxScale;
    Vector3 minScale;

    public float growDuration;
    public float shrinkDuration;

    public bool isGrowing;

    float startTimer;
    public AnimationCurve animationCurve;

    // Use this for initialization
    void Start() {
        // Create Vector3s for both the min and max sizes
        maxScale = new Vector3(maxSize, maxSize, 1);
        minScale = new Vector3(minSize, minSize, 1);
        // set the scale of the object to the minimum scale
        transform.localScale = minScale;
        // initially, the object should grow
        isGrowing = true;

        ResetTimer();
    }

    void ResetTimer() {
        startTimer = Time.time;
    }

    void Grow() {
        // calculate the time elapsed since the timer was started
        float timeElapsed = Time.time - startTimer;
        // get the percent of time elapsed in relation to the grow duration
        float percent = timeElapsed / growDuration;

        // if the timer has elapsed, switch states
        if (percent > 1) {
            transform.localScale = maxScale;
            isGrowing = false;
            ResetTimer();
            return;
        }

        // calculate the current scale of the square using the percent and animation curve
        Vector3 newScale = Vector3.Lerp(minScale, maxScale, animationCurve.Evaluate(percent));
        transform.localScale = newScale;
    }


    void Shrink() {
        // get the percent of time elapsed in relation to the shrink duration
        float percent = (Time.time - startTimer) / shrinkDuration;

        // if the timer has elapsed, switch states
        if (percent > 1) {
            transform.localScale = minScale;
            isGrowing = true;
            ResetTimer();
            return;
        }

        // calculate the current scale of the square using the percent and animation curve
        Vector3 newScale = Vector3.Lerp(maxScale, minScale, animationCurve.Evaluate(percent));
        transform.localScale = newScale;
    }


    // Update is called once per frame
    void Update() {
        if (isGrowing) {
            Grow();
        } else if (!isGrowing) {
            Shrink();
        }
    }
}
