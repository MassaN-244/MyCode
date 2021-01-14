using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpherePrefabManager : MonoBehaviour
{
    [SerializeField] AudioClip collisionSphere;
    AudioSource audioSource;
    GameObject obj;

    Rigidbody rb;

    //bool flag = true;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        obj = GameObject.Find("GenerateAndGameManager");
        audioSource = obj.GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.PlayOneShot(collisionSphere);        
    }

    /*void ChangeFlag()
    {
        flag = true;
    }*/
}
