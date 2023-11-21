using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    private AudioSource footStepSounds;
    [SerializeField]
    private AudioClip[] footStepClip;
    private CharacterController characterController;
    [HideInInspector]
    public float minVolume, maxVolume;
    [HideInInspector]
    public float stepDistance;
    private float accumulatedDistance;
    // Start is called before the first frame update
    void Awake()
    {
        footStepSounds = GetComponent<AudioSource>();
        characterController = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        this.CheckToPlayerFootstepSound();
    }

    void CheckToPlayerFootstepSound()
    {
        if (!characterController.isGrounded)
            return;
        if (characterController.velocity.sqrMagnitude > 0)
        {
            accumulatedDistance += Time.deltaTime;
            Debug.Log($"Accumulated Distance: {accumulatedDistance}, Step Distance: {stepDistance}");

            if(accumulatedDistance > stepDistance)
            {
                footStepSounds.volume = Random.Range(minVolume, maxVolume);
                footStepSounds.clip = footStepClip[Random.Range(0, footStepClip.Length)];
                footStepSounds.Play();
                accumulatedDistance = 0f;
                Debug.Log("Checking footstep sound");

            }
        }
        else
        {
            accumulatedDistance = 0f;
        }
    }
}
