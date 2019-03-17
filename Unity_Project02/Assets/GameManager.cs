using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] plugs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < plugs.Length; i++)
        {
            Debug.Log(i);
            plugs[i].GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
