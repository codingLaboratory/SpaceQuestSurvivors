using UnityEngine;

public class SquidCritter : Enemy
{
    [SerializeField] private Sprite[] sprites;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float moveSpeed;
    private float targetMoveSpeed;

    public override void OnEnable(){
        base.OnEnable();
        moveSpeed = 8f;
        targetMoveSpeed = Random.Range(0.1f, 1.2f);
    } 

    public override void Start()
    {
        base.Start();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        destroyEffectPool = GameObject.Find("SquidCritterPopPool").GetComponent<ObjectPooler>();
        hitSound = AudioManager.Instance.squidHit;
        destroySound = AudioManager.Instance.squidDestroy2;
    }

    public override void Update()
    {
        base.Update();
        targetPosition = PlayerController.Instance.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        Vector3 relativePos = targetPosition - transform.position;
        if (relativePos != Vector3.zero){
            targetRotation = Quaternion.LookRotation(Vector3.forward, relativePos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 90 * Time.deltaTime);
        }

        if (moveSpeed != targetMoveSpeed){
            moveSpeed = Mathf.Lerp(moveSpeed, targetMoveSpeed, Time.deltaTime * 3f);
        }
    }
}
