using UnityEngine;

public class Beetlemorph : Enemy
{
    [SerializeField] private Sprite[] sprites;

    public override void Start(){
        base.Start();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        destroyEffectPool = GameObject.Find("BeetlePopPool").GetComponent<ObjectPooler>();
        hitSound = AudioManager.Instance.beetleHit;
        destroySound = AudioManager.Instance.beetleDestroy;
        speedX = Random.Range(-0.8f, -1.5f);
    }
}
