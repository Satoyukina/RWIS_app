using System.Collections;
using System.IO;
using UnityEngine;

public class AudioLoadTest : MonoBehaviour  //���N���X���͔C��
{
    public AudioSource audioSource;  //�C���X�y�N�^�� AudioSource ���Z�b�g
    public string path;  //���t�@�C���͔C��

    //�O������̌Ăяo���p���\�b�h
    public void LoadAudio(string path)
    {
        if (Path.GetExtension(path) == ".m4a")  //��"m4a"�͍Đ��ł��Ȃ����ۂ�
        {
            Debug.Log("Not supported audio format.");
            return;
        }

        StartCoroutine(LoadToAudioClipAndPlay(path));
    }

    //�t�@�C���̓ǂݍ��݁i�_�E�����[�h�j�ƍĐ�
    IEnumerator LoadToAudioClipAndPlay(string path)
    {
        if (audioSource == null || string.IsNullOrEmpty(path))
            yield break;

        if (!File.Exists(path))
        {
            //�����Ƀt�@�C����������Ȃ�����
            Debug.Log("File not found.");
            yield break;
        }

        using (WWW www = new WWW("file://" + path))  //�������܂Ń��[�J���t�@�C���Ƃ���
        {
            while (!www.isDone)
                yield return null;

            AudioClip audioClip = www.GetAudioClip(false, true);
            if (audioClip.loadState != AudioDataLoadState.Loaded)
            {
                //�����Ƀ��[�h���s����
                Debug.Log("Failed to load AudioClip.");
                yield break;
            }

            //�����Ƀ��[�h��������
            audioSource.clip = audioClip;
            audioSource.Play();
            Debug.Log("Load success : " + path);
        }
    }

    // Use this for initialization
    private void Start()
    {
        //�N�����ɓǂݍ��ނƂ��Ȃ�
        LoadAudio(path);   //�����C������̌Ăяo����
    }
}
