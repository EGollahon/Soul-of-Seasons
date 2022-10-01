using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonManager : MonoBehaviour
{
    int currentSeasonIndex = 0;
    string[] seasonArray = {"Winter", "Spring", "Summer", "Autumn"};
    float seasonTimer = 10.0f;

    void Start()
    {
        Debug.Log("season start");
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
                Debug.Log(seasonArray[currentSeasonIndex]);
            }
        }
    }
}
