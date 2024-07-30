using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Gamemaster : MonoBehaviour
{
    public int Time;
    public TextMeshProUGUI TimeText;
    public Button NextWaveButton;
    public GameObject DayMap;
    
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(UpdateTime());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TimeSet(int timeseted)
    {
        Debug.Log("ELLO");
        Time = timeseted;
        StartCoroutine(UpdateTime());
        NextWaveButton.gameObject.SetActive(false);
    }

    IEnumerator UpdateTime()
    {
        yield return new WaitForSeconds(1f);
        if(Time > 0)
        {
            DayMap.SetActive(false);
            Time -= 1;
            TimeText.text = "Time : " + Time.ToString();
            StartCoroutine(UpdateTime());        }
        else
        {
            //day time
            DayMap.SetActive(true);
            NextWaveButton.gameObject.SetActive(true);
            ChildrenAI[] childrens = FindObjectsOfType<ChildrenAI>();
            foreach (ChildrenAI children in childrens)
            {
                children.IsMoving = false;
                //children.GetComponent<Animator>().enabled = false;
            }
            //FindObjectOfType<Money>().Moneys += FindObjectOfType<ChildSpawner>().MoneyEarn[FindObjectOfType<ChildSpawner>().Wave];
            FindObjectOfType<Money>().Moneys += 1;
        }
    }
}
