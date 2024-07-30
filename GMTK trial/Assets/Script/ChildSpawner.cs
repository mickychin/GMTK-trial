using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSpawner : MonoBehaviour
{
    public GameObject child;
    public int[] NOfChildSpawn;
    public int[] Frequency;
    public int Wave;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnChild());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnChild()
    {
        Debug.Log("HELLO");
        Instantiate(child, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(Frequency[Wave]);
        if(NOfChildSpawn[Wave] >= 0)
        {
            StartCoroutine(SpawnChild());
        }
    }
}
