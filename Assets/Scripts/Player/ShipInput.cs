using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInput : MonoBehaviour
{
    [Range(-1, 1)]
    public float pitch;
    [Range(-1, 1)]
    public float yaw;
    [Range(-1, 1)]
    public float updown;
    [Range(-1, 1)]
    public float roll;
    [Range(-1, 1)]
    public float strafe;
    [Range(0, 1)]
    public float throttle;

    // How quickly the throttle reacts to input.
    private const float THROTTLE_SPEED = 0.5f;

    // Keep a reference to the ship this is attached to just in case.
    private Player playerShip;
    private bool isPlayer;
    private bool boostActive;
    private CameraControl cameraControl;
    private EnemySelect enemySelect;
    private ShipSounds shipSounds;
    private ShipPhysics shipPhysics;


    private void Awake()
    {

        playerShip = GetComponent<Player>();
        shipPhysics = this.GetComponent<ShipPhysics>();
        isPlayer = this.gameObject.name == "Player";
        cameraControl = this.GetComponent<CameraControl>();
        enemySelect = this.GetComponent<EnemySelect>();
        shipSounds = this.GetComponent<ShipSounds>();

    }

    private void Update()
    {
        if (isPlayer)
        {
            strafe = Input.GetAxis("Horizontal");
            roll = Input.GetAxis("Roll");
            updown = Input.GetAxis("UpDown");
            SetStickCommandsUsingMouse();
            UpdateMouseWheelThrottle();
            UpdateKeyboardThrottle(KeyCode.W, KeyCode.S);
            Boost(KeyCode.LeftShift);
            SelectEnemy(KeyCode.T);
            UpdateMouseInput();
      
        }
    }

    //PLAYER ZONE
    private void SelectEnemy(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            enemySelect.SelectEnemy();
        }
    }
    private void Boost(KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            shipPhysics.SetMultiplier(200);
            if (boostActive==false)
            {
                shipSounds.PlayBoostSound();
                boostActive = true;
            }


        }
        if (Input.GetKeyUp(key))
        {
            shipPhysics.SetMultiplier(100);
            boostActive = false;
            shipSounds.StopBoostSound();
        }
    }
    private void UpdateMouseInput()
    {
        //0 - Left 1- Right 2 - Middle
        if (Input.GetMouseButtonDown(2))
        {
            if (cameraControl != null)
                cameraControl.SetBackCamera();
        }
        if (Input.GetMouseButtonUp(2))
        {
            if (cameraControl != null)
                cameraControl.SetNormalCamera();
        }
    }
    private void SetStickCommandsUsingMouse()
    {
        Vector3 mousePos = Input.mousePosition;

        // Figure out most position relative to center of screen.
        // (0, 0) is center, (-1, -1) is bottom left, (1, 1) is top right.      
        pitch = (mousePos.y - (Screen.height * 0.5f)) / (Screen.height * 0.5f);
        yaw = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

        // Make sure the values don't exceed limits.
        pitch = -Mathf.Clamp(pitch, -1.0f, 1.0f);
        yaw = Mathf.Clamp(yaw, -1.0f, 1.0f);
    }

    private void UpdateKeyboardThrottle(KeyCode increaseKey, KeyCode decreaseKey)
    {
        float target = throttle;

        if (Input.GetKey(increaseKey))
            target = 1.0f;
        else if (Input.GetKey(decreaseKey))
            target = 0.0f;

        throttle = Mathf.MoveTowards(throttle, target, Time.deltaTime * THROTTLE_SPEED);
    }
    private void UpdateMouseWheelThrottle()
    {
        throttle += Input.GetAxis("Mouse ScrollWheel");
        throttle = Mathf.Clamp(throttle, 0.0f, 1.0f);
    }
}

