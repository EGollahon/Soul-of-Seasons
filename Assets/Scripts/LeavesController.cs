using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeavesController : MonoBehaviour
{
    SpriteRenderer leavesRenderer;
    public string leavesSeason;
    Sprite leavesSprite;
    string seasonOnLastUpdate;

    void Start()
    {
        leavesRenderer = gameObject.GetComponent<SpriteRenderer>();
        seasonOnLastUpdate = SeasonManager.seasonArray[SeasonManager.currentSeasonIndex];

        if (leavesSeason == "Spring") {
            leavesSprite = Resources.Load<Sprite>("spring-leaf");
        } else {
            leavesSprite = Resources.Load<Sprite>("autumn-leaf");
        }

       ChangeSeasonSprite();
    }

    void Update()
    {
        if (seasonOnLastUpdate != SeasonManager.seasonArray[SeasonManager.currentSeasonIndex]) {
            ChangeSeasonSprite();
        }
        seasonOnLastUpdate = SeasonManager.seasonArray[SeasonManager.currentSeasonIndex];

    }

    void ChangeSeasonSprite()
    {
        if (SeasonManager.seasonArray[SeasonManager.currentSeasonIndex] == leavesSeason) {
            leavesRenderer.sprite = leavesSprite;
        } else {
            leavesRenderer.sprite = null;
        }
    }
}
