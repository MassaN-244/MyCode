using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GM;
using UM;
using UnityEngine.SceneManagement;

public class GenerateAndGameManager : MonoBehaviour
{
    [ColorUsage(false, true)] public Color color1;
    [ColorUsage(false, true)] public Color color2;

    [SerializeField] GameObject partitionPrefab;
    [SerializeField] GameObject redSpherePrefab;
    [SerializeField] GameObject blueSpherePrefab;
    [SerializeField] LayerMask sphereLayer;
    [SerializeField] GameObject player1Text;
    [SerializeField] GameObject player2Text;
    [SerializeField] GameObject player1NextText;
    [SerializeField] GameObject player2NextText;
    [SerializeField] GameObject winText1;
    [SerializeField] GameObject winText2;

    [SerializeField] AudioClip putSphere;
    [SerializeField] AudioClip collisionSphere;
    [SerializeField] AudioClip win;
    [SerializeField] AudioClip menu;
    [SerializeField] AudioClip goBack;
    [SerializeField] AudioClip nextPlayer;
    

    public GameObject menuCanvas;

    bool flag = true;

    AudioSource audioSource;

    Vector3 generateSpherePos = new Vector3(-3, 6, -3);

    GameManager gm = new GameManager();
    UIManager um = new UIManager();

    GameObject uiManager;
    GameObject sphereInstance;
    GameObject setWin;
    Color setColor;
    Transform sphere_tf;
    Rigidbody sphere_rb;

    const int maxUpSize = 3;
    const int maxDownSize = -3;
    const int maxRightSize = 3;
    const int maxLeftSize = -3;

    int[,,] setBox = new int[4, 4, 4];
    GameObject[,,] setGameObject = new GameObject[4, 4, 4];

    int num_1p = 0;
    int num_2p = 1;
    int num_null = 2;

    string winner;

    List<int> list_SetMemory_x = new List<int>();
    List<int> list_SetMemory_y = new List<int>();
    List<int> list_SetMemory_z = new List<int>();



    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();

        uiManager = GameObject.Find("UIManager");
        um = uiManager.GetComponent<UIManager>();
        for (int x = 0; x < 4; x++)
        {
            for (int z = 0; z < 4; z++)
            {
                for (int y = 0; y < 4; y++)
                {
                    setBox[x, z, y] = num_null;
                }
            }
        }
        
        sphereInstance = Instantiate(redSpherePrefab, generateSpherePos, Quaternion.identity);
        sphere_tf = sphereInstance.transform;
        sphere_rb = sphereInstance.GetComponent<Rigidbody>();
        GenerateParatition();

