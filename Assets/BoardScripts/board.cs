using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class board : MonoBehaviour 
{
    public Tiles[] boardValues;

    public GameManager gameManager;
    public static int DEFAULT_BOARD_SIZE = 25;
    public static int IN_PROGRESS = -1;

    int playerScore;


    public static int P1 = 1;
    public static int P2 = 2;
    public static int GLOBALPLAYERSCORE = 4;

    public board()
    {
        boardValues = new Tiles[DEFAULT_BOARD_SIZE];
        playerScore = GLOBALPLAYERSCORE;
    }

    public board(board board)
    {
        int boardLenght = board.GetBoardValues().Length;
        this.boardValues = new Tiles[boardLenght];
        Tiles[] boardValues = board.GetBoardValues();
        int n = boardValues.Length;
        for (int i = 0; i < n; i++)
        {
            this.boardValues[i] = boardValues[i];
        }
    }


    public List<Tiles> UncheckedPositions()
    {
        int size = this.boardValues.Length;
        List<Tiles> uncheckedPositions = new List<Tiles>();
        for (int i = 0;i < size; i++)
        {
            if(this.boardValues[i].GetState().ToString() == "NOT_CHECKED")
            {
                uncheckedPositions.Add(this.boardValues[i]);
            }
        }
        return uncheckedPositions;
    }

    public Tiles[] GetBoardValues()
    {
        return boardValues;
    }

    public int CheckStatus()
    {
        if (UncheckedPositions().Count > 0)
        {
            return IN_PROGRESS;
        }
        else
        {
            return 0;
        }
    }

    public Tiles GetTile(int tile)
    {
        return boardValues[tile];
    }
}
