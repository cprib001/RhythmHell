using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
	public delegate void OnMove(bool isRunning, Vector2 direction);
	public OnMove onMove;
	
	public delegate void OnJump();
	public OnJump onJump;
	
	public delegate bool OnActivate();
	public OnActivate onActivate;

    public delegate void OnShoot();
    public OnShoot onShoot;

    public delegate void OnPause();
    public OnPause onPause;

    public delegate void OnClearMagnets();
    public OnClearMagnets onClearMagnets;

    public float inputThreshold = 0.05f;
	
	public bool isRunning { get; private set; }
	public Vector2 movementVector { get; private set; }
	
	private void Update()
	{
        if (GameManager.Instance.gameState == "Menu" || GameManager.Instance.gameState == "Won" || GameManager.Instance.gameState == "Lost")
            return;
        GetPauseInput();

        if (GameManager.Instance.Paused)
            return;

        GetMovementInput();
        GetJumpInput();
        GetShootInput();
	}
	
	private void GetMovementInput()
	{
		isRunning = Input.GetKey(KeyCode.LeftShift);
		movementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		if(movementVector.sqrMagnitude < inputThreshold)
			return;
		
		if(onMove != null)
			onMove(isRunning, movementVector);
	}
	
	private void GetJumpInput()
	{
		if(!Input.GetButtonDown("Jump"))
			return;
		
		if(onJump != null)
			onJump();
	}

    private void GetPauseInput()
    {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        if (onPause != null)
            onPause();
    }

    private void GetShootInput()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (onShoot != null)
            onShoot();
    }
}
