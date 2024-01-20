using System.Collections;
using System.IO;
using UnityEngine;

public class AudioLoadTest : MonoBehaviour  //※クラス名は任意
{
    public AudioSource audioSource;  //インスペクタで AudioSource をセット
    public string path;  //※ファイルは任意

    //外部からの呼び出し用メソッド
    public void LoadAudio(string path)
    {
        if (Path.GetExtension(path) == ".m4a")  //※"m4a"は再生できないっぽい
        {
            Debug.Log("Not supported audio format.");
            return;
        }

        StartCoroutine(LoadToAudioClipAndPlay(path));
    }

    //ファイルの読み込み（ダウンロード）と再生
    IEnumerator LoadToAudioClipAndPlay(string path)
    {
        if (audioSource == null || string.IsNullOrEmpty(path))
            yield break;

        if (!File.Exists(path))
        {
            //ここにファイルが見つからない処理
            Debug.Log("File not found.");
            yield break;
        }

        using (WWW www = new WWW("file://" + path))  //※あくまでローカルファイルとする
        {
            while (!www.isDone)
                yield return null;

            AudioClip audioClip = www.GetAudioClip(false, true);
            if (audioClip.loadState != AudioDataLoadState.Loaded)
            {
                //ここにロード失敗処理
                Debug.Log("Failed to load AudioClip.");
                yield break;
            }

            //ここにロード成功処理
            audioSource.clip = audioClip;
            audioSource.Play();
            Debug.Log("Load success : " + path);
        }
    }

    // Use this for initialization
    private void Start()
    {
        //起動時に読み込むときなど
        LoadAudio(path);   //※メインからの呼び出し例
    }
}
