using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Gun : MonoBehaviour
{
    public enum elements {Fire, Earth, Water, Wind};

    public elements elType = elements.Fire;        // the default elemental type.

    public new string name = "Gun";
    public int damage = 2;
    public float rateOfFire = 0.5f;
    public int ammo = 10;

    [Header("Randomization")]
    public List<string> names;
    public Vector2 damageRange;
    public Vector2 rateOfFireRange;
    public Vector2 ammoRange;

    public TextMeshPro nameText;

    public Rigidbody bulletPrefab;  // the bullet to be spawned
    public Transform bulSpawn;      // the bullet spawn location

    bool onCooldown = false;

    private Rigidbody rb;

    [Header("Audio")]

    public AudioClip fire, getAmmo;

    private AudioSource aud;


    void Start() {
    
    
        aud = this.gameObject.GetComponent<AudioSource>();
        Debug.Log("nameText: " + nameText);
        rb = this.GetComponent<Rigidbody>();
    }

    public void Randomize() {
        name = names[(int)elType];
        nameText.text = name;

        damage = (int)Random.Range(damageRange.x, damageRange.y);
        ammo = (int)Random.Range(ammoRange.x, ammoRange.y);
        rateOfFire = Random.Range(rateOfFireRange.x, rateOfFireRange.y);
    }

    public void Fire() {
        if(!onCooldown && ammo > 0) {
            Rigidbody bullet = Instantiate(bulletPrefab, bulSpawn.position, bulSpawn.rotation);
            bullet.AddRelativeForce(Vector3.forward * 50, ForceMode.Impulse);
            Bullet bulletStuff = bullet.GetComponent<Bullet>();
            bulletStuff.elType = this.elType;
            bulletStuff.damage = this.damage;
            aud.PlayOneShot(fire);
            ammo -= 1;
            
            StartCoroutine(Cooldown());
        }
    }

    IEnumerator Cooldown() {
        onCooldown = true;
        yield return new WaitForSeconds( 1f / rateOfFire);
        onCooldown = false;
    }

    public void Pickup(Transform hand) {
        this.transform.SetParent(hand);
        rb.isKinematic = true;  // don't let the gun fall
        this.transform.position = hand.position;
        this.transform.rotation = hand.rotation;

    }

    public void Drop() {
        Debug.Log("Trying to Drop()");
        this.transform.SetParent(null);
        this.transform.Translate(-Vector3.forward * 2);
        rb.isKinematic = false;  // let the gun fall
        rb.AddRelativeForce(-Vector3.forward * 20, ForceMode.Impulse);
    }

        void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Ammo"))
        {
            ammo += 20;
            aud.PlayOneShot(getAmmo);
            Destroy(other.gameObject);
        }
    }
}