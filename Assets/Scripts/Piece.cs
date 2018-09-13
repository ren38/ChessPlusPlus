using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField]
    private TeamClass team;
    public Board board;

    private Tile location;
    // Use this for initialization
    private void Update()
    {
        ensureSettings();
    }

    public void ensureSettings()
    {
        if (location == null)
        {
            setBeginningPosition();
        }
        if (team == null)
        {
            setTeam();
        }
    }

    public void setBeginningPosition()
    {
        int roundedX = Mathf.RoundToInt(transform.position.x - 0.5f);
        int roundedY = Mathf.RoundToInt(transform.position.z - 0.5f);
        //Debug.Log("Piece has had its beginning position set! Team: " + team + "\troundedX: " + roundedX + "\troundedY" + roundedY);

        location = board.getTile(roundedX, roundedY);

        if (location != null)
        {
            location.setOccupant(this);
        }
        else
        {
            Debug.Log("Warning! Piece does not have an associated tile! Location: x=" + this.transform.position.x + " y=" + this.transform.position.y + " z=" + this.transform.position.z + " objectName=" + this.name);
        }
    }

    public Tile getLocation()
    {
        /*
        if (location != null)
        {
            Debug.Log("Current Location:\tX: " + location.getX() + "\tY: " + location.getY() + "\tvalid: " + location.getValid() + "\t @getLocation");
        }
        else
        {
            Debug.Log("Location is null at piece getLocation!");
        }
        */
        return location;
    }

    public TeamClass getTeam()
    {
        return team;
    }

    public void setTeam()
    {
        team = transform.parent.gameObject.GetComponent<TeamClass>();
        if(team != null)
        {
            team.registerPiece(this);
        }
    }

    public void setNewLocation(Tile newLocation)
    {
        //Debug.Log("setNewLocation called");

        location.clearTile();
        location = newLocation;
        location.setOccupant(this);
    }

    public void deregister()
    {
        team.deregisterPiece(this);
    }
}
