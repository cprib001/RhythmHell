using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public Camera viewCamera;
	public PlayerInput input;
	public float moveSpeed = 10f;
	public float runMultiplier = 2f;
	public float turnSpeed = 10f;
	public float jumpImpulse = 50f;

	[Range(0f, 2000f)]
	public float additionalGravitationalForce = 30f;
	
	private Rigidbody m_rigidbody;

	private void Awake()
	{
		m_rigidbody = GetComponent<Rigidbody>();
	}
	
	private void Start()
	{
		if(input != null)
		{
			input.onMove += OnMove;
			input.onJump += OnJump;
		}
	}
	
	private void OnDestroy()
	{
		if(input != null)
		{
			input.onMove -= OnMove;
			input.onJump -= OnJump;
		}
	}
	
	private void FixedUpdate()
	{
		m_rigidbody.AddForce(-Vector3.up * additionalGravitationalForce * Time.fixedDeltaTime);
	}
	
	private void OnMove(bool isRunning, Vector2 movement)
	{
		// The direction to move in in world coordinates
		Vector3 worldMoveDirection =  Vector3.Normalize(movement.y * viewCamera.transform.forward + movement.x * viewCamera.transform.right);
		
		// Get a vector for desired direction
		Vector3 normal = transform.up;
		Vector3.OrthoNormalize(ref normal, ref worldMoveDirection);	// transforms worldMoveDirection vector so that it is othogonal to the character's up vector
		
		float turnAmount = Vector3.Dot(transform.right, worldMoveDirection);
		if(Vector3.Dot(transform.forward, worldMoveDirection) < 0f)	// cranks up the turn amount to full, if the desired direction is more than 90 degrees away from the current facing direction
			turnAmount = Mathf.Sign(turnAmount);
		
		float inputMagnitude = Mathf.Clamp01(movement.magnitude);
		if(inputMagnitude > 0.9f && isRunning)
			inputMagnitude *= runMultiplier;
		
		transform.Rotate(Vector3.up * turnSpeed * turnAmount * inputMagnitude * Time.deltaTime);
		m_rigidbody.MovePosition(transform.position + transform.forward * inputMagnitude * moveSpeed);
	}
	
	private void OnJump()
	{
        if (!this.GetComponent<PlayerAttributes>().canJump)
            return;

		m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, jumpImpulse, m_rigidbody.velocity.z);
        this.GetComponent<PlayerAttributes>().canJump = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.GetComponent<PlayerAttributes>().canJump = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        this.GetComponent<PlayerAttributes>().canJump = true;
    }
}
