using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavesController : MonoBehaviour
{
    SpriteRenderer leavesRenderer;
    public string leavesSeason;
    public Sprite leavesSprite;
    string seasonOnLastUpdate;

    void Start()
    {
        leavesRenderer = gameObject.GetComponent<SpriteRenderer>();
        seasonOnLastUpdate = SeasonManager.seasonArray[SeasonManager.currentSeasonIndex];

        if (SeasonManager.seasonArray[SeasonManager.currentSeasonIndex] == leavesSeason) {
            leavesRenderer.sprite = leavesSprite;
        } else {
            leavesRenderer.sprite = null;
        }
    }

    
    void Update()
    {
        if (seasonOnLastUpdate != SeasonManager.seasonArray[SeasonManager.currentSeasonIndex]) {
            if (SeasonManager.seasonArray[SeasonManager.currentSeasonIndex] == leavesSeason) {
                leavesRenderer.sprite = leavesSprite;
            } else {
                leavesRenderer.sprite = null;
            }
        }
        seasonOnLastUpdate = SeasonManager.seasonArray[SeasonManager.currentSeasonIndex];

    }
}
