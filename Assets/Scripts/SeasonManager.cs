using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SeasonManager : MonoBehaviour
{
    public static int currentSeasonIndex = 2;
    public static string[] seasonArray = {"Winter", "Spring", "Summer", "Autumn"};
    float seasonTimer = 10.0f;
    public GameObject colliderTilemapReference;
    public GameObject backgroundTilemapReference;
    public GameObject grassTilemapReference;
    Tilemap colliderTilemap;
    Tilemap backgroundTilemap;
    Tilemap grassTilemap;

    Object[] winterTileArray;
    Object[] springTileArray;
    Object[] summerTileArray;
    Object[] autumnTileArray;

    public static bool winterShardObtained = false;
    public static bool springShardObtained = false;
    public static bool summerShardObtained = false;
    public static bool autumnShardObtained = false;
    public static int piecesLeft = 4;

    public bool isDoneWithCutscene = true;
    public float cutsceneTimer = -1.0f;
    public int seasonChangeCount = 0;

    void Start()
    {
        colliderTilemap = colliderTilemapReference.GetComponent<Tilemap>();
        backgroundTilemap = backgroundTilemapReference.GetComponent<Tilemap>();
        grassTilemap = grassTilemapReference.GetComponent<Tilemap>();
        winterTileArray = Resources.LoadAll("Winter Tiles", typeof(TileBase));
        springTileArray = Resources.LoadAll("Spring Tiles", typeof(TileBase));
        summerTileArray = Resources.LoadAll("Summer Tiles", typeof(TileBase));
        autumnTileArray = Resources.LoadAll("Autumn Tiles", typeof(TileBase));
        ChangeSeasonTiles();
    }

    void Update()
    {
        if (!winterShardObtained || !springShardObtained || !summerShardObtained || !autumnShardObtained) {
            if (seasonTimer >= 0 && isDoneWithCutscene) {
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
            } else if (cutsceneTimer >= 0 && !isDoneWithCutscene) {
                cutsceneTimer -= Time.unscaledDeltaTime;
                if (cutsceneTimer < 0) {
                    if (currentSeasonIndex == 3) {
                        currentSeasonIndex = 0;
                    } else {
                        currentSeasonIndex = currentSeasonIndex + 1;
                    }
                    ChangeSeasonTiles();

                    seasonChangeCount += 1;
                    if (seasonChangeCount >= 3) {
                        cutsceneTimer = -1.0f;
                        isDoneWithCutscene = true;
                    } else {
                        cutsceneTimer = 1.0f;
                    }
                }
            }
        }
    }

    void ChangeSeasonTiles()
    {
        if (seasonArray[currentSeasonIndex] == "Winter") {
            foreach (TileBase tile in winterTileArray)
            {
                colliderTilemap.SwapTile(System.Array.Find(springTileArray, t => t.name == tile.name) as TileBase, tile);
                colliderTilemap.SwapTile(System.Array.Find(summerTileArray, t => t.name == tile.name) as TileBase, tile);
                colliderTilemap.SwapTile(System.Array.Find(autumnTileArray, t => t.name == tile.name) as TileBase, tile);
                backgroundTilemap.SwapTile(System.Array.Find(springTileArray, t => t.name == tile.name) as TileBase, tile);
                backgroundTilemap.SwapTile(System.Array.Find(summerTileArray, t => t.name == tile.name) as TileBase, tile);
                backgroundTilemap.SwapTile(System.Array.Find(autumnTileArray, t => t.name == tile.name) as TileBase, tile);
                grassTilemap.SwapTile(System.Array.Find(springTileArray, t => t.name == tile.name) as TileBase, tile);
                grassTilemap.SwapTile(System.Array.Find(summerTileArray, t => t.name == tile.name) as TileBase, tile);
                grassTilemap.SwapTile(System.Array.Find(autumnTileArray, t => t.name == tile.name) as TileBase, tile);
            }
        } else if (seasonArray[currentSeasonIndex] == "Spring") {
            foreach (TileBase tile in springTileArray)
            {
                colliderTilemap.SwapTile(System.Array.Find(summerTileArray, t => t.name == tile.name) as TileBase, tile);
                colliderTilemap.SwapTile(System.Array.Find(autumnTileArray, t => t.name == tile.name) as TileBase, tile);
                colliderTilemap.SwapTile(System.Array.Find(winterTileArray, t => t.name == tile.name) as TileBase, tile);
                backgroundTilemap.SwapTile(System.Array.Find(summerTileArray, t => t.name == tile.name) as TileBase, tile);
                backgroundTilemap.SwapTile(System.Array.Find(autumnTileArray, t => t.name == tile.name) as TileBase, tile);
                backgroundTilemap.SwapTile(System.Array.Find(winterTileArray, t => t.name == tile.name) as TileBase, tile);
                grassTilemap.SwapTile(System.Array.Find(summerTileArray, t => t.name == tile.name) as TileBase, tile);
                grassTilemap.SwapTile(System.Array.Find(autumnTileArray, t => t.name == tile.name) as TileBase, tile);
                grassTilemap.SwapTile(System.Array.Find(winterTileArray, t => t.name == tile.name) as TileBase, tile);
            }
        } else if (seasonArray[currentSeasonIndex] == "Summer") {
            foreach (TileBase tile in summerTileArray)
            {
                colliderTilemap.SwapTile(System.Array.Find(autumnTileArray, t => t.name == tile.name) as TileBase, tile);
                colliderTilemap.SwapTile(System.Array.Find(winterTileArray, t => t.name == tile.name) as TileBase, tile);
                colliderTilemap.SwapTile(System.Array.Find(springTileArray, t => t.name == tile.name) as TileBase, tile);
                backgroundTilemap.SwapTile(System.Array.Find(autumnTileArray, t => t.name == tile.name) as TileBase, tile);
                backgroundTilemap.SwapTile(System.Array.Find(winterTileArray, t => t.name == tile.name) as TileBase, tile);
                backgroundTilemap.SwapTile(System.Array.Find(springTileArray, t => t.name == tile.name) as TileBase, tile);
                grassTilemap.SwapTile(System.Array.Find(autumnTileArray, t => t.name == tile.name) as TileBase, tile);
                grassTilemap.SwapTile(System.Array.Find(winterTileArray, t => t.name == tile.name) as TileBase, tile);
                grassTilemap.SwapTile(System.Array.Find(springTileArray, t => t.name == tile.name) as TileBase, tile);
            }
        } else if (seasonArray[currentSeasonIndex] == "Autumn") {
            foreach (TileBase tile in autumnTileArray)
            {
                colliderTilemap.SwapTile(System.Array.Find(winterTileArray, t => t.name == tile.name) as TileBase, tile);
                colliderTilemap.SwapTile(System.Array.Find(springTileArray, t => t.name == tile.name) as TileBase, tile);
                colliderTilemap.SwapTile(System.Array.Find(summerTileArray, t => t.name == tile.name) as TileBase, tile);
                backgroundTilemap.SwapTile(System.Array.Find(winterTileArray, t => t.name == tile.name) as TileBase, tile);
                backgroundTilemap.SwapTile(System.Array.Find(springTileArray, t => t.name == tile.name) as TileBase, tile);
                backgroundTilemap.SwapTile(System.Array.Find(summerTileArray, t => t.name == tile.name) as TileBase, tile);
                grassTilemap.SwapTile(System.Array.Find(winterTileArray, t => t.name == tile.name) as TileBase, tile);
                grassTilemap.SwapTile(System.Array.Find(springTileArray, t => t.name == tile.name) as TileBase, tile);
                grassTilemap.SwapTile(System.Array.Find(summerTileArray, t => t.name == tile.name) as TileBase, tile);
            }
        }
    }

    public void SeasonsForOpeningCutscene() {
        isDoneWithCutscene = false;
        cutsceneTimer = 1.0f;
    }

    public void SkipToNextSeason() {
        if (currentSeasonIndex == 3) {
            currentSeasonIndex = 0;
        } else {
            currentSeasonIndex = currentSeasonIndex + 1;
        }
        seasonTimer = 10.0f;
        ChangeSeasonTiles();
    }
}
