using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private int sizeX = 50;
    [SerializeField]
    private int sizeY = 50;
    private Tile[,] tileArray;

    private void Awake()
    {
        tileArray = new Tile[sizeX, sizeY];
    }
    public int getSizeX() { return sizeX; }
    public int getSizeY() { return sizeY; }
    public Tile getTile(int x, int y)
    {
        //checkBoard();
        /*
        if (tileArray[x,y] != null)
        {
            Debug.Log("Attempted to getTile(" + x + ", " + y + "). Success!");
        }
        else
        {
            Debug.Log("Attempted to getTile(" + x + ", " + y + "). Failure!");
        }
        */
        if(x < 0 || x > (sizeX - 1) || y < 0 || y > (sizeY - 1))
        {
            return null;
        }

        else
        {
            return tileArray[x, y];
        }
    } 
    public void setTile(Tile tile, int x, int y) { tileArray[x, y] = tile; }

    
    public void checkBoard()
    {
        foreach(Tile tile in tileArray)
        {
            if(tile != null)
            {
                Debug.Log("X: " + tile.getX() + "\tY: " + tile.getY() + "\tvalid: " + tile.getValid());
            }
            else
            {
                Debug.Log("Nothing here!");
            }
        }
    }


}
