using UnityEngine;

public class RotateTowardMouse : MonoBehaviour {

    // Update is called once per frame
    void Update() {
        // get the position of the mouse in world space
        Vector2 mousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // get the direction to the mouse from the arrow
        Vector3 directionToMouse = (new Vector3(mousePositionInWorld.x, mousePositionInWorld.y, 0) - transform.position).normalized;

        // draw a line in the Scene window to ensure the direction has been calculated correctly
        Debug.DrawLine(transform.position, transform.position + directionToMouse, Color.red);

        // using Mathf.Atan2, calculate the angle of the direction vector
        float angleInRadians = Mathf.Atan2(directionToMouse.y, directionToMouse.x);
        float angleInDegrees = angleInRadians * Mathf.Rad2Deg;

        // set the rotation of the arrow to the angle to the mouse
        // note that sprites need to be pointing to the right or you will have to include an offset
        transform.eulerAngles = new Vector3(0, 0, angleInDegrees);
    }
}
