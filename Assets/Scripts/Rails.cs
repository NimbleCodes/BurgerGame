﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Rails : MonoBehaviour
{
    //reference to TriggerManager and SpawnerManager
    TriggerManager t;
    SpawnerManager s;

    public Vector3Int cellDimen;
    Tilemap tileMap;
    private void Awake()
    {
        tileMap = gameObject.GetComponent<Tilemap>();
        Vector3 bottom_left_cell_center = tileMap.layoutGrid.GetCellCenterWorld(new Vector3Int(0,0,0));
        Vector3 top_right_cell_center = tileMap.layoutGrid.GetCellCenterWorld(new Vector3Int(cellDimen.x, cellDimen.y, 0));
        float tilesizex = tileMap.layoutGrid.cellSize.x;
        float tilesizey = tileMap.layoutGrid.cellSize.y;

        Vector3 bottom_left = new Vector3(bottom_left_cell_center.x - tilesizex / 2, bottom_left_cell_center.y - tilesizey / 2);
        Vector3 top_right = new Vector3(top_right_cell_center.x + tilesizex / 2, top_right_cell_center.y + tilesizey / 2);

        t = FindObjectOfType<TriggerManager>();
        s = FindObjectOfType<SpawnerManager>();

        t.bottomLeft = bottom_left;
        t.topRight = top_right;

        s.bottomLeft = bottom_left;
        s.topRight = top_right;
    }
}
