using System.Numerics;
using System.Threading;
using System.Runtime.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    Rigidbody rb;
    AudioSource audioSource;

    bool isAlive;

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
        if (Input.GetKey(KeyCode.W))
        {
            // rb.AddRelativeForce(0, 1, 0);
            rb.AddRelativeForce(UnityEngine.Vector3.up * mainThrust
                                * Time.deltaTime);
            if (!audioSource.isPlaying) 
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
           audioSource.Stop();     
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            // transform.Rotate(0, 0, 1);
            ApplyRotation(rotationThrust);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            // transform.Rotate(1, 0, 0);
            ApplyRotation(-rotationThrust);
        }
    }

    public void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation to manually rotate
        transform.Rotate(UnityEngine.Vector3.forward * rotationThisFrame
                             * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation to physics system take 
                                   // over
    }
}
