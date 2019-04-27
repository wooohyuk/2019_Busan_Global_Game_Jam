using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoUI : MonoBehaviour {

    [SerializeField] Image image;
    [SerializeField] Text name;
    [SerializeField] Text damage;
    [SerializeField] Text hp;
    [SerializeField] Text attackSpeed;
    [SerializeField] Text feature;
    [SerializeField] Text cost;

    public void Initialize(string towerId)
    {
        var status = TowerTable.GetStatus(towerId);
        name.text           = status.name;
        damage.text         = $"데미지 : {status.damage}";
        hp.text             = $"체력 : {status.hp}";
        attackSpeed.text    = $"공속 : {status.attackSpeed}";
        cost.text           = status.cost.ToString();

        string f = "없음";
        if (towerId == "sg")
            f = "근거리";

        if (towerId == "jw")
            f = "원거리";

        if (towerId == "c")
            f = "슬로우";

        if (towerId == "s")
            f = "힐러";

        if (towerId == "g")
            f = "탱커";

        if (towerId == "cs")
            f = "자폭";

        feature.text        = $"특징 : {f}";

        image.sprite = status.sprite;
    }

}
