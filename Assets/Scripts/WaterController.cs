using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour
{
    SpriteRenderer waterRenderer;
    Sprite waterSprite;
    Sprite iceSprite;
    string seasonOnLastUpdate;
    public Vector2 respawnPoint;

    void Start()
    {
        waterRenderer = gameObject.GetComponent<SpriteRenderer>();
        seasonOnLastUpdate = SeasonManager.seasonArray[SeasonManager.currentSeasonIndex];

        waterSprite = Resources.Load<Sprite>("water");
        iceSprite = Resources.Load<Sprite>("ice");

        ChangeSeasonSprite();
    }

    
    void Update()
    {
        ChangeSeasonSprite();
        seasonOnLastUpdate = SeasonManager.seasonArray[SeasonManager.currentSeasonIndex];

    }

    void ChangeSeasonSprite()
    {
        if (SeasonManager.seasonArray[SeasonManager.currentSeasonIndex] == "Winter") {
            waterRenderer.sprite = iceSprite;
        } else if (SeasonManager.seasonArray[SeasonManager.currentSeasonIndex] == "Summer") {
            waterRenderer.sprite = null;
        } else {
            waterRenderer.sprite = waterSprite;
        }
    }
}
