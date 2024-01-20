using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using System.Collections.Generic;
using System.Linq;
using System;


public class MeasureScript : MonoBehaviour
{
    // サンプリング時間
    private float samplingTime = 5f; // 5秒

    // サンプリングレート
    private float samplingRate;

    // サンプルデータを保存するためのリスト
    private List<float> accelerationData = new List<float>();
    public Text Measurement = null;
    public Text Footstep_text = null;
    public Text Bpm_text = null;
    float mainFrequency = 0;
    public float[] accelerationZ;

    public void OnClickMeasure()
    {
        Array.Clear(accelerationZ, 0, accelerationZ.Length);;
        accelerationData.Clear();
        mainFrequency = 0;
        Measurement.text = "Start!";
        // サンプリングレートの計算
        samplingRate = 1f / Time.fixedDeltaTime;
        // サンプリング開始
        InvokeRepeating("RecordAccelerationData", 0f, Time.fixedDeltaTime);
        // サンプリング時間後に解析実行
        Invoke("AnalyzeAccelerationData", samplingTime);
    }
    public void onClickChangeMode()
    {
        SceneManager.LoadScene("Adjust");
    }

    void RecordAccelerationData()
    {
        // 加速度データの取得と保存
        float acceleration = Input.acceleration.x;
        accelerationData.Add(acceleration);
    }

    void AnalyzeAccelerationData()
    {
        Measurement.text = "Stop!";
        
        // x軸方向の加速度データを取得
        float[] accelerationZ = accelerationData.ToArray();

        // ピーク検出
        List<int> peakIndices = FindPeaks(accelerationZ);

        // 周波数の計算
        mainFrequency = CalculateMainFrequency(peakIndices, samplingRate);

        
        Footstep_text.text = "Frequency: " + mainFrequency*60 + " Hz";
        Bpm_text.text = "Sampling Rate: " + samplingRate + " Hz";
        //Debug.Log("主要な周波数: " + mainFrequency + " Hz");
        //Debug.Log("サンプリングレート: " + samplingRate + " Hz");
    }

    List<int> FindPeaks(float[] data)
    {
        List<int> peakIndices = new List<int>();

        for (int i = 1; i < data.Length - 1; i++)
        {
            if (data[i] > data[i - 1] && data[i] > data[i + 1])
            {
                peakIndices.Add(i);
            }
        }

        return peakIndices;
    }

    float CalculateMainFrequency(List<int> peakIndices, float samplingRate)
    {
        if (peakIndices.Count < 2)
        {
            Debug.LogError("ピークが不足しているため、周波数を計算できません。");
            return 0f;
        }

        // 最初と最後のピークの差分を取り、平均を計算
        float averagePeakDistance = (peakIndices.Last() - peakIndices.First()) / (float)(peakIndices.Count - 1);

        // サンプリングレートを考慮して周波数を計算
        float mainFrequency = samplingRate / averagePeakDistance;

        return mainFrequency;
    }
}


