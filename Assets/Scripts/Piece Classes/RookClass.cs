using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookClass : MonoBehaviour, IRole
{
    private TeamClass thisTeam;

    private void Start()
    {
        thisTeam = this.GetComponent<Piece>().getTeam();
    }

    public List<Tile> findValidLocations(Board board)
    {
        List<Tile> validLocationList = new List<Tile>();
        Tile currentLocation = this.GetComponent<Piece>().getLocation();

        findDirection(validLocationList, currentLocation, board, 1, 0);
        findDirection(validLocationList, currentLocation, board, 0, 1);
        findDirection(validLocationList, currentLocation, board, -1, 0);
        findDirection(validLocationList, currentLocation, board, 0, -1);
        /*
        foreach(Tile tile in validLocationList)
        {
            Debug.Log(tile.getX() + " " + tile.getY());
        }
        */
        return validLocationList;
    }

    private void findDirection(List<Tile> validLocationList, Tile location, Board board, int x, int y)
    {
        Tile next = board.getTile(location.getX() + x, location.getY() + y);
        if (next != null && next.getValid())
        {
            Debug.Log("Looking at tile x:" + next.getX() + " y:" + next.getY() + ".");
            if (next.getOccupant() == null)
            {
                findDirection(validLocationList, next, board, x, y);
                validLocationList.Add(next);
            }
            else if (next.getOccupant().getTeam() != thisTeam)
            {
                validLocationList.Add(next);
            }
        }
    }

    public void moveTrigger()
    {
        //Nothing here... for now.
    }
}