using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    // Use this for initialization
    public Tile north = null, south = null, east = null, west = null;

    //START/END
    public bool END = false;    //end tile
    public bool START = false;  //start tile

    //DIRECTION SWITCHES
    public bool DIRSWITCH = false;  //if true, changes the direction the player is facing
    public Player.Direction newDir;  //direction player is changed to face   

    //CONTROL SCHEME SWITCHES
    public bool CONTROLSWITCH = false;  //If true, changes control type
    public bool switchRight = false;    //If control switch, true = turn on right - only, false turns on no-right

    //KEYS
    public bool KEYTILE = false;       //If there is a key on the tile this will = true
    public GameObject keyModel;       //makes removing keymodel easy

    //FANS
    public bool FANTILE = false;    //If this is true the tile is a fan tile
    public bool FANON = false;      //If this is true the fan will push you
    public Tile Destination;       //the tile where the fan will place you
    public bool FANSWITCH;


    //ONETIMETERRAIN
    public bool ONETIMETERRAIN;
    public GameObject tile;

   //NEXT ADDITION


    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void hideKey()
    {
        keyModel.SetActive(false);
    }

    public void TileDestroy()
    {
        if (north != null)
        {
            north.south = null;
        }

        if (east != null)
        {
            east.west = null;
        }

        if (south != null)
        {
            south.north = null;
        }

        if (west != null)
        {
            west.east = null;
        }

        tile.SetActive(false);
    }
}
