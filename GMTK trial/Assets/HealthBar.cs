using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Mathematics;

public class HealthBar : MonoBehaviour
{
    [Header("HP")]
    public int Health = 5;
    public Image[] HealthImage;

    [Header("LOSE")]
    public float TimeToLoadScreen;
    public GameObject LoseScreens;
    public Image LoseImage;
    public Image DarkSoulPanel;
    public Text DarkSoulText;

    private void Start()
    {
        
    }

    public void TakeDamage()
    {
        HealthImage[Health-1].gameObject.SetActive(false);
        Health = Health - 1;
        if(Health <= 0)
        {
            //SceneManager.LoadScene("Death");
            LoadLose();
        }
    }

    public void LoadLose()
    {
        Debug.Log("LOAD LOSE");
        LoseScreens.SetActive(true);
        StartCoroutine(FadeInLoseScreen()); 
        //SetOpacity(1f, LoseImage);
    }

    IEnumerator FadeInLoseScreen()
    {
        if(LoseImage.color.a == 0)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Debug.Log("FADE IN LOSE SCREEN");
        SetOpacity(LoseImage.color.a + (TimeToLoadScreen / 100), LoseImage);
        SetOpacity(DarkSoulPanel.color.a + (TimeToLoadScreen / 100 * 175 / 255), DarkSoulPanel);
        SetOpacityText(DarkSoulText.color.a + (TimeToLoadScreen / 100), DarkSoulText);
        yield return new WaitForSeconds(0.01f);
        if (LoseImage.color.a < 1)
        {
            StartCoroutine(FadeInLoseScreen());
        }
    }

    public void SetOpacity(float alpha, Image image)
    {
        alpha = Mathf.Clamp01(alpha);

        // Get the current color of the Image
        Color color = image.color;

        // Set the new alpha value
        color.a = alpha;

        // Apply the updated color to the Image
        image.color = color;
    }

    public void SetOpacityText(float alpha, Text text)
    {
        alpha = Mathf.Clamp01(alpha);

        // Get the current color of the Image
        Color color = text.color;

        // Set the new alpha value
        color.a = alpha;

        // Apply the updated color to the Image
        text.color = color;
    }
}
