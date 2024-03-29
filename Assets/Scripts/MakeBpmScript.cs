/*
UniBpmAnalyzer
Copyright (c) 2016 WestHillApps (Hironari Nishioka)
This software is released under the MIT License.
http://opensource.org/licenses/mit-license.php
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MakeBpmScript : MonoBehaviour
{
    public float span = 1f;
    public float span_time = 0f;
    private float currentTime = 0f;
    public float measure_time = 10f;
    int original_bpm = 0;
    int make_bpm = 0;
    int tap_count = 0;
    float changedPitch;
    public Text Bpm_text = null;
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    int startingPitch = 1;
    private AudioSource audioSource;
    bool flag = false;
        
        
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip1;
        audioSource.pitch = startingPitch;
        audioSource.loop = true;
        audioSource.Play();
        original_bpm = UniBpmAnalyzer.AnalyzeBpm(audioClip1)/2;
        Bpm_text.text = original_bpm.ToString();
        if (original_bpm < 0)
        {
            Debug.LogError("AudioClip is null.");
            return;
        }
        
    }
    public void onClickTap()
    {
        tap_count++;
    }
    public void onClickStart()
    {
        flag = true;
        tap_count = 0;
        make_bpm = 0;
        currentTime = 0;
        span_time = 0;
    }

    public void onClickReset()
    {
        tap_count = 0;
        make_bpm = 0;
        changedPitch = 1f;
        Bpm_text.text = original_bpm.ToString();
        flag = false;
    }
    public void onClickChangeMode()
    {
        SceneManager.LoadScene("Start");
    }
    public void onClickChangeNextMode()
    {
        SceneManager.LoadScene("ExternalStorageTest");
    }



    // Update is called once per frame
    void Update()
    {
        if(flag == true){
            currentTime += Time.deltaTime;
            span_time += Time.deltaTime;
            if(currentTime <= measure_time){
                make_bpm = (int)(tap_count*60f/currentTime);
            }
            else if(currentTime > measure_time){
                make_bpm = (int)(tap_count*6);
                changedPitch = (float)make_bpm/original_bpm;
                audioSource.pitch = changedPitch;
                flag = false;
                return;
            }

            if(span_time >= span)
            {
                Bpm_text.text = make_bpm.ToString();
                span_time = 0f;
            }
        }
        //changedPitch = (float)changed_bpm/original_bpm;
        else{
            audioSource.pitch = changedPitch;
        }
    }

}

