using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    board board;
    int playerNo;
    int visitCount;
    double winScore;

    public State()
    {
        board = new board(board);
    }

    public State(State state)
    {
        this.board = new board(board);
        this.playerNo = state.getPlayerNo();
        this.visitCount = state.getVisitCount();
        this.winScore = state.getWinScore();
    }

    public State(board board)
    {
        this.board = new board(board);
    }

    public board getBoard()
    {
        return board;
    }

    public void setBoard(board board)
    {
        this.board = board;
    }

    public int getPlayerNo()
    {
        return playerNo;
    }

    public void setPlayerNo(int playerNo)
    {
        this.playerNo = playerNo;
    }

    public int getOpponent()
    {
        return 3 - playerNo;
    }

    public int getVisitCount()
    {
        return visitCount;
    }

    public void setVisitCount(int visitCount)
    {
        this.visitCount = visitCount;
    }

    public double getWinScore()
    {
        return winScore;
    }

    public void setWinScore(double winScore)
    {
        this.winScore = winScore;
    }
    public List<State>GetAllPossibleStates()
    {
        List<State> allPossibleStates = new List<State>();
        List<Tiles> availableTiles = this.board.UncheckedPositions();
        for (int i = 0; i < availableTiles.Count; i++)
        {
            State newState = new State(this.board);
            newState.getBoard().GetTile(i).ShootTile();
            allPossibleStates.Add(newState);
        }
        return allPossibleStates;
    }

    public void TogglePlayer()
    {
        this.playerNo = 3 - this.playerNo;
    }

    public void RandomPick()
    {
        var randInt = Random.Range(0, this.board.UncheckedPositions().Count);
        board.GetTile(randInt).ShootTile();
    }

    public void IncrementVisit()
    {
        this.visitCount++;
    }

    public void addScore(double score)
    {
        if(this.winScore != int.MinValue)
        {
            this.winScore += score;
        }
    }
}
