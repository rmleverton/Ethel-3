using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawScript : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    // Start is called before the first frame update
    void OnEnable()
    {
        //transform.position = new Vector3(transform.parent.position.x, -1000, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(0, speed * Time.deltaTime, 0);
        // print(speed * Time.deltaTime);
    }
}
