using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public int Health = 5;
    public TextMeshProUGUI HealthText;

    void Start()
    {
    }

    void Update()
    {
        HealthText.text = " : " + Health.ToString();

    }

    public void TakeDamage()
    {
        Health = Health - 1;
        if(Health <= 0)
        {
            SceneManager.LoadScene("Death");
        }
    }
}
