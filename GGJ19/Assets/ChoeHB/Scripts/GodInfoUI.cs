using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodInfoUI : MonoBehaviour {

    [SerializeField] Image image;
    [SerializeField] Text name;
    [SerializeField] Text content;

    public void SetId(string id)
    {
        var status = TowerTable.GetStatus(id);
            image.sprite = status.sprite;
            image.name = status.name;

        string s = "";
        if (id == "sg")
            s = "마루와 집 문을 지키는 신, 흔히들 대장신이라고 하며 현재 문짝에 빙의중이다. 몸을 들이 받아 공격한다.";
        if (id == "jw")
            s = "부엌을 지키는 신, 아궁이에 빙의중이며, 요리를 담당하고 불을 다스릴줄 안다. 때문에 불을 날려 공격한다.";
        if (id == "c")
            s = "뒷간을 지키는 신, 똥을 닦는 새끼줄에 빙의중이며, 마법으로 적에게 슬로우를 건다.";
        if (id == "s")
            s = "안방을 지키는 신, 지팡이에 빙의중이며, 집안에 아이가 태어나면 아이의 앞길을 점지해주고 아이들을 지켜준다, 아군을 회복시켜준다.";
        if (id == "g")
            s = "집터를 지키는 신, 농사와 땅에 관여하며 현재 돌덩이에 빙의중이다, 특별한 능력은 없지만 단단한 신이다.";
        if (id == "cs")
            s = "장독대를 지키는 신, 현재 장독대에 빙의중이며, 자폭하여 적에게 강한 피해를 준다.";

        content.text = s;
    }

}
