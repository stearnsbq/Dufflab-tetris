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

        int i; 

        if(next == -1){
            i = Random.Range(0, groups.Length);
        }else{
            i = next;
        }

        next = Random.Range(0, groups.Length);

        

        //------------------------------------------------------------------------------
        //If a block is in the spawners row then end the game
        
        if(Playfield.isBlockInRow(19)){
          Tetromino.gameOver();
        }
        //------------------------------------------------------------------------------
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
        
        //------------------------------------------------------------------------------
        //Set color of nextTetro and currentTetro to the same random color
        SpriteRenderer[] nextAllChildren = nextTetro.GetComponentsInChildren<SpriteRenderer>();
        SpriteRenderer[] currentAllChildren = currentTetro.GetComponentsInChildren<SpriteRenderer>();
        
        
        float red = Random.Range(0f,1f);
        float green = Random.Range(0f,1f);
        float blue = Random.Range(0f,1f);
        //float alpha = Random.Range(0.2f,2f);
        Color randColor = new Color (red,green,blue,1);
        //Debug.Log(randColor);
        foreach (SpriteRenderer child in nextAllChildren)
        { 
            child.color = randColor;
            //Debug.Log("Preview: " + child.color + "\tValue: " + randColor);
        }
        foreach (SpriteRenderer child in currentAllChildren)
        { 
            child.color = randColor;
            //Debug.Log("Current: " + child.color + "\tValue: " + randColor);
        }
        //nextTetro.GetComponentInChildren<SpriteRenderer>().color = new Color (1,0,0,1);
        //------------------------------------------------------------------------------
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
