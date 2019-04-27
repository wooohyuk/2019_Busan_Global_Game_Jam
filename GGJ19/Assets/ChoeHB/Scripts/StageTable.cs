using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Sirenix.OdinInspector;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

using Random = UnityEngine.Random;

public class StageTable : SingletonScriptableObject<StageTable>
{

#if UNITY_EDITOR

    [MenuItem("테이블/스테이지")]
    public static void Select()
        => Selection.activeObject = instance;

#endif

    public struct WaveData
    {
        [TableColumnWidth(40, false)]
        public bool isBoss;

        public string ids;

        [TableColumnWidth(40, false)]
        public float cooltime;

        [TableColumnWidth(40, false)]
        public int count;

        public Dictionary<string, int> genRate;

    }

    public struct SpawnData
    {
        public int line;
        public string id;
        public float cooltime;
    }

    public struct StageData
    {
        public int towerCount;
        public int column;
        public int row;
        public int defaultCost;
    }

    [TableList]
    [SerializeField] List<StageData> stageDatas;

    [TableList] [TabGroup("Stage1")]
    [SerializeField] List<WaveData> stage1;

    [TableList] [TabGroup("Stage2")]
    [SerializeField] List<WaveData> stage2;

    [TableList] [TabGroup("Stage3")]
    [SerializeField] List<WaveData> stage3;

    [TableList] [TabGroup("Egg")]
    [SerializeField] List<WaveData> eggData;

    public static StageData GetStageData(int stage) => instance.stageDatas[stage - 1];

    public static List<List<SpawnData>> GetStage(int index)
    {
        if (index == 1)
            return GetStage1();
        if (index == 2)
            return GetStage2();
        if (index == 3)
            return GetStage3();
        if (index == 4)
            return GetEggStage();

        throw new Exception($"스테이지 {index}에 대한 정보는 아직 없음");
    }

    [Button]
    private void SetStage(int stage) => Stage.stageIndex = stage;

    private static List<List<SpawnData>> GetStage1()
        => CreateWaves(instance.stage1, GetStageData(1).column);

    private static List<List<SpawnData>> GetStage2()
        => CreateWaves(instance.stage2, GetStageData(2).column);

    private static List<List<SpawnData>> GetStage3()
        => CreateWaves(instance.stage3, GetStageData(3).column);

    public static List<List<SpawnData>> GetEggStage()
        => CreateWaves(instance.eggData, GetStageData(4).column);

    private static List<List<SpawnData>> CreateWaves(List<WaveData> waveDatas, int lineCount)
    {
        int a = 1;
        List<List<SpawnData>> waves = new List<List<SpawnData>>();
        foreach (var data in waveDatas)
        {
            string[] ids = data.ids.SplitTrim(",");
            Dictionary<string, int> genRate = new Dictionary<string, int>();
            foreach (var id in ids)
                genRate.Add(id, data.genRate[id]);

            waves.Add(CreateWave(ids, data.count, data.cooltime, lineCount, data.isBoss, genRate));
        }
        return waves;
    }

    private static List<SpawnData> CreateWave(string[] gsIds, int count, float cooltime, int lineCount, bool isBoss, Dictionary<string, int> genRate)
    {
        List<SpawnData> wave = new List<SpawnData>();
        for (int i = 0; i < count; i++)
        {
            SpawnData data = new SpawnData();
            data.id = genRate.Random();
            data.line = isBoss ? 0 : Random.Range(0, lineCount);
            data.cooltime = cooltime;
            wave.Add(data);
        }
        return wave;
    }
}