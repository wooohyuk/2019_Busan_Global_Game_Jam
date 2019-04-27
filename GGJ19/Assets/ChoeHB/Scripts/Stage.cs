using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage : AdvStaticComponent<Stage> {

    [SerializeField] List<AudioClip> bgms;

    public static int stageIndex = 2;

    public static void StartStage(int stageIndex) {
        Stage.stageIndex = stageIndex;
        GlobalObjectPool.Clear();
        SceneManager.LoadScene("testScene");
    } 
    
    [SerializeField] FieldGenerator fieldGenerator;

    private List<List<StageTable.SpawnData>> waveDatas;
    private Dictionary<string, LocalObjectPool<Ghost>> ghostPools;

    protected override void Initialize()
    {
        AudioManager.PlayMusic(bgms[stageIndex - 1]);
        base.Initialize();

        var stageData = StageTable.GetStageData(stageIndex);

        CostManager.instance.Earn(stageData.defaultCost);

        SummonTower.instance.SetTowerCount(stageData.towerCount);
        fieldGenerator.Generate(stageData.column, stageData.row);

        waveDatas = StageTable.GetStage(stageIndex);
        ghostPools = new Dictionary<string, LocalObjectPool<Ghost>>();

        HashSet<string> ghostIds = new HashSet<string>(
            from wave in waveDatas
            from data in wave
            select data.id
        );

        foreach (var id in ghostIds)
        {
            Ghost prefab = (Ghost)GhostTable.GetStatus(id).prefab;
            var holder = new GameObject($"{id} Holder").transform;
            var pool = new LocalObjectPool<Ghost>(prefab, holder, 15);
            ghostPools.Add(id, pool);
        }

        StartCoroutine(Staging());
        
    }

    private bool pressStart;
    private int unitCount;

    public int curWave          { get; private set; }
    public int maxWave          { get; private set; }
    public bool canStart        { get; private set; }
    public float waveProgress   => Mathf.InverseLerp(startTime, endTime, Time.time);

    private float startTime;
    private float endTime;

    private List<Ghost> ghosts;

    [SerializeField] BossHpBar bossUI;

    private IEnumerator Staging()
    {
        unitCount = waveDatas.Count;
        maxWave = waveDatas.Count;

        curWave = 1;
        
        foreach (var wave in waveDatas)
        {
            canStart = true;
            pressStart = false;
            ghosts = new List<Ghost>();

            yield return new WaitUntil(() => pressStart);

            startTime = Time.time;
            endTime = Time.time + wave[0].cooltime * wave.Count;

            canStart = false;
            unitCount = wave.Count;
            
            
            foreach (var data in wave)
            {
                int line = data.line;
                Ghost instance = ghostPools[data.id].GetPooledObject();
                ghosts.Add(instance);
                instance.OnDead += OnDeadGhost;
                instance.Spawn(line);

                yield return new WaitForSeconds(data.cooltime);
                if (wave == waveDatas.Last())
                    bossUI._Float(instance);
            }

            yield return new WaitUntil(() => unitCount == 0);

            if (curWave == maxWave)
            {
                ResultPanel.instance.Clear();
                yield break;
            }

            curWave++;

        }
        
    }


    public void StartStage() => pressStart = true;

    private void Update()
    {
        if (ghosts.Count != 0)
        {
            foreach (var ghost in ghosts)
                if(ghost.transform.position.x < -8)
                {
                    ResultPanel.instance.Fail();
                    return;
                }
        }
    
        if (Input.GetKeyDown(KeyCode.Space))
            pressStart = true;
    }


    private void OnDeadGhost(Character ghost)
    {
        ghost.OnDead -= OnDeadGhost;
        ghosts.Remove((Ghost)ghost);
        unitCount--;
    }
}

