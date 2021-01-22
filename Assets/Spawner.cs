using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{


    public GameObject[] groups;

    public int next = -1;

    public GameObject nextTetro;

    public void spawnNext() {
        // Random Index

        int i = (next == -1) ? Random.Range(0, groups.Length) : next; 


        next = Random.Range(0, groups.Length);

        // Spawn Group at current Position


        GameObject currentTetro = Instantiate(groups[i],
                    transform.position, 
                    Quaternion.identity);


        
        if(nextTetro){
            Destroy(nextTetro);
        }

        DisplayTetromino display = FindObjectOfType<DisplayTetromino>();

        nextTetro = Instantiate(groups[next],
                    display.transform.position,
                    Quaternion.identity);


        nextTetro.GetComponent<Tetromino>().enabled = false;
    
    }

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        spawnNext();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
