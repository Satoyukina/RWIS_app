using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using System.Collections.Generic;
using System.Linq;
using System;
using FantomLib;


public class StepCountScript : MonoBehaviour
{
    // サンプリング時間
    private float samplingTime = 5f; // 5秒

    // サンプリングレート
    private float samplingRate;
    private float currentTime = 0f;
    private float measure_time = 5f;
    // サンプルデータを保存するためのリスト
    private List<float> accelerationData = new List<float>();
    public Text Measurement = null;
    public Text Footstep_text = null;
    public Text Bpm_text = null;
    float mainFrequency = 0;
    public float[] accelerationZ;

    void Start()
    {
        //StepCounterSensorController stepConter = FindObjectOfType<StepCounterSensorController>();
        SensorControllerBase stepConter = FindObjectOfType<SensorControllerBase>();
        Debug.Log(stepConter);
    }
    public void OnClickMeasure()
    {
        currentTime += Time.deltaTime;;
        Measurement.text = "Start!";

/*
        if (stepConter != null && stepConter.IsSupportedSensor) {   //サポートのチェック
            stepConter.ResetCount();        //０にする（OnstepConterSensorChanged にのみ反映）
        }
        */

    }
    public void onClickChangeMode()
    {
        SceneManager.LoadScene("Adjust");
    }

    void Update()
    {
        if(currentTime <= measure_time)
        {
            /*
            if (stepConter != null && stepConter.IsSupportedSensor) {   //サポートのチェック
                stepConter.StartListening();    //取得開始
            }
        }
        else
        {
            if (stepConter != null && stepConter.IsSupportedSensor) {   //サポートのチェック
                stepConter.StopListening();     //取得停止
            }
            */
        }
        //Footstep_text.text = ;
    }

    /*
    void ReceiveValues(string json)
    {
        if (string.IsNullOrEmpty(json))
            return;

        SensorInfo info = JsonUtility.FromJson<SensorInfo>(json); //JSONから変換
        if (info.type == (int)SensorType.stepConter) {
            //info.values（OS がリブートしてからの歩数：float 型）
            //を用いて何らかの処理など
        }
    }
    */
}


