using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class New_Board : MonoBehaviour {

    public Tile[,] TileArray = new Tile[16, 16];
    
    public int arraySize = 16;

    public Transform tile;
    public GameObject board;

    public float tileSize;

	// Use this for initialization
	void Start () {

        board = GameObject.FindWithTag("Board");

        var newTile = Instantiate(tile, new Vector3(0, 5, 0), Quaternion.identity);
        newTile.transform.parent = board.transform;
        
        // Puts the tile in a tile array
        TileArray[0, 0] = newTile.GetComponent<Tile>();
        
        // Gets the size of the tile for alignment
        tileSize = tile.GetComponent<Tile>().getSize();

        // Sets the first squares color
        newTile.GetComponent<Renderer>().material.SetColor("_Color", Color.black);



        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {

                // Skips the first square
                if (i == 0 && j == 0)
                {
                    j++;
                }

                newTile = Instantiate(tile, new Vector3((j * tileSize), 5, (i * tileSize)), Quaternion.identity);
                newTile.transform.parent = board.transform;
                if (j % 2 == i%2)
                {
                    newTile.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
                }
                else
                {
                    newTile.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                }

                TileArray[i,j] = newTile.GetComponent<Tile>();

            }
        }

        //Testing TileArray
        Tile x;
        x = TileArray[5, 5].GetComponent<Tile>();
        x.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);



    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnDestroy()
    {
        Destroy(tile);
    }

   
}
