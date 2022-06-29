using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    public enum state {MISS, HIT, NOT_CHECKED};

    public state eState = state.NOT_CHECKED;

    public bool hasShip;

    public Renderer tileColor;

    private void Start()
    {
        tileColor = GetComponent<Renderer>();
        tileColor.material.SetColor("_Color", Color.blue);
    }

    private void Update()
    {
        switch (eState)
        {
            case state.MISS:
                tileColor.material.SetColor("_Color", Color.red);
                break;
            case state.HIT:
                tileColor.material.SetColor("_Color", Color.green);
                break;
            case state.NOT_CHECKED:
                tileColor.material.SetColor("_Color", Color.blue);
                break;

        }
    }

    //get if the tile has a ship
    public bool GetHasShip()
    { 
        return hasShip; 
    }
    //place a ship on the tile 
    public bool SetHasShip(bool value)
    {   
        return hasShip = value;
    }

    //get if the tile has been checked, and if it hit or miss
    public state GetState()
    {
        return eState;
    }

    //set the state of the ship
    public state SetState(string newState)
    {
        return eState = (state)System.Enum.Parse(typeof(state), newState);
    }

    //check the unchecked tile, return if it hit or miss
    public state ShootTile()
    {    
         return hasShip? eState = state.HIT : eState = state.MISS;
    }
}
