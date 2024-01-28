// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;
// using UnityEngine.UI;

// public class UIManager : MonoBehaviour
// {
//     [SerializeField] private TMP_Text day_number_text;
//     private string day_number_string;

//     [SerializeField] private GameObject day_ident_panel;
//     [SerializeField] private GameObject end_game_ident_panel;

//     private IEnumerator day_ident_panel_splash;
//     [SerializeField] private float day_ident_panel_splash_time = 5.0f;
//     [SerializeField] private TMP_Text clock_text;

//     // Start is called before the first frame update
//     void Start()
//     {
//         day_ident_panel_splash = Start_Splash(day_ident_panel_splash_time);
//     }

//     // Update is called once per frame
//     void Update()
//     {
       
//     }

//     public void New_Day(int day_num){
//         Set_Day_Number_Text(day_num);

        
//         StartCoroutine(day_ident_panel_splash);
//         Show_Splash(day_ident_panel);
//     }

//     private void Set_Day_Number_Text(int day){
//         day_number_string = "Day " + day;
//         day_number_text.text = day_number_string;
//         //Debug.Log("Day_Num_T: " + day_number_string);
//     }

//     private void Show_Splash(GameObject gameObject){
//         gameObject.SetActive(true);
//     }
//     private void Hide_Splash(GameObject gameObject){
//         gameObject.SetActive(false);
//         StopCoroutine("Start_Splash");
//     }


//     private IEnumerator Start_Splash(float waitTime){
//         while(true)
//         {
            
//             yield return new WaitForSeconds(waitTime);
//             // print("UI:")
            
//             Hide_Splash(day_ident_panel);
            
            
//         }
//     }

//     public void Set_Clock(string clock_string){
//         clock_text.text = clock_string;
//     }

//     public void End_Game(){
        
//         Show_Splash(end_game_ident_panel);
//     }
// }


using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager game_manager;
    [SerializeField] private TMP_Text day_number_text;
    private string day_number_string;

    [SerializeField] private GameObject day_ident_panel;
    public GameObject intro_ident_panel;
    [SerializeField] private GameObject marge_call_panel;
    [SerializeField] private GameObject end_game_ident_panel;

    private Coroutine dayIdentPanelSplashCoroutine; // Store a reference to the coroutine
    [SerializeField] private float day_ident_panel_splash_time = 5.0f;
    [SerializeField] private TMP_Text clock_text;

    [SerializeField] private string[] marge_string_days;
    [SerializeField] private TMP_Text marge_day_text;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the coroutine reference
        //dayIdentPanelSplashCoroutine = StartCoroutine(Start_Splash(day_ident_panel_splash_time));
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void New_Day(int day_num)
    {
        // Stop the existing coroutine if it's running
        if (dayIdentPanelSplashCoroutine != null)
        {
            StopCoroutine(dayIdentPanelSplashCoroutine);
        }

        // Set the day number text
        Set_Day_Number_Text(day_num);

        // Start a new coroutine for the splash
        dayIdentPanelSplashCoroutine = StartCoroutine(Start_Splash(day_ident_panel_splash_time));

        marge_day_text.text = marge_string_days[day_num-1];
        // Show the splash panel
        Show_Splash(day_ident_panel);
    }

    private void Set_Day_Number_Text(int day)
    {
        day_number_string = "Day " + day;
        day_number_text.text = day_number_string;
    }

    private void Show_Splash(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    private void Hide_Splash(GameObject gameObject)
    {
        gameObject.SetActive(false);

        if(gameObject == day_ident_panel){
            print("day_ident");
            // Stop the existing coroutine if it's running
            if (dayIdentPanelSplashCoroutine != null)
            {
                StopCoroutine(dayIdentPanelSplashCoroutine);
            }
            Show_Splash(marge_call_panel);
            game_manager.Play_Marge();
        }
    }

    private IEnumerator Start_Splash(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);

            // Hide the splash panel after the wait time
            Hide_Splash(day_ident_panel);
        }
    }

    public void Set_Clock(string clock_string)
    {
        clock_text.text = clock_string;
    }

    public void End_Game()
    {
        // Show the end game splash panel
        Show_Splash(end_game_ident_panel);
    }

    public void Get_Marge_Call_Panel(){
        print("marge off");
        marge_call_panel.SetActive(false);
    }
}
