using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapTracker : MonoBehaviour
{
    public GameObject objectToTrack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(objectToTrack.transform.position.x, 30, objectToTrack.transform.position.z);
    }
}
