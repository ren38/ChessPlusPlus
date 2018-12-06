using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private TeamClass team;
    private List<Piece> pieceList;
    [SerializeField]
    private Board board;

    public void takeTurn()
    {
        StartCoroutine("Calculate");
    }

    IEnumerator Calculate()
    {
        pieceList = team.getPieceList();
        int currentBestScore = 0;
        Piece bestPiece = null;
        Tile bestLocation = null;
        foreach(Piece piece in pieceList)
        { 
            if(piece != null)
            {
                IRole roleOfPiece = piece.gameObject.GetComponent(typeof(IRole)) as IRole;
                List<Tile> validMoveList = roleOfPiece.findValidLocations(board);
                foreach (Tile tile in validMoveList)
                {
                    if (tile.getOccupant() != null && tile.getOccupant().getTeam() != team)
                    {
                        IRole targetRole = tile.getOccupant().gameObject.GetComponent(typeof(IRole)) as IRole;
                        if (currentBestScore < targetRole.getWorth())
                        {
                            currentBestScore = targetRole.getWorth();
                            bestPiece = piece;
                            bestLocation = tile;
                        }
                    }

                }
            }
        }

        if(bestPiece == null || bestLocation == null)
        {
            Piece pieceToBeMoved = null;

            while (pieceToBeMoved == null)
            {
                pieceToBeMoved = pieceList[Random.Range(0, pieceList.Count)];
            }

            IRole roleOfPiece = pieceToBeMoved.gameObject.GetComponent(typeof(IRole)) as IRole;
            List<Tile> validMoveList = roleOfPiece.findValidLocations(board);

            bestLocation = validMoveList[Random.Range(0, validMoveList.Count)];
            bestPiece = pieceToBeMoved;
        }


        bestPiece.gameObject.transform.position = new Vector3(bestLocation.gameObject.transform.position.x, bestPiece.gameObject.transform.position.y, bestLocation.gameObject.transform.position.z);
        Piece pieceOfPiece = bestPiece.gameObject.GetComponent(typeof(Piece)) as Piece;
        pieceOfPiece.setNewLocation(bestLocation);
        team.turnTaken();
        bestPiece = null;
        yield return null;
    }
}
