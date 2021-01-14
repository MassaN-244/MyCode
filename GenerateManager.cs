using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;

public class GenerateManager : MonoBehaviour
{
    /*
    [SerializeField] GameObject partitionPrefab;
    [SerializeField] GameObject redSpherePrefab;
    [SerializeField] GameObject blueSpherePrefab;
    [SerializeField] LayerMask sphereLayer;

    Vector3 generateSpherePos = new Vector3(0, 7, 0);

    GameManager gm = new GameManager();
    GameObject sphereInstance;
    Transform sphere_tf;
    Rigidbody sphere_rb;

    const int maxUpSize = 4;
    const int maxDownSize = -4;
    const int maxRightSize = 4;
    const int maxLeftSize = -4;

    int[,,] setBox = new int[5, 5, 5];
    int num_1p = 0;
    int num_2p = 1;
    int num_null = 2;

    string winner;



    void Start()
    {
        for (int x = 0; x < 5; x++)
        {
            for (int z = 0; z < 5; z++)
            {
                for (int y = 0; y < 5; y++)
                {
                    setBox[x, z, y] = num_null;
                }
            }
        }

        sphereInstance = Instantiate(redSpherePrefab, generateSpherePos, Quaternion.identity);
        sphere_tf = sphereInstance.transform;
        sphere_rb = sphereInstance.GetComponent<Rigidbody>();
        GenerateParatition();
    }

    void Update()
    {
        JudgeWin(num_1p);
        JudgeWin(num_2p);
    }

    //---------------------------------------------------------------------------------------------------MethodArea---------------------------------------------------------------------------------------------------------------//

    void GenerateParatition()
    {
        for (int x = -2; x <= 2; x++)
        {
            for (int y = -2; y <= 2; y++)
            {
                for (int z = -2; z <= 2; z++)
                {
                    Vector3 generatePos = new Vector3(x * 2, y * 2, z * 2);
                    Instantiate(partitionPrefab, generatePos, Quaternion.identity);
                }
            }
        }
    }
    
    public void GenerateSphere()
    {
        if (gm.is1P == true)
        {
            sphereInstance = Instantiate(redSpherePrefab, generateSpherePos, Quaternion.identity);
            sphere_tf = sphereInstance.transform;
            sphere_rb = sphereInstance.GetComponent<Rigidbody>();
        }
        else if (gm.is1P == false)  
        {
            sphereInstance = Instantiate(blueSpherePrefab, generateSpherePos, Quaternion.identity);
            sphere_tf = sphereInstance.transform;
            sphere_rb = sphereInstance.GetComponent<Rigidbody>();
        }
    }

    bool IsDecision()
    {
        Vector3 startVec = sphere_tf.position;
        Vector3 endVec = sphere_tf.position - new Vector3(0, 4, 0);

        Debug.DrawLine(startVec, endVec);

        return Physics.Linecast(startVec, endVec, sphereLayer);
    }

    public void JudgeWin(int num)
    {
        if (sphere_rb.velocity.y == 0) 
        {
            if (num == num_1p)
            {
                winner = "Player1";
            }
            else if (num == num_2p)
            {
                winner = "Player2";
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    //直線で揃う場合
                    if (setBox[1, i, j] == num && setBox[2, i, j] == num && setBox[3, i, j] == num && setBox[4, i, j] == num)
                    {
                        Debug.Log(winner + "-Win-");
                    }
                    else if (setBox[0, i, j] == num && setBox[1, i, j] == num && setBox[2, i, j] == num && setBox[3, i, j] == num)
                    {
                        Debug.Log(winner + "-Win-");
                    }

                    if (setBox[i, 1, j] == num && setBox[i, 2, j] == num && setBox[i, 3, j] == num && setBox[i, 4, j] == num)
                    {
                        Debug.Log(winner + "-Win-");
                    }
                    else if (setBox[i, 0, j] == num && setBox[i, 1, j] == num && setBox[i, 2, j] == num && setBox[i, 3, j] == num)
                    {
                        Debug.Log(winner + "-Win-");
                    }

                    if (setBox[i, j, 1] == num && setBox[i, j, 2] == num && setBox[i, j, 3] == num && setBox[i, j, 4] == num)
                    {
                        Debug.Log(winner + "-Win-");
                    }
                    else if (setBox[i, j, 0] == num && setBox[i, j, 1] == num && setBox[i, j, 2] == num && setBox[i, j, 3] == num)
                    {
                        Debug.Log(winner + "-Win-");
                    }                    
                }

                //平面上で斜め（右上がり）に揃う場合
                if (setBox[i, 0, 0] == num && setBox[i, 1, 1] == num && setBox[1, 2, 2] == num && setBox[i, 3, 3] == num)
                {
                    Debug.Log(winner + "-Win-");
                }
                else if (setBox[i, 1, 1] == num && setBox[i, 2, 2] == num && setBox[i, 3, 3] == num && setBox[i, 4, 4] == num) 
                {
                    Debug.Log(winner + "-Win-");
                }

                if (setBox[1, i, 1] == num && setBox[2, i, 2] == num && setBox[3, i, 3] == num && setBox[4, i, 4] == num)
                {
                    Debug.Log(winner + "-Win-");
                }
                else if (setBox[0, i, 0] == num && setBox[1, i, 1] == num && setBox[2, i, 2] == num && setBox[3, i, 3] == num)
                {
                    Debug.Log(winner + "-Win-");
                }

                if (setBox[1, 1, i] == num && setBox[2, 2, i] == num && setBox[3, 3, i] == num && setBox[4, 4, i] == num)
                {
                    Debug.Log(winner + "-Win-");
                }
                else if (setBox[0, 0, i] == num && setBox[1, 1, i] == num && setBox[2, 2, i] == num && setBox[3, 3, i] == num)
                {
                    Debug.Log(winner + "-Win-");
                }

                //平面上で斜め（右下がり）に揃う場合
                if (setBox[i, 4, 0] == num && setBox[i, 3, 1] == num && setBox[1, 2, 2] == num && setBox[i, 1, 3] == num)
                {
                    Debug.Log(winner + "-Win-");
                }
                else if (setBox[i, 3, 1] == num && setBox[i, 2, 2] == num && setBox[i, 1, 3] == num && setBox[i, 0, 4] == num)
                {
                    Debug.Log(winner + "-Win-");
                }

                if (setBox[4, i, 0] == num && setBox[3, i, 1] == num && setBox[2, i, 2] == num && setBox[1, i, 3] == num)
                {
                    Debug.Log(winner + "-Win-");
                }
                else if (setBox[3, i, 1] == num && setBox[2, i, 2] == num && setBox[1, i, 3] == num && setBox[0, i, 4] == num)
                {
                    Debug.Log(winner + "-Win-");
                }

                if (setBox[4, 0, i] == num && setBox[3, 1, i] == num && setBox[2, 2, i] == num && setBox[1, 3, i] == num)
                {
                    Debug.Log(winner + "-Win-");
                }
                else if (setBox[3, 1, i] == num && setBox[2, 2, i] == num && setBox[1, 3, i] == num && setBox[0, 4, i] == num)
                {
                    Debug.Log(winner + "-Win-");
                }

                //立体の対角線上で斜めに揃う場合
                if (setBox[4, 0, 0] == num && setBox[3, 1, 1] == num && setBox[2, 2, 2] == num && setBox[1, 3, 3] == num)
                {
                    Debug.Log(winner + "-Win-");
                }
                else if (setBox[3, 1, 1] == num && setBox[2, 2, 2] == num && setBox[1, 3, 3] == num && setBox[0, 4, 4] == num)
                {
                    Debug.Log(winner + "-Win-");
                }
                if (setBox[0, 0, 0] == num && setBox[1, 1, 1] == num && setBox[2, 2, 2] == num && setBox[3, 3, 3] == num)
                {
                    Debug.Log(winner + "-Win-");
                }
                else if (setBox[1, 1, 1] == num && setBox[2, 2, 2] == num && setBox[3, 3, 3] == num && setBox[4, 4, 4] == num)
                {
                    Debug.Log(winner + "-Win-");
                }
                if (setBox[0, 4, 0] == num && setBox[1, 3, 1] == num && setBox[2, 2, 2] == num && setBox[3, 1, 3] == num)
                {
                    Debug.Log(winner + "-Win-");
                }
                else if (setBox[1, 3, 1] == num && setBox[2, 2, 2] == num && setBox[3, 1, 3] == num && setBox[4, 0, 4] == num)
                {
                    Debug.Log(winner + "-Win-");
                }
                if (setBox[0, 0, 4] == num && setBox[1, 1, 3] == num && setBox[2, 2, 2] == num && setBox[3, 3, 1] == num)
                {
                    Debug.Log(winner + "-Win-");
                }
                else if (setBox[1, 1, 3] == num && setBox[2, 2, 2] == num && setBox[3, 3, 1] == num && setBox[4, 4, 0] == num)
                {
                    Debug.Log(winner + "-Win-");
                }
            }
        }
    }



//------------------------------------------------------------------------------------------------------ButtonArea----------------------------------------------------------------------------------------------------------------//



    public void UpButton()
    {
        Vector3 spherePos = sphere_tf.position;

        if (spherePos.z < maxUpSize && spherePos.y == 7)  
        {
            spherePos += new Vector3(0, 0, 2);
            sphere_tf.position = spherePos;
        }

    }

    public void DownButton()
    {
        Vector3 spherePos = sphere_tf.position;

        if (spherePos.z > maxDownSize && spherePos.y == 7)
        {
            spherePos += new Vector3(0, 0, -2);
            sphere_tf.position = spherePos;
        }
    }

    public void RightButton()
    {
        Vector3 spherePos = sphere_tf.position;

        if (spherePos.x < maxRightSize && spherePos.y == 7)
        {
            spherePos += new Vector3(2, 0, 0);
            sphere_tf.position = spherePos;
        }
    }

    public void LeftButton()
    {
        Vector3 spherePos = sphere_tf.position;

        if (spherePos.x > maxLeftSize && spherePos.y == 7)
        {
            spherePos += new Vector3(-2, 0, 0);
            sphere_tf.position = spherePos;
        }
    }

    public void DecisionButton()    //---------KeyFunction--------//    Sphereの設置＆配列への代入(座標の設定)
    {
        if (IsDecision() == false)
        {
            int X = (int)sphere_tf.position.x / 2 + 2;
            int Z = (int)sphere_tf.position.z / 2 + 2;
            sphere_rb.useGravity = true;

            if (sphere_rb.velocity.y == 0)
            {
                for (int y = 0; y < 5; y++)
                {
                    if (setBox[X, Z, y] == num_null)
                    {
                        if (gm.is1P == true)
                        {
                            setBox[X, Z, y] = num_1p;

                            Debug.Log("1P:" + X + "," + Z + "," + y + "=" + setBox[X, Z, y]);

                            return;
                        }
                        else if (gm.is1P == false)
                        {
                            setBox[X, Z, y] = num_2p;

                            Debug.Log("2P:" + X + "," + Z + "," + y + "=" + setBox[X, Z, y]);

                            return;
                        }
                    }
                }
            }
        }
    }

    public void NextPlayerButton()
    {
        if (sphere_tf.position.y < 6.5 && sphere_rb.velocity.y == 0)
        {
            if (gm.is1P == true)
            {
                gm.is1P = false;
            }
            else if (gm.is1P == false)
            {
                gm.is1P = true;
            }
            GenerateSphere();
        }
    }*/
}



