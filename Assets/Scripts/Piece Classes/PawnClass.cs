using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnClass : MonoBehaviour, IRole
{
    private bool firstTurn = true;
    private TeamClass thisTeam;
    private static int worth = 1;

    public List<Tile> findValidLocations(Board board)
    {
        if (thisTeam == null)
        {
            thisTeam = this.GetComponent<Piece>().getTeam();
        }
        teamDirection direction = thisTeam.getTeamDirection();


        List<Tile> validLocationList = new List<Tile>();
        Tile currentLocation = this.GetComponent<Piece>().getLocation();
        

        //Defaults forward
        Tile immediatelyAhead = board.getTile(currentLocation.getX(), (currentLocation.getY() + 1));
        Tile leftCorner = board.getTile((currentLocation.getX() - 1), (currentLocation.getY() + 1));
        Tile rightCorner = board.getTile((currentLocation.getX() + 1), (currentLocation.getY() + 1));
        Tile twoUp = board.getTile(currentLocation.getX(), (currentLocation.getY() + 2));
        
        //Overwrite the direction if the team direction says otherwise.
        if (direction == teamDirection.right)
        {
            immediatelyAhead = board.getTile(currentLocation.getX() + 1, (currentLocation.getY()));
            leftCorner = board.getTile((currentLocation.getX() + 1), (currentLocation.getY() + 1));
            rightCorner = board.getTile((currentLocation.getX() + 1), (currentLocation.getY() - 1));
            twoUp = board.getTile(currentLocation.getX() + 2, (currentLocation.getY()));
        }
        else if (direction == teamDirection.backward)
        {
            immediatelyAhead = board.getTile(currentLocation.getX(), (currentLocation.getY() - 1));
            leftCorner = board.getTile((currentLocation.getX() + 1), (currentLocation.getY() - 1));
            rightCorner = board.getTile((currentLocation.getX() - 1), (currentLocation.getY() - 1));
            twoUp = board.getTile(currentLocation.getX(), (currentLocation.getY() - 2));
        }
        else if (direction == teamDirection.left)
        {
            immediatelyAhead = board.getTile(currentLocation.getX() - 1, (currentLocation.getY()));
            leftCorner = board.getTile((currentLocation.getX() - 1), (currentLocation.getY() - 1));
            rightCorner = board.getTile((currentLocation.getX() - 1), (currentLocation.getY() + 1));
            twoUp = board.getTile(currentLocation.getX() + 2, (currentLocation.getY()));
        }


        if (immediatelyAhead != null && immediatelyAhead.getValid() && immediatelyAhead.getOccupant() == null)
        {
            validLocationList.Add(immediatelyAhead);
        }

        if(leftCorner != null)
        {
            Piece leftTarget = leftCorner.getOccupant();

            if (leftCorner.getValid() && leftTarget != null && leftTarget.getTeam() != thisTeam)
            {
                validLocationList.Add(leftCorner);
            }
        }

        if (rightCorner != null)
        {
            Piece rightTarget = rightCorner.getOccupant();

            if (rightCorner.getValid() && rightTarget != null && rightTarget.getTeam() != thisTeam)
            {
                validLocationList.Add(rightCorner);
            }
        }



        if (firstTurn && twoUp != null && twoUp.getValid() && twoUp.getOccupant() == null)
        {
            validLocationList.Add(twoUp);
        }

        //if(leftcorner.getValid() && piece.isEnemy(leftcorner.getOccupant()))

        /*
        foreach (Tile tile in validLocationList)
        {
            if (tile != null)
            {
                Debug.Log("Valid Movement Locations:\tX: " + tile.getX() + "\tY: " + tile.getY() + "\tvalid: " + tile.getValid());
            }
        }
        */
        return validLocationList;
    }

    public void moveTrigger()
    {
        firstTurn = false;
    }

    public int getWorth()
    {
        return worth;
    }
}
