using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChooser : MonoBehaviour
{
    public Image willieImage, billyImage;
    public Animator willieAnimator, billyAnimator;

    public AudioSource ass;
    public List<AudioClip> choosingSFX;

    public int charIndex = 0;

    public void ChooseWillie()
    {
        charIndex = 0;
        PlayerPrefsManager.SetCharacter(charIndex);
        SetChar();
    }

    public void ChooseBillie()
    {
        charIndex = 1;
        PlayerPrefsManager.SetCharacter(charIndex);
        SetChar();
    }

    private void Start()
    {
        SetCharIndex();
        SetChar();
    }

    private void SetChar()
    {
        if (charIndex == 0)
        {
            willieImage.color = Color.white;
            billyImage.color = Color.black;

            willieAnimator.enabled = true;
            billyAnimator.enabled = false;
        }
        else
        {
            willieImage.color = Color.black;
            billyImage.color = Color.white;

            willieAnimator.enabled = false;
            billyAnimator.enabled = true;
        }

        ass.PlayOneShot(choosingSFX[charIndex]);
    }

    private void SetCharIndex()
    {
        try
        {
            charIndex = PlayerPrefsManager.GetCharacter();
        }
        catch { }
    }
}
