using System.Collections;
using UnityEngine;

public class Boss3 : Enemy
{
    private Animator animator;
    private float basePositionX;
    private float chargeSpeed;

    private ObjectPooler projectilePool;
    private float shootTimer;
    private float shootInterval;

    public override void OnEnable(){
        base.OnEnable();
        basePositionX = 7f + Random.Range(-1f, 1f);
        chargeSpeed = Random.Range(1.2f, 2.5f);
        speedY = Random.value < 0.5 ? -1f : 1f;
        shootInterval = Random.Range(2f, 3f);
        shootTimer = 1f;
    }

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        destroyEffectPool = GameObject.Find("Boom3Pool").GetComponent<ObjectPooler>();
        projectilePool = GameObject.Find("SquidPool").GetComponent<ObjectPooler>();
        hitSound = AudioManager.Instance.squidHit2;
        destroySound = AudioManager.Instance.boom2;
    }

    public override  void Update()
    {
        base.Update();

        // movement x
        float currentX = transform.position.x;
        if (Mathf.Abs(currentX - basePositionX) > 0.1f){
            float posX = Mathf.Lerp(currentX, basePositionX, chargeSpeed * Time.deltaTime);
            transform.position = new Vector3(posX, transform.position.y);
        }

        // movement y
        if (transform.position.y > 5 || transform.position.y < -5){
            speedY *= -1;
        }

        // shooting
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0){
            shootTimer += shootInterval;
            Shoot();
        }
    }

    private void Shoot(){
        GameObject projectile = projectilePool.GetPooledObject();
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;
        projectile.SetActive(true);
        AudioManager.Instance.PlaySound(AudioManager.Instance.squidShoot3);
        animator.SetBool("shooting", true);
        StartCoroutine(ResetShoot());
    }

    IEnumerator ResetShoot(){
        yield return null; // wait one frame
        animator.SetBool("shooting", false);
    }
}
