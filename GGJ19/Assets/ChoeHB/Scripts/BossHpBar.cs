using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : AdvSingletonComponent<BossHpBar> {

    [SerializeField] Text hpText;
    [SerializeField] Text bossName;
    [SerializeField] Image bossImage;
    [SerializeField] Slider hpBar;

    [SerializeField] float zoomSize = 1;
    [SerializeField] float zoomDuring = 1;
    [SerializeField] float zoomWaiting = 1;

    [SerializeField] TextPool tp;
    [SerializeField] float tpInterval;
    [SerializeField] float tpWaiting = 3;

    private Ghost boss;
    public static void Float(Ghost boss)
    {
        instance._Float(boss);
    }

    public void _Float(Ghost boss)
    {
        this.boss = boss;
        bossName.text = boss.name;
        bossImage.sprite = boss.sprite;
        hpBar.maxValue = boss.maxHp;
        gameObject.SetActive(true);

        StartCoroutine(Floating());

        Vector3 originPosition = Camera.main.transform.position;
        Vector3 dst = Camera.main.transform.position;
        dst.x = (dst.x * 4 + boss.transform.position.x) / 5;

        float originZoom = Camera.main.orthographicSize;
        var seq = DOTween.Sequence();
            seq.Append(Camera.main.DOOrthoSize(zoomSize,  zoomDuring));
            seq.Join(Camera.main.transform.DOMove(dst, zoomDuring));
            seq.AppendInterval(zoomWaiting);
            seq.Append(Camera.main.DOOrthoSize(originZoom, zoomDuring));
            seq.Join(Camera.main.transform.DOMove(originPosition, zoomDuring));
        seq.AppendInterval(3);
        seq.OnComplete(() => tp.gameObject.SetActive(false));

    }

    private IEnumerator Floating()
    {
        for (int i = 0; i < boss.name.Length; i++)
        {
            tp.Float(boss.name[i].ToString(), Vector3.zero);
            yield return new WaitForSeconds(tpInterval);
        }
        yield return new WaitForSeconds(tpWaiting);
        tp.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (boss == null)
            gameObject.SetActive(false);
        hpText.text = $"{boss.hp} / {boss.maxHp}";
        hpBar.value = boss.hp;
    }


}
