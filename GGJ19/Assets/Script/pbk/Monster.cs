//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Monster : MonoBehaviour {

//    private const float speedRate = 0.05f;

//    public static List<Monster> monsters = new List<Monster>();

//    [SerializeField]
//    private float speed;
//    [SerializeField]
//    private GameObject MonsterBullet;
//    [SerializeField]
//    private GameObject Melee;
//    private int Atk;
//    private float Ats;
//    private int hp;
//    private Transform tr;
//    [SerializeField]
//    private int type;//0:근거리 1:원거리
//    private int MonsterNum;//0:쥐 1:뱀 2:멧돼지 3:잡귀 4:감기귀신 5: 달걀귀신 6:뱀냥이 7그슨대 8도깨비 9 귀수산 10 구미호 11이무기
//    private float dist;
//    bool isStop;

//    //protected override void OnEnable()
//    //{
//    //    base.OnEnable();
//    //    monsters.Add(this);
//    //}

//    private void OnDisable() => monsters.Remove(this);

//    // Use this for initialization
//    void Start () {
//        tr = GetComponent<Transform>();
//        type = 1;
//	}

//    // Update is called once per frame

//    void Update()
//    {

//        tr.Translate(new Vector2(-speed * Time.deltaTime, 0));
//        if (Input.GetKeyDown(KeyCode.Space))
//        {
//            gameObject.SetActive(false);
//        }
        
//    }

//    public void MonsterSetting()
//    {
//        switch(MonsterNum)
//        {
//            case 0:
//                type = 0;
//                hp = 50;
//                Atk = 10;
//                Ats = 0.8f;
//                speed = 65;
//                break;
//            case 1:
//                type = 0;
//                hp = 45;
//                Atk = 20;
//                Ats = 0.75f;
//                speed = 60;
//                break;
//            case 2:
//                type = 0;
//                hp = 55;
//                Atk = 15;
//                Ats = 0.75f;
//                speed = 65;
//                break;
//            case 3:
//                type = 0;
//                hp = 65;
//                Atk = 15;
//                Ats = 0.75f;
//                speed = 60;
//                break;
//            case 4:
//                type = 1;
//                dist = 2;
//                hp = 60;
//                Atk = 25;
//                Ats = 0.75f;
//                speed = 65;
//                break;
//            case 5:
//                type = 0;
//                hp = 60;
//                Atk = 20;
//                Ats = 0.75f;
//                speed = 65;
//                break;
//            case 6:
//                type = 0;
//                hp = 55;
//                Atk = 30;
//                Ats = 0.85f;
//                speed = 70;
//                break;
//            case 7:
//                type = 0;
//                hp = 70;
//                Atk = 35;
//                Ats = 0.85f;
//                speed = 75;
//                break;
//            case 8:
//                type = 0;
//                hp = 75;
//                Atk = 25;
//                Ats = 0.85f;
//                speed = 75;
//                break;
//            case 9:
//                type = 0;
//                hp = 100;
//                Atk = 20;
//                Ats = 0.5f;
//                speed = 35;
//                break;
//            case 10:
//                type = 1;
//                dist = 3;
//                hp = 65;
//                Atk = 35;
//                Ats = 0.9f;
//                speed = 80;
//                break;
//            case 11:
//                type = 1;
//                dist = 4;
//                hp = 60;
//                Atk = 45;
//                Ats = 1f;
//                speed = 85;
//                break;
//        }
//        dist = type == 0 ? 1 : 4;
//        speed *= speedRate;
//    }

//    public void setMonsterNum(int argMonsterNum)
//    {
//        name = $"Monster {argMonsterNum}";
//        MonsterNum = argMonsterNum;
//    }

//    public override IEnumerable<Character> GetEnemys()
//        => TowerStat.towers;

//    //public override float GetAttackRange()
//    //{
//    //    Debug.Log(GetHashCode() + " Dist ----------- ".Fill("White") + dist);
//    //    return dist;
//    //}

//    public override float GetCooltime()
//        => Ats;

//    public override void Attack(Character target)
//    {
//        switch (type)//공격
//        {
//            case 0://근거리
                

//               MeleeAttack S_Melee = Melee.GetComponent<MeleeAttack>();
//               S_Melee.SetAtk(Atk);
//               //거리계산 후 생성
//               Instantiate(Melee,gameObject.transform);
//               break;
//            case 1://원거리
//                  // //거리 계산 후 생성
//                //MeleeAttack S_Melee2 = Melee.GetComponent<MeleeAttack>();
//                //S_Melee2.SetAtk(Atk);
//                ////거리계산 후 생성
//                //Instantiate(Melee, gameObject.transform);

           
//                Instantiate(MonsterBullet);
//                break;
//        }
//    }
//}
