using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSpawner : MonoBehaviour
{
    [Header("Spawner")]
    public GameObject child;
    public int[] NOfChildSpawn;
    public int[] Frequency;
    public int[] Timer;
    public int[] MoneyEarn;
    public float[] ChildSpeed;
    public int Wave;
    public bool StopSpawning;
    AudioSource audioSource;
    Gamemaster gamemaster;

    // Start is called before the first frame update
    void Start()
    {
        Wave = -1;
        //StartCoroutine(SpawnChild());
        gamemaster = FindObjectOfType<Gamemaster>();
        audioSource = GetComponent<AudioSource>();
        StartNextWave();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartNextWave()
    {
        audioSource.Play();
        Wave++;
        StartCoroutine(SpawnChild());
        Debug.Log(Timer[Wave]);
        gamemaster.TimeSet(Timer[Wave]);
    }

    IEnumerator SpawnChild()
    {
        //Debug.Log("HELLO");
        if (NOfChildSpawn[Wave] > 0)
        {
            GameObject newchild = Instantiate(child, transform.position, Quaternion.identity);
            newchild.GetComponent<ChildrenAI>().speed = ChildSpeed[Wave];
        }
        yield return new WaitForSeconds(Frequency[Wave]);
        if (NOfChildSpawn[Wave] > 0)
        {
            //Debug.Log(NOfChildSpawn[Wave]);
            NOfChildSpawn[Wave]--;
            StartCoroutine(SpawnChild());
        }
        else
        {
            //Debug.Log("HELLO");
            StopSpawning = true;
        }
    }
}
