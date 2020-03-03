using UnityEngine;

public class LerpSpeed : MonoBehaviour {

    public float minSpeed;
    public float maxSpeed;

    public float speed;

    public float changeSpeedInterval;
    float startTimer;

    float initialSpeed;
    float targetSpeed;

    Vector2 targetPosition;

    public AnimationCurve animationCurve;

    // Use this for initialization
    void Start() {
        // select a new random target position
        ChooseNewRandomTargetPosition();

        // reset the current speed and initial speed, in case they've been changed in the Inspector
        initialSpeed = 0;
        speed = 0;
        // reset the timer
        ResetTimer();
        // choose a new target speed
        SetNewTargetSpeed();
    }

    void ResetTimer() {
        startTimer = Time.time;
    }

    void SetNewTargetSpeed() {
        // the new initial speed is the old target speed
        initialSpeed = targetSpeed;
        // set a new target speed
        targetSpeed = Random.Range(minSpeed, maxSpeed);
    }

    void ChooseNewRandomTargetPosition() {
        targetPosition = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height)));
    }

    // Update is called once per frame
    void Update() {
        // the time elapsed since the timer was started
        float timeElapsed = Time.time - startTimer;
        // the percent of time elapsed in relation to the speed change interval
        float percent = timeElapsed / changeSpeedInterval;
        // if time has elapsed, set a new target speed

        if (percent > 1) {
            SetNewTargetSpeed();
            percent = 0;
            ResetTimer();
        }

        // calculate the current speed, using percent and the animation curve
        speed = Mathf.Lerp(initialSpeed, targetSpeed, animationCurve.Evaluate(percent));

        // move toward the target position using the interpolated speed
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // if we've reached our destination, set a new destination
        if (Vector3.Distance(transform.position, targetPosition) < .1f) {
            ChooseNewRandomTargetPosition();
        }
    }
}
