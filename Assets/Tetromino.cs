using UnityEngine;
using System.Collections;

public class Tetromino : MonoBehaviour
{

    // Time since last gravity tick
    float lastFall = 0;
    int cells = 0;


    // Use this for initialization
    void Start()
    {
        // Default position not valid? Then it's game over
        if (!isValidGridPos())
        {
            //Debug.Log("GAME OVER");
            //Destroy(gameObject);
            gameOver();
        }
    }

    // Update is called once per frame 
    void Update()
    {

            // Move Left
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                // Modify position
                transform.position += new Vector3(-1, 0, 0);

                // See if valid
                if (isValidGridPos())
                    // It's valid. Update grid.
                    updateGrid();
                else
                    // It's not valid. revert.
                    transform.position += new Vector3(1, 0, 0);
            }

            // Move Right
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // Modify position
                transform.position += new Vector3(1, 0, 0);

                // See if valid
                if (isValidGridPos())
                    // It's valid. Update grid.
                    updateGrid();
                else
                    // It's not valid. revert.
                    transform.position += new Vector3(-1, 0, 0);
            }

            // Rotate
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.Rotate(0, 0, -90);

                // See if valid
                if (isValidGridPos())
                    // It's valid. Update grid.
                    updateGrid();
                else
                    // It's not valid. revert.
                    transform.Rotate(0, 0, 90);
            }

            // Move Downwards and Fall
            else if (Input.GetKeyDown(KeyCode.DownArrow) ||
                     Time.time - lastFall >= 1)
            {
                // Modify position
                transform.position += new Vector3(0, -1, 0);
                cells++;

                // See if valid
                if (isValidGridPos())
                {
                    // It's valid. Update grid.
                    updateGrid();
                }
                else
                {
                    // It's not valid. revert.
                    transform.position += new Vector3(0, 1, 0);

                    // Clear filled horizontal lines
                    Playfield.deleteFullRows();



                    FindObjectOfType<GameController>().updateScore(cells);


                    // Spawn next Group
                    FindObjectOfType<Spawner>().spawnNext();

                    // Disable script
                    enabled = false;
                }

                lastFall = Time.time;
            }
            // Slam Downwards
            else if (Input.GetKeyDown(KeyCode.Space) ||
                     Time.time - lastFall >= 1)
            {


                while (isValidGridPos())
                {
                    // Modify position
                    transform.position += new Vector3(0, -1, 0);
                    // See if valid
                    cells++;
                    if (isValidGridPos())
                    {
                        // It's valid. Update grid.
                        updateGrid();
                    }
                }

                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                Playfield.deleteFullRows();


                FindObjectOfType<GameController>().updateScore(2 * cells);
                // Spawn next Group
                FindObjectOfType<Spawner>().spawnNext();

                // Disable script
                enabled = false;

                lastFall = Time.time;

            }



    }
    //------------------------------------------------------------------------------
    bool isValidGridPos(){
      try{
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);

            // Not inside Border?
            if (!Playfield.insideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?
            if (Playfield.grid[(int)v.x, (int)v.y] != null &&
                Playfield.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
      }
      catch(System.IndexOutOfRangeException){
        Debug.Log("IndexOutOfRangeException Caught");
        //return false;
        return true;
      }
    }
    //------------------------------------------------------------------------------
    public static void gameOver(){
      Debug.Log("GAME OVER");
      //Destroy(gameObject);
      Time.timeScale = 0;
      Application.Quit();
      Debug.Break();
    }
    
    //------------------------------------------------------------------------------
    void updateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Playfield.h; ++y)
            for (int x = 0; x < Playfield.w; ++x)
                if (Playfield.grid[x, y] != null)
                    if (Playfield.grid[x, y].parent == transform)
                        Playfield.grid[x, y] = null;

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.roundVec2(child.position);
            Playfield.grid[(int)v.x, (int)v.y] = child;
        }
    }
}
