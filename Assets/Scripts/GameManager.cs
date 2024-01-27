using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TimeManager time_manager;
    [SerializeField] private UIManager ui_manager;
    [SerializeField] private CameraManager camera_manager;
    [SerializeField] private RythmGameManager rythm_game_manager;
    [SerializeField] private PlayerController player_controller;    

    [SerializeField] private GameObject[] door_array;
    [SerializeField] private int end_of_day_time = 22;
    private int day_number = 0;
    private bool end_game = false;

    private int num_cats = 0;

    // Start is called before the first frame update
    void Start()
    {
        New_Day();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void End_Day(){
        time_manager.End_Day();

        if(day_number == 9){
            End_Game();
        }

        if(!end_game){
            
            print("End Day");
            New_Day();
            
        }
    }
    private void New_Day(){
        day_number++;
        Set_Day_Number(day_number);

        time_manager.New_Day();
        ui_manager.New_Day(day_number);

    }


    private void Set_Day_Number(int num){
        //Debug.Log("GameManager: Set_Day_Number");
        day_number = num;
        
    }

    public int Get_Day_Number(){
        return day_number;
    }

    public void Set_Time(int game_time_hours, int game_time_mminutes, string time_string){
        ui_manager.Set_Clock(time_string);

        if(game_time_hours == end_of_day_time){
            End_Day();
        }
    }
    public void End_Game(){
        end_game = true;
        ui_manager.End_Game();
        print("Game Over");
    }

    public void Handle_Interaction(int computer_id, bool focus){
        if(focus){
            camera_manager.Switch_Cam(computer_id + 1);
        }
        else{
            camera_manager.Switch_Cam(0);
        }
    }

    public void Open_Door(int door_id){
        //launch rythm game
        rythm_game_manager.gameObject.SetActive(true);
        rythm_game_manager.OnCreate(1, door_id);
        time_manager.PauseClock();
        door_array[door_id].GetComponent<Door>().Open();
    }

    public void Close_Door(bool success, int door_id){
        rythm_game_manager.gameObject.SetActive(false);
        if(success){
            print("YAAAAAY");
            num_cats ++;
            print("Num Cats: " + num_cats);
        }
        else{
            print("AWWWWWW");
            print("Num Cats: " + num_cats);
        }

        time_manager.ResumeClock();
        player_controller.Close_Door();
        
        door_array[door_id].GetComponent<Door>().Close();

    }
}
