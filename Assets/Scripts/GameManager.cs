using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TimeManager time_manager;
    [SerializeField] private UIManager ui_manager;
    [SerializeField] private CameraManager camera_manager;
    [SerializeField] private RythmGameManager rythm_game_manager;
    [SerializeField] private PlayerController player_controller;
    [SerializeField] private CatManager cat_manager;  
    [SerializeField] private MusicManager music_manager;    

    [SerializeField] private GameObject[] door_array;
    [SerializeField] private int end_of_day_time = 22;
    private int day_number = 0;
    private bool end_game = false;
    public int cat_place;

    private int num_cats = 0;

    private bool start = true;

    private VideoPlayer videoPlayer;
    public GameObject videoobj;

    // Start is called before the first frame update
    void Start()
    {

        videoPlayer = GetComponent<VideoPlayer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(start){
            if(Input.anyKey){
            Intro();
            }
        }
        
    }

    void Intro(){
        ui_manager.intro_ident_panel.SetActive(false);

        videoPlayer.renderMode = VideoRenderMode.MaterialOverride;
        videoPlayer.targetMaterialRenderer = videoobj.GetComponent<Renderer>();
        videoPlayer.targetMaterialProperty = "_MainTex";

        videoPlayer.loopPointReached += OnVideoFinished;

        videoPlayer.Play();
        
    }
    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Video finished playing!");

        // Call your method or perform any action here
        Setup_Day();

        // Hide the video by disabling the VideoPlayer component or setting the GameObject inactive
        videoPlayer.enabled = false;
        videoobj.SetActive(false);
        // Or: gameObject.SetActive(false);
        player_controller.setup();
    }

    public void End_Day(){
        time_manager.End_Day();
        List<Cat> cats = cat_manager.Get_Cats();
        Cat last_cat = cats[0];
        cat_manager.Remove_Cat(last_cat);
        Destroy(last_cat);


        

        if(!end_game){
            
            print("End Day");
            Setup_Day();
            
        }
    }

    private void Setup_Day(){
        day_number++;
        Set_Day_Number(day_number);

        
        ui_manager.New_Day(day_number);
        

        
    }
    public void Play_Marge(){
        music_manager.PlayMargeCallDay(day_number);
    }
    public void New_Day(){
        if(day_number == 9){
            End_Game();
            return;
        }
        ui_manager.Get_Marge_Call_Panel();

        cat_manager.Spawn_Cat();
        time_manager.New_Day();
    }


    private void Set_Day_Number(int num){
        //Debug.Log("GameManager: Set_Day_Number");
        day_number = num;
        
    }

    public int Get_Day_Number(){
        return day_number;
    }

    public void Set_Time(int game_time_hours, int game_time_minutes, string time_string){
        ui_manager.Set_Clock(time_string);
        cat_manager.Move_Cats_Random();

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
        Debug.Log("Cat Place: " + (int)(door_id+1));

        if(cat_place == door_id + 1){
            //launch rythm game
            time_manager.PauseClock();
            rythm_game_manager.gameObject.SetActive(true);
            Cat cat = cat_manager.Get_Cat(0);
            rythm_game_manager.OnCreate(cat, door_id);

            
            door_array[door_id].GetComponent<Door>().Open();
        }
        else{
            player_controller.Close_Door();
        }
    }

    public void Close_Door(bool success, int door_id, Cat cat){
        rythm_game_manager.gameObject.SetActive(false);
        if(success){
            print("YAAAAAY");
            num_cats ++;
            print("Num Cats: " + num_cats);
            cat_manager.Remove_Cat(cat);
            cat_manager.Spawn_Cat();
        }
        else{
            print("AWWWWWW");
            print("Num Cats: " + num_cats);
        }

        time_manager.ResumeClock();
        player_controller.Close_Door();
        
        door_array[door_id].GetComponent<Door>().Close();


        

    }

    public void Move_Rythm_Cat(Cat cat, int dir){
        cat_manager.Move_Cat(cat, dir);
    }
}
