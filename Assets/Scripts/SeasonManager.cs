using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SeasonManager : MonoBehaviour
{
    public static int currentSeasonIndex = 0;
    public static string[] seasonArray = {"Winter", "Spring", "Summer", "Autumn"};
    float seasonTimer = 10.0f;
    public GameObject tilemapObject;
    Tilemap tilemap;

    Object[] winterTileArray;
    Object[] springTileArray;
    Object[] summerTileArray;
    Object[] autumnTileArray;

    void Start()
    {
        tilemap = tilemapObject.GetComponent<Tilemap>();
        winterTileArray = Resources.LoadAll("Winter Tiles", typeof(TileBase));
        springTileArray = Resources.LoadAll("Spring Tiles", typeof(TileBase));
        summerTileArray = Resources.LoadAll("Summer Tiles", typeof(TileBase));
        autumnTileArray = Resources.LoadAll("Autumn Tiles", typeof(TileBase));
        ChangeSeasonTiles();
    }

    void Update()
    {
         if (seasonTimer >= 0) {
            seasonTimer -= Time.deltaTime;
            if (seasonTimer < 0) {
                if (currentSeasonIndex == 3) {
                    currentSeasonIndex = 0;
                } else {
                    currentSeasonIndex = currentSeasonIndex + 1;
                }
                seasonTimer = 10.0f;
                ChangeSeasonTiles();
            }
        }
    }

    void ChangeSeasonTiles()
    {
        if (seasonArray[currentSeasonIndex] == "Winter") {
            foreach (TileBase tile in winterTileArray)
            {
                tilemap.SwapTile(System.Array.Find(springTileArray, t => t.name == tile.name) as TileBase, tile);
                tilemap.SwapTile(System.Array.Find(summerTileArray, t => t.name == tile.name) as TileBase, tile);
                tilemap.SwapTile(System.Array.Find(autumnTileArray, t => t.name == tile.name) as TileBase, tile);
            }
        } else if (seasonArray[currentSeasonIndex] == "Spring") {
            foreach (TileBase tile in springTileArray)
            {
                tilemap.SwapTile(System.Array.Find(summerTileArray, t => t.name == tile.name) as TileBase, tile);
                tilemap.SwapTile(System.Array.Find(autumnTileArray, t => t.name == tile.name) as TileBase, tile);
                tilemap.SwapTile(System.Array.Find(winterTileArray, t => t.name == tile.name) as TileBase, tile);
            }
        } else if (seasonArray[currentSeasonIndex] == "Summer") {
            foreach (TileBase tile in summerTileArray)
            {
                tilemap.SwapTile(System.Array.Find(autumnTileArray, t => t.name == tile.name) as TileBase, tile);
                tilemap.SwapTile(System.Array.Find(winterTileArray, t => t.name == tile.name) as TileBase, tile);
                tilemap.SwapTile(System.Array.Find(springTileArray, t => t.name == tile.name) as TileBase, tile);
            }
        } else if (seasonArray[currentSeasonIndex] == "Autumn") {
            foreach (TileBase tile in autumnTileArray)
            {
                tilemap.SwapTile(System.Array.Find(winterTileArray, t => t.name == tile.name) as TileBase, tile);
                tilemap.SwapTile(System.Array.Find(springTileArray, t => t.name == tile.name) as TileBase, tile);
                tilemap.SwapTile(System.Array.Find(summerTileArray, t => t.name == tile.name) as TileBase, tile);
            }
        }
    }
}
