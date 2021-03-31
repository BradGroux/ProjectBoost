using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - e.g. references for readability or speed
    // STATE - private instance (member) variables

    [SerializeField] float varBoost = 1000f;
    [SerializeField] float varThrust = 100f;
    [SerializeField] AudioClip sfxThrust;
    [SerializeField] ParticleSystem fxBoost;
    [SerializeField] ParticleSystem fxThrustLeft;
    [SerializeField] ParticleSystem fxThrustRight;

    Rigidbody varRigidbody;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        varRigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessBoost();
        ProcessThrust();
    }

    void ProcessBoost()
    {
        if (Input.GetKey(KeyCode.W))
        {
            StartBoost();

        }
        else
        {
            StopBoost();
        }
    }

    void StartBoost()
    {
        // Vector3 is for 3D - x, y, z
        varRigidbody.AddRelativeForce(Vector3.up * varBoost * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(sfxThrust);
        }
        if (!fxBoost.isPlaying)
        {
            fxBoost.Play();
        }
    }

    void StopBoost()
    {
        audioSource.Stop();
        fxBoost.Stop();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RightThrust();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            LeftThrust();
        }
        else
        {
            StopThrust();
        }
    }

    void RightThrust()
    {
        ApplyThrust(varThrust);
        if (!fxThrustRight.isPlaying)
        {
            fxThrustRight.Play();
        }
    }

    void LeftThrust()
    {
        ApplyThrust(-varThrust);
        if (!fxThrustLeft.isPlaying)
        {
            fxThrustLeft.Play();
        }
    }

    void StopThrust()
    {
        fxThrustRight.Stop();
        fxThrustLeft.Stop();
    }

    void ApplyThrust(float thrustThisFrame)
    {
        varRigidbody.freezeRotation = true; // freezes the physics rotation so our thrust works
        transform.Rotate(Vector3.forward * thrustThisFrame * Time.deltaTime);
        varRigidbody.freezeRotation = false; // unfreezes the physics rotation
    }
}