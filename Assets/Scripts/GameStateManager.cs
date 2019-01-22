using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class GameStateManager : MonoBehaviour {
    public static GameStateManager instance;
    public Image[] nationValueUIImages;
    public NationEvent[] nationEvents;

    public Text economicValue;
    public Text environmentalValue;
    public Text socialValue;

    public Text globalEconomicValue;
    public Text globalEnvironmentalValue;
    public Text globalSocialValue;

    int totalEconomics;
    int totalEnvironmental;
    int totalSocial;

    public Text cashText;
    public DisplayNationEvent displayNationEvent;
    public GameObject endGameImage;
    public Text endGameText;
    
    public bool isTutorial;
    public bool activeListeners = true;

    public float nationEventTimer = 5f;
    public float gameSpeed = 1f;
    private int cash;

    private List<Nation> nations;
    private int currentNationIndex = 0;
    private Nation currentNation;

    void Awake() {
        instance = this;
    }

    void Start() {
        if (isTutorial)
            return;

        nations = new List<Nation>();
        GameObject[] nationsGameObjects = GameObject.FindGameObjectsWithTag("Nation");
        for (int i = 0; i < nationsGameObjects.Length; i++) {
            nations.Add(nationsGameObjects[i].GetComponent<Nation>());
        }

        currentNation = nations[currentNationIndex];

        currentNation.Select();

        // TODO: !! HACK to initialise the correct Nation for the UI
        currentNation.UpdateEconomic(0);
        currentNation.UpdateEnvironmental(0);
        currentNation.UpdateSocial(0);
        UpdateNationUIImagesForCurrentNation();

        CalculateAndSetGlobalValues();

        StartCoroutine("GameStateTick");
        StartCoroutine("NationEventTimer");
        StartCoroutine("GenerateIncome");
    }

    void Update() {
        string message = "";
        if (totalEconomics < 0 || totalEnvironmental < 0 || totalSocial < 0) {
            if (totalEconomics < 0)
                message = "Due to poor economic managent\n";
            else if (totalEnvironmental < 0)
                message = "Due to poor environmental management\n";
            else if (totalSocial < 0)
                message = "Due to poor social liberties\n";

            message +=
                "and cooperation between nations\n the world has been ravaged and\n is no longer fit for habitation.";

            endGameText.text = message;

            EndGame();
        }

        if (totalEconomics >= 100 && totalEnvironmental >= 100 && totalSocial >= 100) {
            message =
                "Thanks to proper resource management,\ncooperation, environmental concern, and\nsocial awareness between nations\nthe world has been made sustainable\nfor future generations";

            endGameText.text = message;

            EndGame();
        }
    }

    private void EndGame() {
        activeListeners = false;

        endGameImage.SetActive(true);

        gameSpeed = 1000000f;
    }

    private void CalculateAndSetGlobalValues() {
        int normaliser = nations.Count;

        foreach (Nation nation in nations) {
            totalEconomics += nation.economicLevel;
            totalEnvironmental += nation.environmentalLevel;
            totalSocial += nation.socialLevel;
        }

        totalEconomics = totalEconomics / normaliser;
        totalEnvironmental = totalEnvironmental / normaliser;
        totalSocial = totalSocial / normaliser;

        globalEconomicValue.text = totalEconomics.ToString();
        globalEnvironmentalValue.text = totalEnvironmental.ToString();
        globalSocialValue.text = totalSocial.ToString();
    }

    private void UpdateNationUIImagesForCurrentNation() {
        Color color = currentNation.GetColorWhenNationSelected();

        foreach (Image image in nationValueUIImages) {
            image.color = color;
        }
    }

    public IEnumerator GameStateTick() {
        // TODO: !! HACK to initialise the correct Nation for the UI
        currentNation.UpdateEconomic(0);
        currentNation.UpdateEnvironmental(0);
        currentNation.UpdateSocial(0);

        CalculateAndSetGlobalValues();

        yield return new WaitForSeconds(1);
        StartCoroutine("GameStateTick");
    }

    public IEnumerator NationEventTimer() {
        ApplyNationEvent();

        yield return new WaitForSeconds(nationEventTimer * gameSpeed);
        StartCoroutine("NationEventTimer");
    }

    public IEnumerator GenerateIncome() {
        cash++;
        cash++;

        cashText.text = cash.ToString();

        yield return new WaitForSeconds(gameSpeed);
        StartCoroutine("GenerateIncome");
    }

    public void NationClickListener(Nation nation) {
        if (activeListeners == false)
            return;

        if (currentNation.Equals(nation)) {
            return;
        }

        currentNation.Deselect();

        currentNation = nation;

        currentNation.Select();

        // TODO: !! HACK to initialise the correct Nation for the UI
        currentNation.UpdateEconomic(0);
        currentNation.UpdateEnvironmental(0);
        currentNation.UpdateSocial(0);

        CalculateAndSetGlobalValues();

        UpdateNationUIImagesForCurrentNation();
    }

    void ApplyNationEvent() {
        if (Random.Range(0, 3) == 0) {
            return;
        }

        NationEvent nationEvent = nationEvents[Random.Range(0, nationEvents.Length)];

        if (Random.Range(0, 3) == 0) {
            if (nationEvent.eventName.Contains("Crisis")) {
                foreach (Nation nation in nations) {
                    nation.economicLevel += nationEvent.economicImpact;
                    nation.environmentalLevel += nationEvent.environmentalImpact;
                    nation.socialLevel += nationEvent.socialImpact;
                }
                displayNationEvent.DisplayNewGlobalEvent(nationEvent);
            }
        }
        else {
            Nation affectedNation = nations[Random.Range(0, nations.Count)];
            affectedNation.economicLevel += nationEvent.economicImpact;
            affectedNation.environmentalLevel += nationEvent.environmentalImpact;
            affectedNation.socialLevel += nationEvent.socialImpact;
            displayNationEvent.DisplayNewNationEvent(nationEvent);
        }

        CalculateAndSetGlobalValues();
    }

    public void UpdateEconomicValue(float value) {
        economicValue.text = value.ToString();
    }

    public void UpdateEnvironmentalValue(float value) {
        environmentalValue.text = value.ToString();
    }

    public void UpdateSocialValue(float value) {
        socialValue.text = value.ToString();
    }
}