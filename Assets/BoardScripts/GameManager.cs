using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPlacingShip;
    public int currShips;
    public int maxShips = 4;
    public int playerScore = 4;
    public int enemyScore;
    public bool isPlayerPlaying = true;

    public board playerBoard;
    public board enemyBoard;

    MCTSEnemy enemy = new MCTSEnemy();

    private void Start()
    {
        PopulateEnemyBoard();
        playerScore = 4;
    }

    public void Update()
    {

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //place your ships at the start of the game 
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButton(0) && isPlacingShip == true)
            {
                if (hit.collider.tag == "tile")
                {
                    var tile = hit.collider.GetComponent<Tiles>();
                    if (tile.transform.parent.gameObject.name == "playerBoard")
                    {
                        if (!tile.GetHasShip())
                        {
                            Debug.Log(tile.name + "ship placed");
                            tile.SetHasShip(true);
                            currShips++;
                        }
                    }
                }
            }
            //try to attack the enemy
            else if (Input.GetMouseButtonDown(0) && isPlacingShip == false)
            {
                if (hit.collider.tag == "tile")
                {
                    var tile = hit.collider.GetComponent<Tiles>();
                    if (tile.transform.parent.gameObject.name == "enemyBoard")
                    {
                        tile.ShootTile();
                        if (tile.GetState().ToString() == "HIT")
                        {
                            enemyScore--;
                        }
                        Debug.Log(tile.name + " " + tile.GetState());
                        isPlayerPlaying = false;
                    }

                }

            }

            if (currShips >= maxShips)
            {
                isPlacingShip = false;
            }

            //AI does a move and passes to player 
            if (!isPlayerPlaying)
            {
                enemy.FindNextMove(playerBoard, board.P1);
                isPlayerPlaying=true;
            }
        }
    }

    private void RandomPick()
    {
        var randInt = Random.Range(0, playerBoard.boardValues.Length);
        if (playerBoard.boardValues[randInt].GetState ().ToString() == "NOT_CHECKED")
        {
            playerBoard.boardValues[randInt].ShootTile();
        }
        else
        {
            RandomPick();
        }
    }

    private void PopulateEnemyBoard()
    {
        for (int i = 0; i < maxShips; i++)
        {
            var randInt = Random.Range(0, enemyBoard.boardValues.Length);
            if (enemyBoard.boardValues[randInt].GetHasShip() == false) 
            {
                enemyBoard.boardValues[randInt].SetHasShip(true);
            }
            else
            {
                i--;
            }
        }
    }
}
