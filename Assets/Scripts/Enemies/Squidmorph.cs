using UnityEngine;

public class Squidmorph : Enemy
{
    [SerializeField] private Sprite[] sprites;
    private Quaternion targetRotation;
    private float targetSpeedX;
    private ObjectPooler projectilePool;

    private float shootTimer;
    private float shootInterval;

    public override void OnEnable(){
        base.OnEnable();
        transform.rotation = Quaternion.Euler(0, 0, 90);
        speedX = -15f;
        targetSpeedX = Random.Range(-0.1f, -0.6f);
        speedY = Random.Range(-0.3f, 0.3f);
        shootInterval = Random.Range(1f, 6f);
    }

    public override void Start()
    {
        base.Start();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        destroyEffectPool = GameObject.Find("SquidPopPool").GetComponent<ObjectPooler>();
        projectilePool = GameObject.Find("SquidCritterPool").GetComponent<ObjectPooler>();
        hitSound = AudioManager.Instance.squidHit2;
        destroySound = AudioManager.Instance.squidDestroy;
    }


    public override void Update()
    {
        base.Update();

        if (transform.position.y > 5 || transform.position.y < -5){
            speedY *= -1;
        }

        if (speedX != targetSpeedX){
            speedX = Mathf.Lerp(speedX, targetSpeedX, Time.deltaTime * 2.5f);
            if (Mathf.Abs(speedX - targetSpeedX) < 1f){
                speedX = targetSpeedX;
            }
        } else {
            Vector3 relativePos = PlayerController.Instance.transform.position - transform.position;
            if (relativePos != Vector3.zero){
                targetRotation = Quaternion.LookRotation(Vector3.forward, relativePos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1080 * Time.deltaTime);
            }

            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0){
                shootTimer += shootInterval;
                Shoot();
            }
        }
    }

    private void Shoot(){
        GameObject projectile = projectilePool.GetPooledObject();
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;
        projectile.SetActive(true);
        if (Random.value < 0.3f){
            AudioManager.Instance.PlaySound(AudioManager.Instance.squidShoot);
        } else {
            AudioManager.Instance.PlaySound(AudioManager.Instance.squidShoot2);
        }
    }
}
