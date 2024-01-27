using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TimeManager time_manager;
    [SerializeField] private UIManager ui_manager;
    [SerializeField] private CameraManager camera_manager;

    private int day_number = 0;
    private bool end_game = false;

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

        if(game_time_hours == 10){
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
}
