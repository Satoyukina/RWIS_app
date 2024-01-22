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

public class ChangeBpm : MonoBehaviour
{
    int original_bpm = 0;
    int changed_bpm = 0;
    float changedPitch;
    public Text Bpm_text = null;
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    int startingPitch = 1;
    private AudioSource audioSource;
    public InputField inputField;
        
        
    // Start is called before the first frame update
    void Start()
    {
        inputField = inputField.GetComponent<InputField> ();
        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip1;
        //audioClip1 = AudioLoadTest.audioClip;
        audioSource.pitch = startingPitch;
        audioSource.loop = true;
        audioSource.Play();
        original_bpm = UniBpmAnalyzer.AnalyzeBpm(audioClip1)/2;
        changed_bpm = UniBpmAnalyzer.AnalyzeBpm(audioClip1)/2;
        Bpm_text.text = original_bpm.ToString();
        if (original_bpm < 0)
        {
            Debug.LogError("AudioClip is null.");
            return;
        }
        
    }
    public void onClickUp()
    {
        changed_bpm++;
        Bpm_text.text = changed_bpm.ToString();
    }
    public void onClickDown()
    {
        changed_bpm--;
        Bpm_text.text = changed_bpm.ToString();
    }
    public void onClickReset()
    {
        changed_bpm = original_bpm;
        Bpm_text.text = changed_bpm.ToString();
    }
     public void onClickChangeMode()
    {
        SceneManager.LoadScene("Start");
    }
    public void InputText()
    {
        //テキストにinputFieldの内容を反映
        Bpm_text.text = inputField.text;
        changed_bpm = int.Parse(inputField.text);
    } 



    // Update is called once per frame
    void Update()
    {
        changedPitch = (float)changed_bpm/original_bpm;
        audioSource.pitch = changedPitch;

    }

}

