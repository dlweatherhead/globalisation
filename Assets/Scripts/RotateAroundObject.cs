using UnityEngine;

public class RotateAroundObject : MonoBehaviour {
    public Transform target;
    public float rotateSpeed = 1f;

    void Update() {
        if (Input.GetMouseButton(1)) {
            Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);

            float mousePosX = (Input.mousePosition.x - screenCentre.x) / (screenCentre.x);
            float mousePosY = (Input.mousePosition.y - screenCentre.y) / (screenCentre.y);

            Vector3 moveVector = new Vector3(mousePosX, mousePosY, 0f);
            transform.Translate(moveVector * rotateSpeed * Time.deltaTime);
            transform.LookAt(target);
        }
    }
}