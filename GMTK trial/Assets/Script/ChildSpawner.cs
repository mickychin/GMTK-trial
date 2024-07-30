using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSpawner : MonoBehaviour
{
    public GameObject child;
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
        yield return new WaitForSeconds(5f);
        StartCoroutine(SpawnChild());
    }
}
