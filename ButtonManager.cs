using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class ButtonManager : MonoBehaviour
{
    GenerateManager generateManager = new GenerateManager();

    Transform sphere_tf;
    Rigidbody sphere_rb;

    GameObject instance;

    const int maxUpSize = 4;
    const int maxDownSize = -4;
    const int maxRightSize = 4;
    const int maxLeftSize = -4;

    // Start is called before the first frame update
    void Start()
    {
        instance = generateManager.sphereInstance;
        sphere_tf = instance.GetComponent<Transform>();
        sphere_rb = instance.GetComponent<Rigidbody>();

        Debug.Log(sphere_tf);
        Debug.Log(sphere_rb);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpButton()
    {
        Vector3 spherePos = sphere_tf.position;

        if (spherePos.z < maxUpSize)
        {
            spherePos += new Vector3(0, 0, 2);
            sphere_tf.position = spherePos;
        }
        
    }

    public void DownButton()
    {
        Vector3 spherePos = sphere_tf.position;

        if (spherePos.z > maxDownSize)
        {
            spherePos += new Vector3(0, 0, -2);
            sphere_tf.position = spherePos;
        }
    }

    public void RightButton()
    {
        Vector3 spherePos = sphere_tf.position;

        if (spherePos.x < maxRightSize)
        {
            spherePos += new Vector3(2, 0, 0);
            sphere_tf.position = spherePos;
        }
    }

    public void LeftButton()
    {
        Vector3 spherePos = sphere_tf.position;

        if (spherePos.x > maxLeftSize)
        {
            spherePos += new Vector3(-2, 0, 0);
            sphere_tf.position = spherePos;
        }
    }

    public void DecisionButton()
    {
        sphere_rb.useGravity = true;
    }

    public void NextPlayerButton()
    {

    }
}*/
