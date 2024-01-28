using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.Random;

public class RythmGameManager : MonoBehaviour
{

    [SerializeField] private GameManager game_manager;

    [SerializeField] private GameObject success_screen;

    [SerializeField] private GameObject target_p, target_s, pawprints_p, pawprints_s, cur_p, cur_s;
    private float success_meter = 0;
    private float success_target = 10;

    [SerializeField] private List<GameObject> p_array;// = new List<GameObject>();
    [SerializeField] private List<GameObject> s_array;// = new List<GameObject>();

    [SerializeField] private GameObject paw_prefab;
    [SerializeField] private float lower_bound = -100f, upper_bound = 100f;

    private bool running = true;
    private int spawn_rate = 50;
    private int fail_num = 0;
    private int fail_target = 5;

    private int cat_pos;
    private int door_number;

    private Cat _cat;
    // Start is called before the first frame update
    void OnEnable()
    {
        
    }

    public void OnCreate(Cat cat, int door_id){
        p_array = new List<GameObject>();
        s_array = new List<GameObject>();

        _cat = cat;

        success_meter = 0;
        fail_num = 0;

        int[] cat_loc = cat.Get_Location();
        cat_pos = cat_loc[1];
        running = true;

        door_number = door_id;

    }

    // Update is called once per frame
    void Update()
    {
        if(running){
            if(fail_num == fail_target){
                Fail();
            }
            if(success_meter >= success_target){
                Succeed();
            }
            if(p_array.Count > 0){
                if( p_array[0].GetComponent<RectTransform>().anchoredPosition.y > upper_bound){
                    p_array.Remove(p_array[0]);
                    fail_num++;
                    print("Fail Num: " + fail_num);
                }
            }
            if(s_array.Count > 0){
                if(s_array[0].GetComponent<RectTransform>().anchoredPosition.y > upper_bound){
                    s_array.Remove(s_array[0]);
                    fail_num++;
                    print("Fail Num: " + fail_num);
                }
            }
            if(Time.frameCount % spawn_rate == 0){
                Generate_Paw();
            }
            if(Input.GetKeyDown(KeyCode.P)){
                //print("P Attempt");
                Check_Attempt(p_array[0], p_array);
            }
            if(Input.GetKeyDown(KeyCode.S)){
                //print("S Attempt");
                Check_Attempt(s_array[0], s_array);
            }
        }
    }

    private void Generate_Paw(){
        //print("Generate Paw");
        GameObject parent_obj;
        
        float picker = Random.Range(0.0f, 1.0f);
        var parent_list = new List<GameObject>();

        if(picker > 0.5f){
            parent_obj = pawprints_p;
            parent_list = p_array;
        }
        else{
            parent_obj = pawprints_s;
            parent_list = s_array;
        }

        Vector3 spawn_pos = new Vector3(parent_obj.transform.position.x, -1000, parent_obj.transform.position.z);
        GameObject new_paw = Instantiate(paw_prefab, spawn_pos, Quaternion.identity);
        new_paw.transform.SetParent(parent_obj.transform);

        parent_list.Add(new_paw);


    }


    private void Check_Attempt(GameObject gameObject, List<GameObject> parent_list){

        if(gameObject.GetComponent<RectTransform>().anchoredPosition.y > lower_bound && gameObject.GetComponent<RectTransform>().anchoredPosition.y < upper_bound){
            parent_list.Remove(gameObject);
            print("Success");
            success_meter ++;
            
            //Destroy(gameObject);
            gameObject.GetComponent<Image>().enabled = false;
        }

        else{
            parent_list.Remove(gameObject);
            print("fail");
            fail_num ++;
            print(fail_num);
        }
    }

    private void Fail(){
        cat_pos --;
        print("Cat Pos: " + cat_pos);

        game_manager.Move_Rythm_Cat(_cat, -1);

        if(cat_pos == 0){
            
            //fail screen
            End_Rythm_Game(false);
            return;
        }
       
        fail_num = 0;
        success_meter = 0;

        

    }

    private void Succeed(){
        cat_pos ++;
        print("Cat Pos: " + cat_pos);

        if(cat_pos == 4){
            //success screen
            End_Rythm_Game(true);
            return;
        }

        fail_num = 0;
        success_meter = 0;

        game_manager.Move_Rythm_Cat(_cat, 1 );

    }

    private void End_Rythm_Game(bool success){
        
        running = false;

        success_screen.SetActive(true);

         // Start a coroutine to introduce a delay before calling Close_Door
        StartCoroutine(DelayBeforeCloseDoor(success));
    }

    // Coroutine to introduce a delay before calling Close_Door
    private IEnumerator DelayBeforeCloseDoor(bool success)
    {
        // Wait for 5 seconds (adjust the time as needed)
        yield return new WaitForSeconds(5f);

        // After the delay, call the Close_Door method
        game_manager.Close_Door(success, door_number, _cat);
        success_screen.SetActive(false);
    }
}