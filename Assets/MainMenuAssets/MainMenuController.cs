using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{


    public Button button;




    public void onStartButtonClick(){

        Debug.Log("you clicked the start button");

        SceneManager.LoadScene("Loading");

    }


    // Start is called before the first frame update
    void Start()
    {

        this.button.onClick.AddListener(onStartButtonClick);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
