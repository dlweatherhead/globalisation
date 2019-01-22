using UnityEngine;

public class RotateObject : MonoBehaviour {
    public float speed;

    private float currentSpeed;

    void Start() {
        currentSpeed = speed;
    }

    void Update() {
        transform.RotateAround(Vector3.right, currentSpeed * Time.deltaTime);
    }
}