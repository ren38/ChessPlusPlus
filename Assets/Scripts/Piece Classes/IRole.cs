using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRole
{
    List<Tile> findValidLocations(Board board);
    void moveTrigger();
}
