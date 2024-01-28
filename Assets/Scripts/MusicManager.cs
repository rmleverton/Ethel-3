using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    [SerializeField] private GameManager game_manager;
   public AudioSource audioSource;
   public AudioClip[] _dayAudioTracks;  // Array to hold audio clips for _days 1-9
   public AudioClip[] margeCallTracks;

   private int day;

    void Start()
    {
        // Accessing audio for _day 1 (index 0)
        //PlayAudioFor_day(1);
    }

    public void PlayAudioForDay(int _day)
    {
        // Check if the array has an entry for the specified _day
        if (_day >= 1 && _day <= _dayAudioTracks.Length)
        {
            // Play the audio clip for the specified _day
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = _dayAudioTracks[_day - 1];  // Adjusting for zero-based index

            // Set looping property
            audioSource.loop = true;

            audioSource.Play();
        }
        else
        {
            Debug.LogError("Invalid _day specified or no audio track available for that _day.");
        }
    }

    public void PlayMargeCallDay(int _day){
        day = _day;
        if (_day >= 1 && _day <= margeCallTracks.Length)
        {
            // Play the audio clip for the specified _day
            audioSource = gameObject.GetComponent<AudioSource>();
            audioSource.clip = margeCallTracks[_day - 1];  // Adjusting for zero-based index

            // Set looping property
            audioSource.loop = false;

            audioSource.Play();

            // Invoke a method after the audio clip duration
            Invoke("AudioFinishedCallback", audioSource.clip.length);

        }
        else
        {
            Debug.LogError("Invalid _day specified or no audio track available for that _day.");
        }
    }

    private void AudioFinishedCallback()
    {
        Debug.Log("Audio Finished! Triggering the method after audio completion.");
        // Call your method or perform any action here
        PlayAudioForDay(day);
        
        game_manager.New_Day();
    }

    public void StopAudio()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
