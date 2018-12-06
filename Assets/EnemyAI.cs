using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private TeamClass team;
    private List<Piece> peiceList;
    [SerializeField]
    private Board board;

    public void takeTurn()
    {
        StartCoroutine("Calculate");
    }

    IEnumerator Calculate()
    {
        peiceList = team.getPieceList();
        Piece pieceToBeMoved = peiceList[Random.Range(0, peiceList.Count)];
        IRole roleOfPiece = pieceToBeMoved.gameObject.GetComponent(typeof(IRole)) as IRole;
        List<Tile> validMoveList = roleOfPiece.findValidLocations(board);
        Tile newLocation = validMoveList[Random.Range(0, validMoveList.Count)];

        pieceToBeMoved.gameObject.transform.position = new Vector3(newLocation.gameObject.transform.position.x, pieceToBeMoved.gameObject.transform.position.y, newLocation.gameObject.transform.position.z);
        Piece pieceOfPiece = pieceToBeMoved.gameObject.GetComponent(typeof(Piece)) as Piece;
        pieceOfPiece.setNewLocation(newLocation);
        team.turnTaken();
        yield return null;
    }
}
