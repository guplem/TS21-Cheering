using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DefaultSoundButton : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip; 
    [SerializeField] private TMPro.TMP_Text buttonText;

    private void Awake()
    {
        buttonText.text = audioClip != null? audioClip.name : "";
    }

    public void PlaySound()
    {
        AppManager.Instance.soundManager.PlaySound(audioClip);
    }
}
