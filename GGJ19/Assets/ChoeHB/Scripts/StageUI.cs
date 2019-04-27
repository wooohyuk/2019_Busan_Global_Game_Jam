using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageUI : MonoBehaviour {

    [SerializeField] Text stageText;
    [SerializeField] Slider waveBar;
    [SerializeField] Button startButton;

    private Stage stage;
    private void Awake()
    {
        stage = Stage.instance;
        waveBar.maxValue = 1;
    }

    private void Update()
    {
        stageText.text = $"Stage {stage.curWave} / {stage.maxWave}";
        waveBar.value = stage.waveProgress;
        startButton.interactable = stage.canStart;
    }

    public void StartStage() => stage.StartStage();

}
