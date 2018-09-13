using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggablePiece : Piece
{
    public GameObject ValidLocationIndicator;

    private int layerMask = (1 << 8);
    private bool moveReady = false;
    private GameObject ghost;
    private Color ghostColor;
    private Color invalidGhostColor;
    [SerializeField]
    private Material transparentMaterial;
    private Tile candidateLocation;
    private IRole[] roles;
    private List<Tile> validMoveChoiceList;
    private List<GameObject> locationIndicators;

    private void Start()
    {
        ghostColor = gameObject.GetComponent<Renderer>().material.color;
        ghostColor.a = 0.5f;
        invalidGhostColor = new Color(1 - ghostColor.r, 1 - ghostColor.g, 1 - ghostColor.b, 0.5f);
        validMoveChoiceList = new List<Tile>();
        locationIndicators = new List<GameObject>();
            //get reference to role
        roles = this.GetComponents<IRole>();
    }
    

    public void OnMouseDown()
    {

        if(getTeam().getTurn() == true)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    moveReady = true;
                    ghost = Instantiate(this.gameObject, this.transform.position, this.transform.rotation);
                    Destroy(ghost.GetComponent<DraggablePiece>());
                    ghost.GetComponent<Renderer>().material = transparentMaterial;
                }
            }

            //get list of locations that are available to move to
            foreach (IRole role in roles)
            {
                //Debug.Log("Getting new valid move list");
                validMoveChoiceList.AddRange(role.findValidLocations(board));
                //create objects to show the player where these objects are
            }
            foreach (Tile tile in validMoveChoiceList)
            {
                locationIndicators.Add(Instantiate(ValidLocationIndicator, tile.transform));
            }
        }
    }

    private void Update()
    {
        ensureSettings();

        if (moveReady)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, layerMask))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    return;
                }
                else if (hit.collider != null)
                {
                    float x = Mathf.RoundToInt(hit.point.x - 0.5f) + 0.5f;
                    float z = Mathf.RoundToInt(hit.point.z - 0.5f) + 0.5f;
                    float y = 0.3f;

                    int candidateX = Mathf.Clamp(Mathf.RoundToInt(x - 0.5f), 0, board.getSizeX() - 1);
                    int candidateY = Mathf.Clamp(Mathf.RoundToInt(z - 0.5f), 0, board.getSizeY() - 1);
                    candidateLocation = board.getTile(candidateX, candidateY);
                    if (candidateLocation != null)
                    {
                        ghost.transform.position = new Vector3(candidateLocation.transform.position.x, y, candidateLocation.transform.position.z);
                    }
                }
            }
        }
        if (candidateLocation != null) 
        {
            if (candidateLocation.getValid() && validMoveChoiceList.Contains(candidateLocation)) // .Contains might not be reading the candidateLocation correctly.
            {
                ghost.GetComponent<Renderer>().material.color = ghostColor;
            }
            else
            {
                ghost.GetComponent<Renderer>().material.color = invalidGhostColor;
            }
        }
    }

    public void OnMouseUp()
    {
        if (getTeam().getTurn() == true)
        {
            moveReady = false;
            //destroy all valid move location indicators
            foreach (GameObject locator in locationIndicators)
            {
                Destroy(locator);
            }
            //check that the target location is valid
            if (candidateLocation != null && candidateLocation.getValid() && validMoveChoiceList.Contains(candidateLocation))
            {
                if (candidateLocation.getOccupant() != null)
                {
                    Piece victim = candidateLocation.getOccupant().gameObject.GetComponent<Piece>();
                    victim.deregister();
                    Destroy(victim.gameObject);
                }
                this.transform.position = ghost.transform.position;
                setNewLocation(candidateLocation);
                foreach (IRole role in roles)
                {
                    role.moveTrigger();
                }
                getTeam().turnTaken();
            }
            validMoveChoiceList = new List<Tile>();
            Destroy(ghost);
            candidateLocation = null;
        }
    }
}
