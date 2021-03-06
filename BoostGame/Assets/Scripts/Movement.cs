using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainParticles;
    [SerializeField] ParticleSystem leftParticles;
    [SerializeField] ParticleSystem rightParticles;
    

    Rigidbody rb;
    AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
    
    void ProcessRotation()
    {        
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainParticles.isPlaying)
        {
            mainParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainParticles.Stop();
    }
   

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        if (!leftParticles.isPlaying)
        {
            leftParticles.Play();
        }
    }
    
    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        if (!rightParticles.isPlaying)
        {
            rightParticles.Play();
        }
    }
    
    private void StopRotating()
    {
        leftParticles.Stop();
        rightParticles.Stop();
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
