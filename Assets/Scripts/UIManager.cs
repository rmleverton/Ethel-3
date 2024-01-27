using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text day_number_text;
    private string day_number_string;

    [SerializeField] private GameObject day_ident_panel;
    [SerializeField] private GameObject end_game_ident_panel;

    private IEnumerator day_ident_panel_splash;
    [SerializeField] private float day_ident_panel_splash_time = 5.0f;
    [SerializeField] private TMP_Text clock_text;

    // Start is called before the first frame update
    void Start()
    {
        day_ident_panel_splash = Start_Splash(day_ident_panel_splash_time);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void New_Day(int day_num){
        Set_Day_Number_Text(day_num);

        
        StartCoroutine(day_ident_panel_splash);
        Show_Splash(day_ident_panel);
    }

    private void Set_Day_Number_Text(int day){
        day_number_string = "Day " + day;
        day_number_text.text = day_number_string;
        //Debug.Log("Day_Num_T: " + day_number_string);
    }

    private void Show_Splash(GameObject gameObject){
        gameObject.SetActive(true);
    }
    private void Hide_Splash(GameObject gameObject){
        gameObject.SetActive(false);
        StopCoroutine("Start_Splash");
    }


    private IEnumerator Start_Splash(float waitTime){
        while(true)
        {
            
            yield return new WaitForSeconds(waitTime);
            // print("UI:")
            
            Hide_Splash(day_ident_panel);
            
            
        }
    }

    public void Set_Clock(string clock_string){
        clock_text.text = clock_string;
    }

    public void End_Game(){
        
        Show_Splash(end_game_ident_panel);
    }
}
