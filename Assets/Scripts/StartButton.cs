using System.Collections;
using System.Collections.Generic;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public float span = 1f;
    private float currentTime = 0f;
    private float total_time = 0f;
    float time;
    //Vector3 dire = Vector3;
    bool flag;
    bool first_data = true;
    public float acceleration = 0f;
    public float ac_x;
    public float ac_y;
    public float ac_z;
    public Text Ac = null;
    public Text Measurement = null;
    public Text Footstep_text = null;
    public Text Bpm_text = null;
    public Text Time_text = null;
    public Text Debug_text = null;
    public Text Debug_textmax = null;
    public Text Debug_textmin = null;
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    private AudioSource audioSource;
    public int startingPitch = 1;
    public float changedPitch = 0f;
    float max_acc;
    float min_acc;
    float acc_data;
    public int TargetBpm = 100;
    public float pre_time = 4f;
    public int footstep = 0;
    public bool one_step = false;
    public float measure_time = 10f;
    int bpm = 0;
    int walking_bpm = 0;
        
        
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClip1;
        audioSource.pitch = startingPitch;
        audioSource.loop = true;
        audioSource.Play();
        bpm = UniBpmAnalyzer.AnalyzeBpm(audioClip1)/2;
        Bpm_text.text = bpm.ToString();
        if (bpm < 0)
        {
            Debug.LogError("AudioClip is null.");
            return;
        }
        
    }

    public void onClick()
    {
        currentTime = 0f;
        acceleration = 0f;
        footstep = 0;
        Footstep_text.text = footstep.ToString();
        Debug.Log("Start!");
        flag = true;
        first_data = true;
    }

    public void onClickChangeMode()
    {
        SceneManager.LoadScene("Start");
    }

    // Update is called once per frame
    void Update()
    {
        if(flag == true){ //歩数計測中
            currentTime += Time.deltaTime;
            Time_text.text = currentTime.ToString();
            ac_y = Input.acceleration.x;
            ac_y = Input.acceleration.x;
            ac_y = Input.acceleration.x;
            acceleration = Mathf.Sqrt(Mathf.Pow(ac_x, 2) + Mathf.Pow(ac_y, 2) + Mathf.Pow(ac_z, 2));
            Ac.text =  acceleration.ToString();
            if(currentTime <= measure_time){
                if(currentTime <= pre_time){ //データ取得時間中
                    Measurement.text = "1st Measurement";
                    if(first_data == true)
                    {
                        max_acc = acceleration;
                        min_acc = acceleration;
                        first_data = false;
                    }
                    else
                    {
                        if(acceleration > max_acc)
                        {
                            max_acc = acceleration;
                        }
                        if(acceleration < min_acc)
                        {
                            min_acc = acceleration;
                        }
                    }
                }
                else //計測中
                {
                    Debug_textmax.text = max_acc.ToString();
                    Debug_textmin.text = min_acc.ToString();
                    Measurement.text = "2nd Measurement";
                    if(Mathf.Abs(max_acc - min_acc) <= 0.5)
                    {
                        flag = true;
                        Measurement.text = "Measurement failed.";
                        return;
                    }
                    if(acceleration >= (max_acc - 0.5))
                    {
                        one_step = true;
                        Debug_text.text = "up_count";
                        return;
                    }
                    if(one_step == true)
                    {
                        if(acceleration <= (min_acc + 0.3))
                        {
                            one_step = false;
                            footstep++;
                            Debug_text.text = "down_count";
                            Footstep_text.text = footstep.ToString();
                            return;
                        }
                    }
                }

            }    
            else if(currentTime > measure_time){
                walking_bpm = footstep * 10;
                changedPitch = (float)walking_bpm/bpm;
                Time_text.text = changedPitch.ToString();
                Measurement.text = walking_bpm.ToString();
                audioSource.pitch = changedPitch;
                flag = false;
                first_data = true;
                Ac.text =  "Acceleration: ";
                return;
            }
            

            /*
            if(currentTime >= span){
                Debug.LogFormat ("{0}秒経過", total_time);
                currentTime = 0f;
                total_time ++;
                if(total_time == measure_time){
                    flag = false;
                    acc_data = max_acc - min_acc;
                    Ac.text =  "Acceleration:" + acc_data.ToString();
                    /*
                    if(acc_data < 1.5f)
                    {
                        changedPitch = 0.8909f;
                    }
                    else if(acc_data >= 1.5f && acc_data <= 3.0f)
                    {
                        changedPitch = 1.0f;
                    }
                    else if(acc_data > 3.0f)
                    {
                        changedPitch = 1.12246f;
                    }
                    audioSource.pitch = changedPitch;
    
                    total_time = 0f;
                    acceleration = 0f;
                }
                */

        }
        else
        {
            Footstep_text.text = footstep.ToString();
            //Measurement.text = " ";
        }

    }
}