        //um.SetTextAndColor(player1Text, um.color1);
    }

    void Update()
    {
        if (flag == true)
        {
            JudgeWin(num_1p);
            JudgeWin(num_2p);
        }
        
        /*if (gm.is1P == true)
        {
            um.FadeIn(player1Text, um.color1);
        }
        else if (gm.is1P == false)
        {
            um.FadeIn(player2Text, um.color2);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    //---------------------------------------------------------------------------------------------------MethodArea---------------------------------------------------------------------------------------------------------------//

    public void GenerateParatition()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int z = 0; z < 4; z++)
                {
                    Vector3 generatePos = new Vector3(x * 2 - 3, y * 2 - 3, z * 2 - 3);
                    Instantiate(partitionPrefab, generatePos, Quaternion.identity);
                }
            }
        }
    }

    public void GenerateSphere()        //-------Sphere生成------//
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
        Vector3 endVec = sphere_tf.position - new Vector3(0, 3, 0);

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
                setWin = winText1;
                setColor = color1;
            }
            else if (num == num_2p)
            {
                winner = "Player2";
                setWin = winText2;
                setColor = color2;
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    //直線で揃う場合
                    if (setBox[0, i, j] == num && setBox[1, i, j] == num && setBox[2, i, j] == num && setBox[3, i, j] == num)
                    {
                        Debug.Log(winner + "-Win-");
                        setWin.SetActive(true);
                        audioSource.PlayOneShot(win); flag=false;

                        for (int k = 0; k < 4; k++)
                        {
                            setGameObject[k, i, j].GetComponent<Renderer>().material.SetColor("_EmissionColor", setColor);
                        }
                    }
                    else if (setBox[i, 0, j] == num && setBox[i, 1, j] == num && setBox[i, 2, j] == num && setBox[i, 3, j] == num)
                    {
                        Debug.Log(winner + "-Win-"); 
                        setWin.SetActive(true);
                        audioSource.PlayOneShot(win); flag=false;

                        for (int k = 0; k < 4; k++)
                        {
                            setGameObject[i, k, j].GetComponent<Renderer>().material.SetColor("_EmissionColor", setColor);
                        }
                    }
                    else if (setBox[i, j, 0] == num && setBox[i, j, 1] == num && setBox[i, j, 2] == num && setBox[i, j, 3] == num)
                    {
                        Debug.Log(winner + "-Win-"); 
                        setWin.SetActive(true);
                        audioSource.PlayOneShot(win); flag=false;

                        for (int k = 0; k < 4; k++)
                        {
                            setGameObject[i, j, k].GetComponent<Renderer>().material.SetColor("_EmissionColor", setColor);
                        }
                    }
                }

                //平面上で斜め（右上がり）に揃う場合
                if (setBox[i, 0, 0] == num && setBox[i, 1, 1] == num && setBox[1, 2, 2] == num && setBox[i, 3, 3] == num)
                {
                    Debug.Log(winner + "-Win-"); 
                    setWin.SetActive(true);
                    audioSource.PlayOneShot(win); flag=false;

                    for (int k = 0; k < 4; k++)
                    {
                        setGameObject[i, k, k].GetComponent<Renderer>().material.SetColor("_EmissionColor", setColor);
                    }
                }
                else if (setBox[0, i, 0] == num && setBox[1, i, 1] == num && setBox[2, i, 2] == num && setBox[3, i, 3] == num)
                {
                    Debug.Log(winner + "-Win-"); 
                    setWin.SetActive(true);
                    audioSource.PlayOneShot(win); flag=false;

                    for (int k = 0; k < 4; k++)
                    {
                        setGameObject[k, i, k].GetComponent<Renderer>().material.SetColor("_EmissionColor", setColor);
                    }
                }
                else if (setBox[0, 0, i] == num && setBox[1, 1, i] == num && setBox[2, 2, i] == num && setBox[3, 3, i] == num)
                {
                    Debug.Log(winner + "-Win-"); 
                    setWin.SetActive(true);
                    audioSource.PlayOneShot(win); flag=false;

                    for (int k = 0; k < 4; k++)
                    {
                        setGameObject[k, k, i].GetComponent<Renderer>().material.SetColor("_EmissionColor", setColor);
                    }
                }

                //平面上で斜め（右下がり）に揃う場合
                if (setBox[i, 3, 0] == num && setBox[i, 2, 1] == num && setBox[i, 1, 2] == num && setBox[i, 0, 3] == num)
                {
                    Debug.Log(winner + "-Win-"); 
                    setWin.SetActive(true);
                    audioSource.PlayOneShot(win); flag=false;

                    for (int k = 0; k < 4; k++)
                    {
                        setGameObject[i, 3 - k, k].GetComponent<Renderer>().material.SetColor("_EmissionColor", setColor);
                    }
                }
                else if (setBox[3, i, 0] == num && setBox[2, i, 1] == num && setBox[1, i, 2] == num && setBox[0, i, 3] == num)
                {
                    Debug.Log(winner + "-Win-"); 
                    setWin.SetActive(true);
                    audioSource.PlayOneShot(win); flag=false;

                    for (int k = 0; k < 4; k++)
                    {
                        setGameObject[3 - k, i, k].GetComponent<Renderer>().material.SetColor("_EmissionColor", setColor);
                    }
                }
                else if (setBox[3, 0, i] == num && setBox[2, 1, i] == num && setBox[1, 2, i] == num && setBox[0, 3, i] == num)
                {
                    Debug.Log(winner + "-Win-"); 
                    setWin.SetActive(true);
                    audioSource.PlayOneShot(win); flag=false;

                    for (int k = 0; k < 4; k++)
                    {
                        setGameObject[3 - k, k, i].GetComponent<Renderer>().material.SetColor("_EmissionColor", setColor);
                    }
                }

                //立体の対角線上で斜めに揃う場合
                if (setBox[3, 0, 0] == num && setBox[2, 1, 1] == num && setBox[1, 2, 2] == num && setBox[0, 3, 3] == num)
                {
                    Debug.Log(winner + "-Win-"); 
                    setWin.SetActive(true);
                    audioSource.PlayOneShot(win); flag=false;

                    for (int k = 0; k < 4; k++)
                    {
                        setGameObject[3 - k, k, k].GetComponent<Renderer>().material.SetColor("_EmissionColor", setColor);
                    }
                }
                else if (setBox[0, 0, 0] == num && setBox[1, 1, 1] == num && setBox[2, 2, 2] == num && setBox[3, 3, 3] == num)
                {
                    Debug.Log(winner + "-Win-"); 
                    setWin.SetActive(true);
                    audioSource.PlayOneShot(win); flag=false;

                    for (int k = 0; k < 4; k++)
                    {
                        setGameObject[k, k, k].GetComponent<Renderer>().material.SetColor("_EmissionColor", setColor);
                    }
                }
                else if (setBox[0, 3, 0] == num && setBox[1, 2, 1] == num && setBox[2, 1, 2] == num && setBox[3, 0, 3] == num)
                {
                    Debug.Log(winner + "-Win-"); 
                    setWin.SetActive(true);
                    audioSource.PlayOneShot(win); flag=false;

                    for (int k = 0; k < 4; k++)
                    {
                        setGameObject[k, 3 - k, k].GetComponent<Renderer>().material.SetColor("_EmissionColor", setColor);
                    }
                }
                else if (setBox[0, 0, 3] == num && setBox[1, 1, 2] == num && setBox[2, 2, 1] == num && setBox[3, 3, 0] == num)
                {
                    Debug.Log(winner + "-Win-"); 
                    setWin.SetActive(true);
                    audioSource.PlayOneShot(win); flag=false;

                    for (int k = 0; k < 4; k++)
                    {
                        setGameObject[k, k, 3 - k].GetComponent<Renderer>().material.SetColor("_EmissionColor", setColor);
                    }
                }
            }
        }
    }



    //------------------------------------------------------------------------------------------------------ButtonArea----------------------------------------------------------------------------------------------------------------//



    public void UpButton()
    {
        Vector3 spherePos = sphere_tf.position;

        if (spherePos.z < maxUpSize && spherePos.y == 6)
        {
            spherePos += new Vector3(0, 0, 2);
            sphere_tf.position = spherePos;
        }
    }

    public void DownButton()
    {
        Vector3 spherePos = sphere_tf.position;

        if (spherePos.z > maxDownSize && spherePos.y == 6)
        {
            spherePos += new Vector3(0, 0, -2);
            sphere_tf.position = spherePos;
        }
    }

    public void RightButton()
    {
        Vector3 spherePos = sphere_tf.position;

        if (spherePos.x < maxRightSize && spherePos.y == 6)
        {
            spherePos += new Vector3(2, 0, 0);
            sphere_tf.position = spherePos;
        }
    }

    public void LeftButton()
    {
        Vector3 spherePos = sphere_tf.position;

        if (spherePos.x > maxLeftSize && spherePos.y == 6)
        {
            spherePos += new Vector3(-2, 0, 0);
            sphere_tf.position = spherePos;
        }
    }

    public void DecisionButton()    //---------KeyFunction--------//    Sphereの設置＆配列への代入(座標の設定)
    {
        if (IsDecision() == false && sphereInstance.transform.position.y == 6) 
        {
            audioSource.PlayOneShot(putSphere);

            int X = (int)(sphere_tf.position.x + 3) / 2;
            int Z = (int)(sphere_tf.position.z + 3) / 2;
            sphere_rb.useGravity = true;

            if (sphere_rb.velocity.y == 0)
            {
                for (int y = 0; y < 4; y++)
                {
                    if (setBox[X, Z, y] == num_null)
                    {
                        if (gm.is1P == true)
                        {
                            setBox[X, Z, y] = num_1p;
                            setGameObject[X, Z, y] = sphereInstance;

                            Debug.Log("1P:" + X + "," + Z + "," + y + "=" + setGameObject[X, Z, y]);
                            Debug.Log("1P:" + X + "," + Z + "," + y + "=" + setBox[X, Z, y]);

                            list_SetMemory_x.Add(X);
                            list_SetMemory_y.Add(Z);
                            list_SetMemory_z.Add(y);

                            return;
                        }
                        else if (gm.is1P == false)
                        {
                            setBox[X, Z, y] = num_2p;
                            setGameObject[X, Z, y] = sphereInstance;

                            Debug.Log("2P:" + X + "," + Z + "," + y + "=" + setGameObject[X, Z, y]);
                            Debug.Log("2P:" + X + "," + Z + "," + y + "=" + setBox[X, Z, y]);

                            list_SetMemory_x.Add(X);
                            list_SetMemory_y.Add(Z);
                            list_SetMemory_z.Add(y);

                            return;
                        }
                    }
                }
            }
        }
    }

    public void NextPlayerButton()
    {
        if (sphere_tf.position.y < 4 && sphere_rb.velocity.y == 0)
        {
            audioSource.PlayOneShot(nextPlayer);
            if (gm.is1P == true)
            {
                gm.is1P = false;

                player1Text.SetActive(false);
                player2Text.SetActive(true);

                player1NextText.SetActive(true);
                player2NextText.SetActive(false);

                //um.SetTextAndColor(player2Text, um.color2);
            }
            else if (gm.is1P == false)
            {
                gm.is1P = true;

                player2Text.SetActive(false);
                player1Text.SetActive(true);

                player1NextText.SetActive(false);
                player2NextText.SetActive(true);

                //um.SetTextAndColor(player1Text, um.color1);
            }
            GenerateSphere();
        }
    }

    public void GoBack()
    {
        audioSource.PlayOneShot(goBack);
        /*if (IsDecision() == false && list_SetMemory_x.Count != 0) 
        {
            Destroy(sphereInstance);
            setBox[list_SetMemory_x[list_SetMemory_x.Count - 1], list_SetMemory_y[list_SetMemory_y.Count - 1], list_SetMemory_z[list_SetMemory_z.Count - 1]] = num_null;
            GenerateSphere();
        }*/
    }

    public void ResetGameButton()
    {
        audioSource.PlayOneShot(menu);
        SceneManager.LoadScene("MainScene");
    }

    public void TitleButton()
    {
        audioSource.PlayOneShot(menu);
        SceneManager.LoadScene("TitleScene");
    }

    public void MenuButton()
    {
        menuCanvas.SetActive(true);
        audioSource.PlayOneShot(menu);
    }

    public void ReturnButton()
    {
        menuCanvas.SetActive(false);
        audioSource.PlayOneShot(menu);
    }
}
