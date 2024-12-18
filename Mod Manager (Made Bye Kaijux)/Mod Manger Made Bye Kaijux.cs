using UnityEngine;
using UnityEngine.InputSystem;

public class GorillaTagMod : MonoBehaviour
{
    public float flightSpeed = 5.0f;
    public float colorChangeSpeed = 1.0f;

    private bool isMenuVisible = false;
    private bool isFlying = false;
    private bool isRainbowActive = false;
    private bool isStickToWallsActive = false;
    private bool isGhostMonkeyActive = false;

    private Rigidbody playerRigidbody;
    private Renderer playerRenderer;
    private PlayerInput playerInput;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerRenderer = GetComponent<Renderer>();
        playerInput = GetComponent<PlayerInput>();

        playerInput.actions["Fly"].performed += OnTriggerPressed;
        playerInput.actions["Fly"].canceled += OnTriggerReleased;
    }

    private void OnDestroy()
    {
        playerInput.actions["Fly"].performed -= OnTriggerPressed;
        playerInput.actions["Fly"].canceled -= OnTriggerReleased;
    }

    void Update()
    {
        if (IsLShapeGestureDetected())
        {
            isMenuVisible = !isMenuVisible;
        }

        if (isFlying)
        {
            HandleFlight();
        }

        if (isRainbowActive)
        {
            ChangeColor();
        }

        if (isStickToWallsActive)
        {
            StickToWalls();
        }
    }

    void OnGUI()
    {
        if (isMenuVisible)
        {
            GUI.Box(new Rect(10, 10, 200, 420), "Mod Menu");

            if (GUI.Button(new Rect(20, 40, 160, 30), isFlying ? "Disable Fly" : "Enable Fly"))
            {
                if (isFlying)
                {
                    DisableFlightMode();
                }
                else
                {
                    EnableFlightMode();
                }
            }

            if (GUI.Button(new Rect(20, 80, 160, 30), isRainbowActive ? "Disable Rainbow Monkey" : "Enable Rainbow Monkey"))
            {
                if (isRainbowActive)
                {
                    DisableRainbowMode();
                }
                else
                {
                    EnableRainbowMode();
                }
            }

            if (GUI.Button(new Rect(20, 120, 160, 30), isStickToWallsActive ? "Disable Stick to Walls" : "Enable Stick to Walls"))
            {
                if (isStickToWallsActive)
                {
                    DisableStickToWalls();
                }
                else
                {
                    EnableStickToWalls();
                }
            }

            if (GUI.Button(new Rect(20, 160, 160, 30), isGhostMonkeyActive ? "Disable Ghost Monkey" : "Enable Ghost Monkey"))
            {
                if (isGhostMonkeyActive)
                {
                    DisableGhostMonkey();
                }
                else
                {
                    EnableGhostMonkey();
                }
            }

            if (GUI.Button(new Rect(20, 200, 160, 30), "Leave Lobby"))
            {
                LeaveLobby();
            }
        }
    }

    bool IsLShapeGestureDetected()
    {
        return Input.GetKeyDown(KeyCode.L); // Placeholder
    }

    public void EnableFlightMode()
    {
        isFlying = true;
        playerRigidbody.isKinematic = true;
    }

    public void DisableFlightMode()
    {
        isFlying = false;
        playerRigidbody.isKinematic = false;
    }

    void HandleFlight()
    {
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0, input.y);
        transform.position += direction * flightSpeed * Time.deltaTime;
    }

    public void EnableRainbowMode()
    {
        isRainbowActive = true;
    }

    public void DisableRainbowMode()
    {
        isRainbowActive = false;
        playerRenderer.material.color = Color.white;
    }

    void ChangeColor()
    {
        float hue = Mathf.PingPong(Time.time * colorChangeSpeed, 1);
        Color color = Color.HSVToRGB(hue, 1, 1);
        playerRenderer.material.color = color;
    }

    public void EnableStickToWalls()
    {
        isStickToWallsActive = true;
    }

    public void DisableStickToWalls()
    {
        isStickToWallsActive = false;
    }

    void StickToWalls()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1.0f))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                playerRigidbody.isKinematic = true;
                transform.position = hit.point;
                Debug.Log("Sticking to wall.");
            }
        }
    }

    public void EnableGhostMonkey()
    {
        isGhostMonkeyActive = true;
        SetPlayerVisibility(false);
    }

    public void DisableGhostMonkey()
    {
        isGhostMonkeyActive = false;
        SetPlayerVisibility(true);
    }

    void SetPlayerVisibility(bool isVisible)
    {
        Color color = playerRenderer.material.color;
        color.a = isVisible ? 1.0f : 0.0f;
        playerRenderer.material.color = color;
    }

    private void OnTriggerPressed(InputAction.CallbackContext context)
    {
        EnableFlightMode();
    }

    private void OnTriggerReleased(InputAction.CallbackContext context)
    {
        DisableFlightMode();
    }

    void LeaveLobby()
    {
        Debug.Log("Leaving lobby...");
        // Placeholder for actual leave lobby implementation
    }
}