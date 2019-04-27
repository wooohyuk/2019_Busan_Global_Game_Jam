//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Spawner : MonoBehaviour {
//    [SerializeField]
//    private GameObject G_Monster;//소환하는 몬스터
//    private GameObject PoolingMonster;
//    [SerializeField]
//    private GameObject[] Pool = new GameObject[10];
//    private int Wave=0;
//    private int SpawnMonsterNum;//스폰되는 몬스터 수
//    private float SpawnCoolTime;
//    private int count = 0;
//    private bool bfull = false;

//	// Use this for initialization
//	void Start () {
//	    for(int i=0; i<10; i++)
//        {
//            Pool[i] = null;
//        }
//        StartCoroutine(Spawn());
//	}
	
//	// Update is called once per frame
//	void Update () {
		
//	}

//    IEnumerator Spawn()
//    {
//        while (true)
//        {
//            SpawnSetting();
//            Monster S_Monster = G_Monster.GetComponent<Monster>();
//            S_Monster.setMonsterNum(randMonsterNum());
//            S_Monster.MonsterSetting();
//            yield return new WaitForSeconds(1.0f);
//            if (count < 10&&Pool[count]==null)
//            {
//                PoolingMonster = Instantiate(G_Monster);
//                PoolingMonster.GetComponent<Monster>().setMonsterNum(randMonsterNum());
//                PoolingMonster.GetComponent<Monster>().MonsterSetting();
//                Pool[count] = PoolingMonster;
//                count++;
//            }
//            else if(count==10&&!bfull)
//            {
//                bfull = true;
//                continue;
//            }
//            else if(bfull)
//            {
//                int index = 0;
//                while(true)
//                {
//                    if (Pool[index].active)
//                    {
//                        index++;
//                        if (index > 9)
//                        {
//                            index = 0;
//                            break;
//                        }
                           
//                    }
//                    else
//                    {
//                        PoolingMonster = Pool[index];
//                        PoolingMonster.transform.position = gameObject.transform.position;
//                        PoolingMonster.SetActive(true);
//                        break;
//                    }  
//                }
//            }
//        }
//    }
//    private int randMonsterNum()
//    {
//        int Monsternum=1;
//        switch(Wave)
//        {
//            case 1:
//                Monsternum = 1;
//                break;
//            case 2:
//                Monsternum = Random.RandomRange(0, 1);
//                break;
//            case 3:
//                Monsternum = Random.RandomRange(0, 2);
//                break;
//            case 4:
//                Monsternum = Random.RandomRange(0, 2);
//                break;
//            case 5:
//                Monsternum = Random.RandomRange(0, 2);
//                break;
//        }
//        return Monsternum;
//    }
//    void SpawnSetting()
//    {
//        switch(Wave)
//        {
//            case 1:
//                SpawnMonsterNum = 10;
//                SpawnCoolTime = 3.0f;
//                break;
//            case 2:
//                SpawnMonsterNum = 20;
//                SpawnCoolTime = 2.5f;
//                break;
//            case 3:
//                SpawnMonsterNum = 30;
//                SpawnCoolTime = 2.5f;
//                break;
//            case 4:
//                SpawnMonsterNum = 30;
//                SpawnCoolTime = 2.0f;
//                break;
//            case 5:
//                SpawnMonsterNum = 40;
//                SpawnCoolTime = 2.0f;
//                break;
//        }
//    }
//}
