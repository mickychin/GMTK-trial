using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public int Health = 5;
    public Image[] HealthImage;

    public void TakeDamage()
    {
        HealthImage[Health-1].gameObject.SetActive(false);
        Health = Health - 1;
        if(Health <= 0)
        {
            SceneManager.LoadScene("Death");
        }
    }
}
