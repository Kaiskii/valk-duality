using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Typewriter : MonoBehaviour
{
    [SerializeField]
    string textBlipEffectName = "TextBlip";
    public float characterSpeed = 0.125f;
    private bool isPlaying;
    public bool Play = false;
    bool Interrupt = false;

    Text txt;
    public string story;

    void Start()
    {
        txt = GetComponent<Text>();
        story = txt.text;
        txt.text = "";
    }

    private void Update()
    {
       if(Play)
        {
            Play = false;
            StartCoroutine(PlayText());
        }
    }

    private IEnumerator PlayText()
    {
        if (!isPlaying)
        {
            txt.text = "";
            isPlaying = true;
            foreach (char c in story)
            {
                if (SoundManager.Instance != null) {
                    SoundManager.Instance.Play(textBlipEffectName);
                }

                if (!Interrupt)
                {
                    txt.text += c;
                    yield return new WaitForSeconds(characterSpeed);
                }
                else
                {
                    txt.text = "";
                    break;
                }
            }
            Interrupt = false;
            isPlaying = false;
        }
    }

    public void InterruptPlaying()
    {
        if(isPlaying)
            Interrupt = true;
    }
}
