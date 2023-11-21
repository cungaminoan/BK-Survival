using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public float sprintSpeed = 7f;
    public float normalSpeed = 5f;
    public float crouchSpeed = 2f;
    private Transform lookRoot;
    private float standHeight = 1.6f;
    private float crouchHeight = 1.0f;
    private bool isCrouching;
    private PlayerFootsteps playerFootsteps;
    private float sprintVolume = 1f;
    private float crouchVolume = 0.1f;
    private float minimumWalkVolume = 0.3f, maximumWalkVolume = 0.7f;
    private float walkStepDistance = 0.4f;
    private float sprintStepDistance = 0.25f;
    private float crouchStepDistance = 0.5f;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        lookRoot = transform.GetChild(0);
        playerFootsteps = GetComponentInChildren<PlayerFootsteps>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerFootsteps.maxVolume = maximumWalkVolume;
        playerFootsteps.minVolume = minimumWalkVolume;
        playerFootsteps.stepDistance = walkStepDistance;
    }
    // Update is called once per frame
    void Update()
    {
        this.Sprint();
        this.Crouch();
    }

    void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = sprintSpeed;
            playerFootsteps.stepDistance = sprintStepDistance;
            playerFootsteps.minVolume = sprintVolume;
            playerFootsteps.maxVolume = sprintVolume;
            Debug.Log("sprint");
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = normalSpeed;
            playerFootsteps.stepDistance = walkStepDistance;
            playerFootsteps.maxVolume = maximumWalkVolume;
            playerFootsteps.minVolume = minimumWalkVolume;
        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouching)
            {
                lookRoot.localPosition = new Vector3(0f, standHeight, 0f);
                playerMovement.speed = normalSpeed;
                playerFootsteps.stepDistance = walkStepDistance;
                playerFootsteps.maxVolume = maximumWalkVolume;
                playerFootsteps.minVolume = minimumWalkVolume;
                isCrouching = false;
            }
            else
            {
                lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                playerMovement.speed = crouchSpeed;
                playerFootsteps.stepDistance = crouchStepDistance;
                playerFootsteps.maxVolume = crouchVolume;
                playerFootsteps.minVolume = crouchVolume;
                isCrouching = true;
            }
        }
    }
}