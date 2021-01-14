using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] GameObject partitionPrefab;
    [SerializeField] GameObject redSpherePrefab;
    [SerializeField] GameObject blueSpherePrefab;
    [SerializeField] AudioClip selectButton;

    AudioSource audioSource;

    GameObject obj; //子にする媒介用

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        GenerateParatition_Title();
        GenerateSphere_Title();
    }

    void Update()
    {
        this.transform.Rotate(new Vector3(0, 0.1f, 0));
    }

    public void GenerateParatition_Title()
    {
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                for (int z = 0; z < 4; z++)
                {
                    Vector3 generatePos = new Vector3(x * 2 - 3, y * 2 - 3, z * 2 - 3);
                    obj = (GameObject)Instantiate(partitionPrefab, generatePos, Quaternion.identity);
                    obj.transform.parent = this.transform;
                }
            }
        }
    }

    public void GenerateSphere_Title()
    {
            //RedSphere生成
        obj = (GameObject)Instantiate(redSpherePrefab, new Vector3(-3,-3,-3), Quaternion.identity);
        obj.transform.parent = this.transform;

        obj = (GameObject)Instantiate(redSpherePrefab, new Vector3(-3, -3, 1), Quaternion.identity);
        obj.transform.parent = this.transform;

        obj = (GameObject)Instantiate(redSpherePrefab, new Vector3(1, -3, -3), Quaternion.identity);
        obj.transform.parent = this.transform;

        obj = (GameObject)Instantiate(redSpherePrefab, new Vector3(-1, -3, 1), Quaternion.identity);
        obj.transform.parent = this.transform;

        obj = (GameObject)Instantiate(redSpherePrefab, new Vector3(1, -3, 1), Quaternion.identity);
        obj.transform.parent = this.transform;

        obj = (GameObject)Instantiate(redSpherePrefab, new Vector3(1, -1, 1), Quaternion.identity);
        obj.transform.parent = this.transform;

        obj = (GameObject)Instantiate(redSpherePrefab, new Vector3(1, -1, -1), Quaternion.identity);
        obj.transform.parent = this.transform;

            //BlueSphere生成
        obj = (GameObject)Instantiate(blueSpherePrefab, new Vector3(-1, -3, -1), Quaternion.identity);
        obj.transform.parent = this.transform;

        obj = (GameObject)Instantiate(blueSpherePrefab, new Vector3(1, -3, -1), Quaternion.identity);
        obj.transform.parent = this.transform;

        obj = (GameObject)Instantiate(blueSpherePrefab, new Vector3(3, -3, -1), Quaternion.identity);
        obj.transform.parent = this.transform;

        obj = (GameObject)Instantiate(blueSpherePrefab, new Vector3(-1, -1, 1), Quaternion.identity);
        obj.transform.parent = this.transform;

        obj = (GameObject)Instantiate(blueSpherePrefab, new Vector3(1, 1, 1), Quaternion.identity);
        obj.transform.parent = this.transform;

        obj = (GameObject)Instantiate(blueSpherePrefab, new Vector3(-1, -3, 3), Quaternion.identity);
        obj.transform.parent = this.transform;

        obj = (GameObject)Instantiate(blueSpherePrefab, new Vector3(3, -3, 3), Quaternion.identity);
        obj.transform.parent = this.transform;
    }

    public void LoadMainScene()
    {
        audioSource.PlayOneShot(selectButton);
        SceneManager.LoadScene("MainScene");

    }
}
