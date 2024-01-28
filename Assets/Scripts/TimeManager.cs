using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    private float day_start_time;
    private int clock_tick_counter;
    // [SerializeField] private int day_length = 10;
    // private int day_counter; 

    private IEnumerator day_cycle;
    private string clock_time;
    private int clock_hours;
    private int clock_minutes;
    private bool pause_clock = false;

    [SerializeField] private GameManager game_manager;
    // Start is called before the first frame update
    void Start()
    {


        // day_counter = 1;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Start_Clock(float waitTime){
        

        while(true)
        {
            yield return new WaitUntil(() => !pause_clock);
                yield return new WaitForSeconds(waitTime);
                Clock_Tick();
            

        }
    }

    public void End_Day(){
        StopCoroutine(day_cycle);

    }

    public void New_Day(){
        
        //print("Day: " + day_counter);
        
        clock_tick_counter = 0;

        day_cycle = Start_Clock(1.0f);
        StartCoroutine(day_cycle);
        Reset_Clock();
    }

    private void Clock_Tick(){
        clock_tick_counter ++;

        clock_minutes += 6;
        clock_minutes %= 60;

        if(clock_minutes == 0){
            clock_hours ++;
        }

        clock_time = clock_hours.ToString("D2") + ":" + clock_minutes.ToString("D2");
        //print(clock_time);
        game_manager.Set_Time(clock_hours, clock_minutes, clock_time);

    }
    private void Reset_Clock(){
        clock_hours = 7;
        clock_minutes = 0;
    }

    public void PauseClock()
    {
        pause_clock = true;
    }

// Call this method to resume the clock coroutine
    public void ResumeClock()
    {
        pause_clock = false;
    }

}
