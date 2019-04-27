using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Effect : SerializedMonoBehaviour {

    public class EffectInfo
    {
        public string id;
        public float interval = 0.1f;
        public Sprite[] sprites;
    }

    [TableList]
    [SerializeField] List<EffectInfo> effects;
    [SerializeField] SpriteRenderer sr;

    private void Reset() => sr = GetComponent<SpriteRenderer>();

    public void Play(string id, Vector3 position, float scale)
    {
        transform.position = position;
        transform.localScale = Vector3.one * scale;
        sr.sprite = null;
        gameObject.SetActive(true);
        StartCoroutine(Playing(id));
    }

    private IEnumerator Playing(string id)
    {
        var effect = effects.SingleOrDefault(s => s.id == id);
        if (effect == null)
            throw new System.Exception($"{id}를 찾을 수 없음");

        foreach (var sprite in effect.sprites)
        {
            sr.sprite = sprite;
            yield return new WaitForSeconds(effect.interval);
        }
        sr.sprite = null;
        gameObject.SetActive(false);
    }
}
