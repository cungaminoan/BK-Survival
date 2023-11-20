using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMouse : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot, lookRoot;
    [SerializeField]
    private bool invert;
    [SerializeField]
    private bool canUnlock = true;
    [SerializeField]
    private float sensivity = 2f;
    [SerializeField]
    private int smoothStep = 10;
    [SerializeField]
    private float smoothWeight = 0.4f;
    [SerializeField]
    private float rollAngle = 10f;
    [SerializeField]
    private float rollSpeed = 3f;
    [SerializeField]
    private Vector2 defaultLookLimits = new Vector2 (-70, 80f);
    private Vector2 lookAngle;
    private Vector2 currentMouseLook;
    private Quaternion lastValidPlayerRotation;
    private Quaternion lastValidLookRotation;
    private Vector2 smoothMove;
    private float currentRollAngle;
    private int lastLookFrame;
    // Start is called before the first frame update
    void Start()
    {
        lastValidPlayerRotation = playerRoot.localRotation;
        lastValidLookRotation = lookRoot.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.LockAndUnlockCursor();
        this.LookAround();
        this.FreezeLook();
    }

    void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Debug.Log("unlock cursor");
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Debug.Log("locked cursor");

            }
        }
    }
    void LookAround()
    {
        currentMouseLook = new Vector2(
            Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));
        lookAngle.x += currentMouseLook.x * sensivity * (invert ? 1f : -1f);
        lookAngle.y += currentMouseLook.y * sensivity;
        lookAngle.x = Mathf.Clamp(lookAngle.x, defaultLookLimits.x, defaultLookLimits.y);
        lookRoot.localRotation = Quaternion.Euler(lookAngle.x, 0f, 0f);
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngle.y, 0f);

    }
    void FreezeLook()
    {
        if (!IsCursorLocked())
        {
            // Apply last valid rotations when cursor is unlocked
            lookRoot.localRotation = lastValidLookRotation;
            playerRoot.localRotation = lastValidPlayerRotation;
        }
        else
        {
            // Update last valid rotations when cursor is locked
            lastValidLookRotation = lookRoot.localRotation;
            lastValidPlayerRotation = playerRoot.localRotation;
        }
    }

    bool IsCursorLocked()
    {
        return Cursor.lockState == CursorLockMode.Locked;
    }
}










