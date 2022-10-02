using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NarrativeManager : MonoBehaviour
{
    public GameObject ivyReference;
    public GameObject seasonReference;
    public GameObject textBox;
    public GameObject miniSoul;
    SylvieController ivy;
    SeasonManager seasons;

    bool continueCutsceneIvy = true;
    bool continueCutsceneSeasons = true;
    float dialogTimer = -1.0f;
    int cutSceneStage = 0;

    void Start()
    {
        ivy = ivyReference.GetComponent<SylvieController>();
        seasons = seasonReference.GetComponent<SeasonManager>();
        OpeningCutsceneStart();
    }

    void Update()
    {
        if (!continueCutsceneIvy && ivy.isDoneWithCutscene) {
            continueCutsceneIvy = true;
        }

        if (!continueCutsceneSeasons && seasons.isDoneWithCutscene) {
            continueCutsceneSeasons = true;
            if (cutSceneStage == 2) {
                PlayDialog("Now the seasons are all out of balance! I must find all four pieces.");
            }
        }

        if (dialogTimer >= 0) {
            dialogTimer -= Time.unscaledDeltaTime;
            if (dialogTimer < 0) {
                textBox.SetActive(false);
                if (cutSceneStage == 1) {
                    seasons.SeasonsForOpeningCutscene();
                    continueCutsceneSeasons = false;
                    cutSceneStage = cutSceneStage + 1;
                } else if (cutSceneStage == 3 || cutSceneStage == 4 || cutSceneStage == 5 || cutSceneStage == 6) {
                    cutSceneStage += 1;
                    Play();
                } else if (cutSceneStage == 8) {
                    ivy.ClosingCutsceneMovement();
                    miniSoul.SetActive(true);
                    PlayDialog("The forest is at peace once more.");
                } else if (cutSceneStage == 9) {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
    }

    void Pause() {
        Time.timeScale = 0.0f;
    }
    void Play() {
        Time.timeScale = 1.0f;
    }
    void PlayDialog(string text) {
        textBox.SetActive(true);
        textBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        dialogTimer = 5.0f;
        cutSceneStage = cutSceneStage + 1;
    }

    void OpeningCutsceneStart() {
        Pause();
        PlayDialog("Oh no! The storm last night blew the pieces of the Soul of Seasons all over the forest.");
    }

    public void FragmentDialog(string fragmentSeason) {
        if (SeasonManager.piecesLeft > 0) {
            PlayDialog("Here's the " + fragmentSeason + " piece! Only " + SeasonManager.piecesLeft + " more to find.");
        } else {
            PlayDialog("Here's the " + fragmentSeason + " piece, the last one! Time to put the Soul of Seasons back where it belongs.");
        }
    }

    void ClosingCutscene() {

    }
}
