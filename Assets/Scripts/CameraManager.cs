using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private RenderTexture alleyway_texture_input, alleyway_texture_output;
    [SerializeField] private GameObject main_cam, alley_cam, street_cam, roof_cam, decontamination_cam;
    private GameObject[] cameras;
    // Start is called before the first frame update
    void Start()
    {
        cameras = new GameObject[]{main_cam, alley_cam, street_cam, roof_cam, decontamination_cam};
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void Switch_Cam(int cam_id){
        for(int i =0; i < cameras.Length; i++){
            if(i == cam_id){
                cameras[i].SetActive(true);
            }
            else{
                cameras[i].SetActive(false);
            }
        }
    }
}
