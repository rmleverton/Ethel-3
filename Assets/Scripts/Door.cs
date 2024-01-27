using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(){
        // transform.rotation = Quaternion.Euler(0,-90,0);
        transform.Rotate(0, -90, 0);
    }

    public void Close(){
        // transform.rotation = Quaternion.Euler(0,0,0);
        transform.Rotate(0, 90, 0);
    }
}
