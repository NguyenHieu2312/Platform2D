using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Transform cam;
    Vector3 startCampos;
    private float distance;


    GameObject[] backgrounds;
    Material[] mat;
    private float[] backgroundSpeed;

    float farthestBackground;


    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;


    private void Start()
    {
        cam = Camera.main.transform;
        startCampos = cam.position;

        int backgroundCount = transform.childCount;
        mat = new Material[backgroundCount];
        backgroundSpeed = new float[backgroundCount];
        backgrounds = new GameObject[backgroundCount];

        for (int i = 0;  i < backgroundCount ; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }

        BackGroundCaculate(backgroundCount);
    }

    void BackGroundCaculate(int backGroundcount)
    {
        for (int i = 0; i < backGroundcount; i++)
        {
            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBackground)
            {
                farthestBackground = backgrounds[i].transform.position.z - cam.position.z;
            }
        }

        for (int i = 0; i < backGroundcount; i++)
        {
            backgroundSpeed[i] = 1.5f - (backgrounds[i].transform.position.z - cam.position.z) / farthestBackground;
        }
    }


    private void LateUpdate()
    {
        distance = cam.position.x - startCampos.x;
        transform.position = new Vector3(cam.position.x, transform.position.y, transform.position.z);
        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backgroundSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance, 0) * speed);
        }
    }
}
