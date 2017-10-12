using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	public enum MoveMode { UPDATE, FIXED_UPDATE, LATE_UPDATE }
	public MoveMode moveMode = MoveMode.UPDATE;
	public Transform target;
    public float minDistance = .05f;
	public float maxDistance = 4f;
    public float cameraX = 1f;
    public float cameraY = .75f;
	public Vector2 verticalLimit = new Vector2(-80f, 80f);
	public float speed = 1.5f;
    public LayerMask mask;

    private bool inObject = false;
	private Vector2 m_currentRotation;
	
	private void Start()
	{
		m_currentRotation = Vector2.zero;
	}
	
	private void Update()
	{
        if (GameManager.Instance.Paused)
            return;

        if (moveMode == MoveMode.UPDATE)
            UpdateCamera();
	}
	
	private void FixedUpdate()
	{
        if (GameManager.Instance.Paused)
            return;

        if (moveMode == MoveMode.FIXED_UPDATE)
                UpdateCamera();
	}
	
	private void LateUpdate()
	{
        if (GameManager.Instance.Paused)
            return;

        if (moveMode == MoveMode.LATE_UPDATE)
            UpdateCamera();
    }

    private void UpdateCamera()
    {
        if (target == null)
            return;

        m_currentRotation.x += Input.GetAxis("Mouse X") * speed;
        m_currentRotation.y += Input.GetAxis("Mouse Y") * speed * -1;

        m_currentRotation.y = ClampAngle(m_currentRotation.y, verticalLimit.x, verticalLimit.y);

        Quaternion rotation = Quaternion.Euler(m_currentRotation.y, m_currentRotation.x, 0.0f);
        Vector3 position = rotation * new Vector3(cameraX, cameraY, -maxDistance) + target.position;

        transform.rotation = rotation;
        transform.position = position;

        RaycastHit hit;
        while ((Physics.Raycast(transform.position, (target.transform.position - transform.position), out hit, maxDistance, mask) 
            && hit.transform != target.transform 
            && Vector3.Distance(target.transform.position, transform.position) > minDistance) 
            || inObject)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * speed);
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360.0f)
            angle += 360.0f;
        if (angle > 360.0f)
            angle -= 360.0f;

        return Mathf.Clamp(angle, min, max);
    }

    private void OnCollisionEnter(Collision collision)
    {
        inObject = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        inObject = false;
    }
}
