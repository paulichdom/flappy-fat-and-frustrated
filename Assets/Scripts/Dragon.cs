using UnityEngine;

public class Dragon : MonoBehaviour {
    
    private const float JumpAmount = 100f;

    private Rigidbody2D _dragonRigidBody2d;

    private void Awake() {
        _dragonRigidBody2d = GetComponent<Rigidbody2D>();
    }
    
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            Jump();
        }
    }

    private void Jump() {
        _dragonRigidBody2d.velocity = Vector2.up * JumpAmount;
    }
}
