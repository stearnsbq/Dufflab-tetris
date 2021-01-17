using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    public int score;


    public Text scoreText;


    // Start is called before the first frame update
    void Start()
    {
        this.score = 0;
        this.scoreText.text = "0";
    }


    public void updateScore(int score){
        this.score += score;
        this.scoreText.text = this.score.ToString("#,##0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
