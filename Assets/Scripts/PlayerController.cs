using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private GameManager game_manager;

    private Vector3 player_transform;

    [SerializeField] private float input_delay = 0.2f;
    private float left_input_time = 0;
    private float right_input_time = 0;
    private float interact_input_time = 0;
    private float door_input_time = 0;
    private string current_computer;
    private int current_computer_id;
    private string[] computer_array;

    private bool move_controls_enabled = false;
    private bool door_enabled = false;
    private bool interact_enabled = false;
    // Start is called before the first frame update
    void Start()
    {

        computer_array = new string[]{"Front", "Right", "Back"};//, "Left"};
        current_computer_id = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // print("door enabled: " + door_enabled);
        if(move_controls_enabled){
            if(Input.GetAxis("Horizontal")> 0 && Time.time - right_input_time >= input_delay){
                Move_Right();
                right_input_time = Time.time;
            }
            else if (Input.GetAxis("Horizontal") < 0 && Time.time - left_input_time >= input_delay){
                Move_Left();
                left_input_time = Time.time;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)&& Time.time - interact_input_time >= input_delay){
            print("interacting space: " + interact_enabled);
        }
        if (Input.GetKeyDown(KeyCode.Space) && Time.time - interact_input_time >= input_delay && interact_enabled){
            print("interacting");
            Interact();
            interact_input_time = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.D)&& Time.time - door_input_time >= input_delay){
            print("interacting d: " + door_enabled);
        }
        if(Input.GetKeyDown(KeyCode.D) && Time.time - door_input_time >= input_delay && door_enabled){
            door_input_time = Time.time;
            print("opening door");
            
            door_enabled = false;
            interact_enabled = false;
            Open_Door();
        }
    }

    private void Move_Right(){
        print("Move Right");
        Vector3 old_rotation = transform.eulerAngles;
        old_rotation += new Vector3(0, 90, 0);
        transform.rotation = Quaternion.Euler(old_rotation); 

        Switch_Computer(1);
        print(current_computer);

    }

    private void Move_Left(){
        print("Move Left");
        Vector3 old_rotation = transform.eulerAngles;
        old_rotation += new Vector3(0, -90, 0);
        transform.rotation = Quaternion.Euler(old_rotation);

        Switch_Computer(-1);
        print(current_computer);
    }

    private void Interact(){
        print("Interaction");
        move_controls_enabled = !move_controls_enabled;
        door_enabled = !door_enabled;

        bool focus = !move_controls_enabled;
        print("focus: " + focus);
        game_manager.Handle_Interaction(current_computer_id, focus);

    }

    private void Switch_Computer(int dir){
        current_computer_id = current_computer_id + dir;
        if(current_computer_id <= -1){
            current_computer_id = 2;
        }
        else if (current_computer_id >= 3){
            current_computer_id = 0;
        }
    }

    private void Open_Door(){
        game_manager.Open_Door(current_computer_id);
    }

    public void Close_Door(){
        print("close door");
        door_enabled = true;
        interact_enabled = true;
    }

    public void setup(){
        
        interact_enabled = true;
        move_controls_enabled = true;
    }
}
