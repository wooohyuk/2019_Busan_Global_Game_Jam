//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TowerStat : Character {

//    public static List<TowerStat> towers = new List<TowerStat>();

//    [SerializeField]
//    private int type;//
//    private int hp;
//    private int Atk;
//    private float Ats;
//    private int Price;
//    [SerializeField]
//    private GameObject TowerBullet;
//    [SerializeField]
//    private GameObject TowerMelee;

//    protected override void OnEnable()
//    {
//        base.OnEnable();
//        towers.Add(this);
//    } 

//    private void OnDisable() => towers.Remove(this);

//    // Use this for initialization
//    void Start () {
//        SettingTower();
//	}
	
//	// Update is called once per frame
//	void Update () {
		
//	}
  
 
//    void SettingTower()
//    {
//        switch(type)
//        {
//            case 0:
//                hp = 95;
//                Atk = 50;
//                Ats = 0.8f;
//                Price = 15;
//                break;
//            case 1:
//                hp = 90;
//                Atk = 55;
//                Ats = 0.7f;
//                Price = 25;
//                break;
//            case 2:
//                hp = 85;
//                Atk = 0;
//                Ats = 0.5f;
//                Price = 40;
//                break;
//            case 3:
//                hp = 80;
//                Atk = 0;
//                Ats = 0.3f;
//                Price = 50;
//                break;
//            case 4:
//                hp = 200;
//                Atk = 30;
//                Ats = 0.6f;
//                Price = 70;
//                break;
//            case 5:
//                hp = 100;
//                Atk = 60;
//                Ats = 1f;
//                Price = 100;
//                break;
//            case 6:
//                Price = 55;
//                type = Random.RandomRange(0, 5);
//                SettingTower();
//                break;
//        }
//    }

//    public override void Attack(Character character)
//    {
//        if (type == 1 || type == 2)
//            Instantiate(TowerBullet);
//        else
//        {
//            Instantiate(TowerMelee);
//        }
//    }

//    public override IEnumerable<Character> GetEnemys()
//        => Monster.monsters;

//    //public override float GetAttackRange()
//    //    => type == 0 ? 1 : 4;

//    public override float GetCooltime()
//        => Ats;

//}
