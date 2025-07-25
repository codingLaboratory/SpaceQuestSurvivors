using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource ice;
    public AudioSource fire;
    public AudioSource hit;
    public AudioSource pause;
    public AudioSource unpause;
    public AudioSource boom2;
    public AudioSource hitRock;
    public AudioSource shoot;
    public AudioSource squished;
    public AudioSource burn;
    public AudioSource hitArmor;
    public AudioSource bossCharge;
    public AudioSource bossSpawn;
    public AudioSource beetleHit;
    public AudioSource beetleDestroy;
    public AudioSource locustHit;
    public AudioSource locustDestroy;
    public AudioSource locustCharge;
    public AudioSource squidHit;
    public AudioSource squidHit2;
    public AudioSource squidDestroy;
    public AudioSource squidDestroy2;
    public AudioSource squidShoot;
    public AudioSource squidShoot2;
    public AudioSource squidShoot3;

    void Awake(){
        if (Instance != null){
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    public void PlaySound(AudioSource sound){
        sound.Stop();
        sound.Play();
    }

    public void PlayModifiedSound(AudioSource sound){
        sound.pitch = Random.Range(0.7f, 1.3f);
        sound.Stop();
        sound.Play();
    }
}
