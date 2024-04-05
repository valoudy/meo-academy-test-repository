using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [Header("����������")]
    public AudioSource Background;

    [Header("�����")]
    public AudioClip BthClick;
    public AudioClip BlockCreate;
    public AudioClip Money;
    public AudioClip Compile;
    public AudioClip End;
    public AudioClip Timer;
    public AudioClip BlockTransform;

    [Header("������ �����")]
    public GameObject PrefSound;

    public static SoundController init;
    [HideInInspector]
    /// <summary>
    /// ����
    /// </summary>
    public bool onSound;
    [HideInInspector]

    /// <summary>
    /// ������
    /// </summary>
    public bool onMusic;

    public SoundController() {
        init = this;
    }
    void Awake()
    {
        onSound = Convert.ToBoolean(PlayerPrefs.GetInt("Sound", 1));
        onMusic = Convert.ToBoolean(PlayerPrefs.GetInt("Music", 1));
        Background.enabled = onMusic;
    }
    public bool OnSound() {
        onSound = !onSound;
        PlayerPrefs.SetInt("Sound", Convert.ToInt32(onSound));

        return onSound;
    }
    public bool OnMusic()
    {
        onMusic = !onMusic;
        Background.enabled = onMusic;

        PlayerPrefs.SetInt("Music", Convert.ToInt32(onMusic));

        return onMusic;
    }
    public void PlaySound(string name) {
        if (!onSound)
            return;

        // ������ ������ � AudioSource
        GameObject objectSound = Instantiate(PrefSound, this.transform);
        // �������� ������ �� ���������� AudioSource
        AudioSource sourceSound = objectSound.GetComponent<AudioSource>();
        // � ����������� �� �������� ����������� ��� ����, ������� ����������
        if (name == "bth")
            sourceSound.clip = BthClick;
        else if(name == "create")
            sourceSound.clip = BlockCreate;
        else if (name == "money")
            sourceSound.clip = Money;
        else if (name == "compile")
            sourceSound.clip = Compile;
        else if (name == "end")
            sourceSound.clip = End;
        else if (name == "timer")
            sourceSound.clip = Timer;
        else if (name == "transform")
            sourceSound.clip = BlockTransform;

        // ������������� ����
        sourceSound.Play();
        // ������� ��������� ������
        Destroy(objectSound, 5f);
    }
}
