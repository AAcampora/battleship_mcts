using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class MCTSEnemy
{
    static int WIN_SCORE = 10;
    int level;
    int opponent;

    public board FindNextMove(board board, int playerno)
    {
        //terminal state for our alghorithim, we use milliseconds to
        //set this
        int curCycle = 0;
        int endCylce = 1;

        //setup of the tree 
        opponent = 3 - playerno;
        Tree tree = new Tree();

        Node rootNode = tree.getRoot();
        rootNode.GetState().setBoard(board);
        rootNode.GetState().setPlayerNo(opponent);

        while (curCycle <= endCylce)
        {
            //phase 1: select node
            Node promisingNode = SelectPromisingNode(rootNode);
            //phase 2: expand node
            if (promisingNode.GetState().getBoard().CheckStatus() == board.IN_PROGRESS)
            {
                expandNode(promisingNode);
            }
            //Phase 3: Simulation
            Node nodeToExplore = promisingNode;
            if(promisingNode.GetChildArray().Count() > 0)
            {
                nodeToExplore = promisingNode.GetRandomChildNode();
            }
            int playResult = SimulateRandomPlayout(nodeToExplore);

            //Phase 4 - Update
            BackPropagation(nodeToExplore, playResult);
            curCycle++;
        }

            Node WinnerNode = rootNode.GetChildWithMaxScore();
        tree.setRoot(WinnerNode);
        return WinnerNode.GetState().getBoard();
    }

    private Node SelectPromisingNode(Node rootNode)
    {
        Node node = rootNode;
        while(node.GetChildArray().Count() !=0)
        {
            node = UCT.FindBestNodeWithUTC(node);
        }
       return node;
    }

    private void expandNode(Node node)
    {
        List<State> possibleStates = node.GetState().GetAllPossibleStates();
        foreach (var state in possibleStates)
        {
            Node newNode = new Node(state);
            newNode.SetParent(node);
            newNode.GetState().setPlayerNo(node.GetState().getOpponent());
            node.GetChildArray().Add(newNode);
        }
    }

    private int SimulateRandomPlayout(Node node)
    {
        Node tempNode = new Node(node);
        State tempState = tempNode.GetState();
        int boardStatus = tempState.getBoard().CheckStatus();

        if (boardStatus == opponent)
        {
            tempNode.GetParent().GetState().setWinScore(int.MinValue);
            return boardStatus;
        }
        while(boardStatus == board.IN_PROGRESS)
        {
            tempState.TogglePlayer();
            tempState.RandomPick();
            boardStatus = tempState.getBoard().CheckStatus();
        }
        return boardStatus;
    }
    private void BackPropagation(Node nodeToExplore, int playerNo)
    {
        Node tempNode = nodeToExplore;
        while (tempNode != null)
        {
            tempNode.GetState().IncrementVisit();
            if(tempNode.GetState().getPlayerNo() ==playerNo)
            {
                tempNode.GetState().addScore(WIN_SCORE);
            }
            tempNode = tempNode.GetParent();
        }
    }
}
