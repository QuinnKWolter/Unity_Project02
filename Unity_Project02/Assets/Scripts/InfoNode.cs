using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoNode : MonoBehaviour
{
    public float conversion, speed;
    public GameObject ring1, ring2, particles, light, influence, finale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ring1.transform.Rotate(Vector3.up, speed);
        ring2.transform.Rotate(Vector3.left, speed);
    }

    public void Convert()
    {
        if (conversion <= 0)
        {
            influence.SetActive(false);
            light.SetActive(false);
            particles.SetActive(false);
            ring1.SetActive(false);
            ring2.SetActive(false);
            finale.SetActive(true);
        } else
        {
            conversion -= .5f;
            speed += .1f;
            Debug.Log(conversion);
        } 
    }
}
