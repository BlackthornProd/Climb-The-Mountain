using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	[Header("Mouvement")]
	public float moveSpeed = 6;
	public float accelarationTimeInAir = 0.2f;
	public float accelarationTimeGrounded = 0.1f;

	[Header("Jumping")]
	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	public float doubleJumpHeightModifier = 1;

	float jumpVelocity;
	float gravity;
	Vector3 velocity;
	float velocityXSmoothing;
	bool canDoubleJump;

	Controller2D controller;

	void Start () {
		controller = GetComponent<Controller2D> ();

		gravity = -(2 * jumpHeight / Mathf.Pow (timeToJumpApex, 2));
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}

	void Update () {

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));

		if (Input.GetMouseButtonDown (1) && controller.collisions.below) {
			Jump (false);
		}

		if (!controller.collisions.below && canDoubleJump && Input.GetMouseButtonDown (1)) {
			Jump (true);
		}

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelarationTimeGrounded : accelarationTimeInAir);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
	}

	void Jump (bool isDoubleJump) {
		if (!isDoubleJump) {
			velocity.y = jumpVelocity;
			canDoubleJump = true;
		} else {
			velocity.y = jumpVelocity * doubleJumpHeightModifier;
			canDoubleJump = false;
		}
	}

}
