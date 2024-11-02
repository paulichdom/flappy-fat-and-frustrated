using UnityEngine;

public class Bird : MonoBehaviour {
    
    private const float JumpAmount = 100f;

    private Rigidbody2D _birdRigidBody2d;

    private void Awake() {
        _birdRigidBody2d = GetComponent<Rigidbody2D>();
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            Jump();
        }
    }

    private void Jump() {
        _birdRigidBody2d.velocity = Vector2.up * JumpAmount;
    }
}
