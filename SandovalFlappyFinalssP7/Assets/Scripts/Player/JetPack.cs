using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    public ParticleSystem ps;
    private ParticleSystem.EmissionModule em;
    public Animator anim;
    private void Awake()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

    }
    void Start()
    {
        em = ps.emission;

    }
    void Update()

    {

        bool pressed = Input.GetKey(KeyCode.Space);
        if (Input.GetKey(KeyCode.Space))
        {
            em.enabled = true;
            anim.SetBool("IsFirng", pressed);
        }
        else
        {
            em.enabled = false;
            anim.SetBool("IsFirng", false);
        }
    }
}

