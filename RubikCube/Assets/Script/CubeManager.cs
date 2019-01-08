
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeManager : MonoBehaviour {
    bool isPossible = true;
    bool isShuffle = false;
    bool anyMove = false;
    const int randomCnt = 2;
    const int Size = 3;
    const int groupSize = 9;
    const float angleSpeed = 5f;
    const int X = 0;
    const int Y = 1;
    const int Z = 2;
    const int top = 0;
    const int bottom = 1;
    const int right = 2;
    const int left = 3;
    const int foward = 4;
    const int back = 5;
    const int End = 2;
    float wait = 0f;
    float rand;
    public GameObject[,,] allCube = new GameObject[3,3,3];
    public GameObject prefabCube;
    public GameObject cam;
    IEnumerator[] arrIenum = new IEnumerator[100];
    IEnumerator previous = null, current = null;

    void Start () {
        CreateCube();
	}
    
	void Update () {
        if (isPossible && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(CubeRotations(X, 0, Vector3.left));
            anyMove = true;
        }
        else if (isPossible && Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(CubeRotations(X, 1, Vector3.left));
            anyMove = true;
        }
        else if (isPossible && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(CubeRotations(X, 2, Vector3.left));
            anyMove = true;
        }
        else if (isPossible && Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(CubeRotations(Y, 0, Vector3.up));
            anyMove = true;
        }
        else if (isPossible && Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(CubeRotations(Y, 1, Vector3.up));
            anyMove = true;
        }
        else if (isPossible && Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(CubeRotations(Y, 2, Vector3.up));
            anyMove = true;
        }
        else if (isPossible && Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(CubeRotations(Z, 0, Vector3.forward));
            anyMove = true;
        }
        else if (isPossible && Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(CubeRotations(Z, 1, Vector3.forward));
            anyMove = true;
        }
        else if (isPossible && Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(CubeRotations(Z, 2, Vector3.forward));
            anyMove = true;
        }
        else if (isPossible && Input.GetKeyDown(KeyCode.Space))
        {
            anyMove = false; isShuffle = true; 
            //ClearCube();
            for(int count=0; count<30;++count)
            {
                Debug.Log(count);
                RandomizeCube(); wait += 0.4f;
            }
            cam.GetComponent<Camera>().backgroundColor = Color.white;
            current = null; previous =null; isShuffle = false;
            wait = 0f;
        }
        else if (isPossible && Input.GetKeyDown(KeyCode.Backspace))
        {
            anyMove = false;
            cam.GetComponent<Camera>().backgroundColor = Color.white;
            ClearCube();
        }
        if (GameEnd()==End)
        {
            cam.GetComponent<Camera>().backgroundColor = Color.black;
            Debug.Log("perfect cube");
        }
    }

    void CreateCube()
    {
        for (int i = 0; i < Size; ++i)
        {
            for (int j = 0; j < Size; ++j)
            {
                for (int k = 0; k < Size; ++k)
                {
                    Vector3 v = new Vector3(i, j, k);
                    allCube[i, j, k] = Instantiate(prefabCube);
                    allCube[i, j, k].transform.position = v;
                    allCube[i,j,k].name = ""+(9*i+j*3+k);
                }
            }
        }
    }

    // X = UP DOWN, Y = LEFT RIGHT, Z = FOWARD BACK
    IEnumerator CubeRotations(int xyz, int layer, Vector3 shaft)
    {
        yield return new WaitForSeconds(wait);
        //if(isShuffle)
        //    yield return StartCoroutine(current);
        isPossible = false; 
        int angle = 0;
        List<GameObject> listCube = new List<GameObject>();
        for(int i=0; i<Size; ++i)
        {
            for(int j=0; j<Size; ++j)   
            {
                for(int k=0; k<Size; ++k)
                {
                    if (xyz==X && Mathf.RoundToInt(allCube[i, j, k].transform.position.x) == layer)
                        listCube.Add(allCube[i, j, k]);
                    else if (xyz == Y && Mathf.RoundToInt(allCube[i, j, k].transform.position.y) == layer)
                        listCube.Add(allCube[i, j, k]);
                    else if (xyz == Z && Mathf.RoundToInt(allCube[i, j, k].transform.position.z) == layer)
                        listCube.Add(allCube[i, j, k]);
                }
            }
        }
        while (angle < 90)
        {
            for (int i = 0; i < groupSize; ++i)
            {
                listCube[i].transform.RotateAround(new Vector3(1f, 1f, 1f), shaft, angleSpeed);
            }
            angle += 5;
            yield return null;
        }
        isPossible = true;
    }

    void Rotations(int xyz, int layer, Vector3 shaft)
    {
        int angle = 0;
        isPossible = false;
        List<GameObject> listCube = new List<GameObject>();
        for (int i = 0; i < Size; ++i)
        {
            for (int j = 0; j < Size; ++j)
            {
                for (int k = 0; k < Size; ++k)
                {
                    if (xyz == X && Mathf.RoundToInt(allCube[i, j, k].transform.position.x) == layer)
                        listCube.Add(allCube[i, j, k]);
                    else if (xyz == Y && Mathf.RoundToInt(allCube[i, j, k].transform.position.y) == layer)
                        listCube.Add(allCube[i, j, k]);
                    else if (xyz == Z && Mathf.RoundToInt(allCube[i, j, k].transform.position.z) == layer)
                        listCube.Add(allCube[i, j, k]);
                }
            }
        }
        while (angle < 90)
        {
            for (int i = 0; i < groupSize; ++i)
            {
                listCube[i].transform.RotateAround(new Vector3(1f, 1f, 1f), shaft, 5f);
            }
            angle += 5;
        }
        isPossible = true;

    }

    void ClearCube()
    {
        for (int i = 0; i < Size; ++i)
        {
            for (int j = 0; j < Size; ++j)
            {
                for (int k = 0; k < Size; ++k)
                {
                    Destroy(allCube[i, j, k]);
                }
            }
        }
        CreateCube();
    }
    // 2바퀴 - ac
    int GameEnd()
    {
        if (!anyMove)
            return 1;
        //List<List<GameObject>> tmpList = new List<List<GameObject>>();
        List<GameObject> tmpListR = new List<GameObject>();
        List<GameObject> tmpListL = new List<GameObject>();
        List<GameObject> tmpListT = new List<GameObject>();
        List<GameObject> tmpListB = new List<GameObject>();
        List<GameObject> tmpListF = new List<GameObject>();
        List<GameObject> tmpListK = new List<GameObject>();

        // 6 direction 람다식 이용해보기
        for (int i = 0; i < Size; ++i)
        {
            for (int j = 0; j < Size; ++j)
            {
                for (int k = 0; k < Size; ++k)
                {
                    if (Mathf.RoundToInt(allCube[i, j, k].transform.position.x) == 2)
                        tmpListR.Add(allCube[i, j, k]);
                    if (Mathf.RoundToInt(allCube[i, j, k].transform.position.x) == 0)
                        tmpListL.Add(allCube[i, j, k]);
                    if (Mathf.RoundToInt(allCube[i, j, k].transform.position.y) == 2)
                        tmpListT.Add(allCube[i, j, k]);
                    if (Mathf.RoundToInt(allCube[i, j, k].transform.position.y) == 0)
                        tmpListB.Add(allCube[i, j, k]);
                    if (Mathf.RoundToInt(allCube[i, j, k].transform.position.z) == 2)
                        tmpListF.Add(allCube[i, j, k]);
                    if (Mathf.RoundToInt(allCube[i, j, k].transform.position.z) == 0)
                        tmpListK.Add(allCube[i, j, k]);
                }
            }
        }
        for (int d = 1; d < groupSize; ++d)
        {
            if (GtoQ(tmpListL[0]) != GtoQ(tmpListL[d]))
                return 0;
            if (GtoQ(tmpListR[0]) != GtoQ(tmpListR[d]))
                return 0;
            if (GtoQ(tmpListT[0]) != GtoQ(tmpListT[d]))
                return 0;
            if (GtoQ(tmpListB[0]) != GtoQ(tmpListB[d]))
                return 0;
            if (GtoQ(tmpListF[0]) != GtoQ(tmpListF[d]))
                return 0;
            if (GtoQ(tmpListK[0]) != GtoQ(tmpListK[d]))
                return 0;
        }
        return 2;
    }


    Quaternion GtoQ(GameObject q)
    {
        return new Quaternion(Mathf.Abs(q.transform.rotation.x),
                Mathf.Abs(q.transform.rotation.y),
                Mathf.Abs(q.transform.rotation.z),
                Mathf.Abs(q.transform.rotation.w));
    }

    void RandomizeCube()
    {
        int rand = Mathf.RoundToInt(Random.Range(0f, 10f) % 3);
        if (rand == 0)
        {
            StartCoroutine(previous = CubeRotations(X, Mathf.RoundToInt(Random.Range(0f, 9f)) % 3, Vector3.left));
            current = previous;
        }
        else if (rand == 1)
        {
            StartCoroutine(previous = CubeRotations(Y, Mathf.RoundToInt(Random.Range(0f, 9f)) % 3, Vector3.up));
            current = previous;
        }
        else if (rand == 2)
        {
            StartCoroutine(previous = CubeRotations(Z, Mathf.RoundToInt(Random.Range(0f, 9f)) % 3, Vector3.forward));
            current = previous;
        }
        //Rotations(X, 2, Vector3.left);
        //Rotations(Y, Mathf.RoundToInt(Random.Range(0f, 9f)) % 3, Vector3.up);
        //Rotations(Z, Mathf.RoundToInt(Random.Range(0f, 9f)) % 3, Vector3.forward);
    }
}   
