using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveClass : MonoBehaviour
{
    private Piece piece = null;
    private Tile startingLocation = null;
    private Tile newLocation = null;

    public MoveClass(Piece piece, Tile startingLocation, Tile newLocation)
    {
        this.piece = piece;
        this.startingLocation = startingLocation;
        this.newLocation = newLocation;
    }

    public Piece getPiece()
    {
        return piece;
    }

    public Tile getStartingLocation()
    {
        return startingLocation;
    }
    
    public Tile getNewLocation()
    {
        return newLocation;
    }
}
