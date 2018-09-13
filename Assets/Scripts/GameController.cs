using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private List<TeamClass> Teams;
    [SerializeField]
    private int turn;
    [SerializeField]
    private int round;

    private void Start()
    {
        foreach(TeamClass team in Teams)
        {
            team.setController(this);
        }
        turn = 0;
        giveControl();
    }

    public void turnTaken()
    {
        turn++;
        if(turn == Teams.Count)
        {
            turn = 0;
            round++;
        }
        giveControl();
    }

    public void giveControl()
    {
        if (Teams[turn].getActive() == true)
        {
            Teams[turn].takeTurn();
        }
        else
        {
            turnTaken();
        }
    }

    public TeamClass whoseTurnIsItAnyway()
    {
        return Teams[turn];
    }

}
