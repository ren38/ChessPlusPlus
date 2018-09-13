using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamClass : MonoBehaviour
{

    private GameController gameController;
    [SerializeField]
    private string teamName;
    [SerializeField]
    private List<Piece> pieces;
    [SerializeField]
    private bool active;
    private bool control;
    [SerializeField]
    private teamDirection facing;
    

    public void deregisterPiece(Piece piece)
    {
        pieces.Remove(piece);
        if (pieces.Count == 0)
        {
            active = false;
        }
    }

    public void registerPiece(Piece piece)
    {
        pieces.Add(piece);
        active = true;
    }

    public void deactivateTeam()
    {
        active = false;
    }

    public bool getActive()
    {
        return active;
    }

    public void turnTaken()
    {
        control = false;
        gameController.turnTaken();
    }

    public void takeTurn()
    {
        control = true;
    }

    public bool getTurn()
    {
        return control;
    }

    public void setController(GameController c)
    {
        gameController = c;
    }

    public teamDirection getTeamDirection() { return facing; }
}
