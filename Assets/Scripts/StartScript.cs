using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public void onClicktoMeasure()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void onClickToAdjust()
    {
        SceneManager.LoadScene("Adjust");
    }

    public void onClickToMake()
    {
        SceneManager.LoadScene("MakeBpm");
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
