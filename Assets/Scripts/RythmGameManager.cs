using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.Random;

public class RythmGameManager : MonoBehaviour
{
    [SerializeField] private GameObject target_p, target_s, pawprints_p, pawprints_s;
    private float success_meter;

    private GameObject[] p_array, s_array;

    [SerializeField] private GameObject paw_prefab;
    // Start is called before the first frame update
    void OnEnable()
    {
        p_array = new GameObject[10];
        s_array = new GameObject[10];
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.frameCount % 10 == 0){
            Generate_Paw();
        }
        if(Input.GetKeyDown(KeyCode.P)){
            print("P Attempt");
        }
        if(Input.GetKeyDown(KeyCode.S)){
            print("S Attempt");
        }
    }

    private void Generate_Paw(){
        print("Generate Paw");
        GameObject parent_obj = Choose_Side();
        Vector3 spawn_pos = new Vector3(parent_obj.transform.position.x, -1000, parent_obj.transform.position.z);
        GameObject new_paw = Instantiate(paw_prefab, spawn_pos, Quaternion.identity);
        new_paw.transform.SetParent(parent_obj.transform);


    }

    private GameObject Choose_Side(){
        float picker = Random.Range(0.0f, 1.0f);

        if(picker > 0.5f){
            return pawprints_p;
        }
        else{
            return pawprints_s;
        }
    }
}
