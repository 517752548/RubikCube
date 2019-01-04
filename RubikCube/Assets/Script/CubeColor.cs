using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeColor : MonoBehaviour
{
    public GameObject[] g;
    const int size = 6;
    const int top = 0;
    const int bottom = 1;
    const int right = 2;
    const int left = 3;
    const int foward = 4;
    const int back = 5;

    void Start()
    {
        for (int i = 0; i < 6; ++i)
        {
            g[i].SetActive(false);
        }
        setColor();
        Debug.Log("cube start()");
    }

    void Update()
    {
    }

    void setColor()
    {

        if(Mathf.RoundToInt(this.transform.position.x) >= 2)
        {
            g[right].SetActive(true);
        }
        else if(Mathf.RoundToInt(this.transform.position.x) <= 0)
        {
            g[left].SetActive(true);
            //this.transform.FindChild("Bot").gameObject.SetActive(true);
        }
        if (Mathf.RoundToInt(this.transform.position.y) >= 2)
        {
            g[top].SetActive(true);
        }
        else if (Mathf.RoundToInt(this.transform.position.y) <= 0)
        {
            g[bottom].SetActive(true);
        }
        if (Mathf.RoundToInt(this.transform.position.z) >= 2)
        {
            g[foward].SetActive(true);
        }
        else if (Mathf.RoundToInt(this.transform.position.z) <= 0)
        {
            g[back].SetActive(true);
        }
    }
}
