using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Board board;
    [SerializeField]
    private bool validLocation;
    private Piece occupyingPiece = null;
    private int xPosition;
    private int yPosition;

    public int getX() { return xPosition; }
    public int getY() { return yPosition; }

    private void Start()
    {
        findPosition();
        registerInBoard();
    }

    public Piece getOccupant()
    {
        return occupyingPiece;
    }

    public void setOccupant(Piece newPiece)
    {
        occupyingPiece = newPiece;
    }

    public void clearTile()
    {
        occupyingPiece = null;
    }

    public bool getValid()
    {
        return validLocation;
    }

    private void findPosition()
    {
        xPosition = Mathf.RoundToInt(this.transform.position.x - 0.5f);
        yPosition = Mathf.RoundToInt(this.transform.position.z - 0.5f);
    }

    private void registerInBoard()
    {
        board.setTile(this, xPosition, yPosition);
    }
}
