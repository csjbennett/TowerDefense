using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject Target;
    public int Hold;
    private Vector3 TargetVector;
    private Vector3 TargetPosition;
    public GameObject Turret;
    public float ShootTime;
    public GameObject Particles;
    public GameObject ParticleSpawner;
    public GameObject UpgradeParts;
    private bool Shooting;
    private AudioSource Audio;
    public AudioClip ShootSound;

    // Start is called before the first frame update
    void Awake()
    {
        Shooting = false;
        Hold = 0;
        UpgradeParts.GetComponent<MeshRenderer>().enabled = false;
        Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Enemies[0] != null)
        {
            Target = Enemies[0];
            TargetVector = Target.transform.position - this.transform.position;
            Turret.transform.rotation = Quaternion.LookRotation(TargetVector);
        }
        else
        {
            Target = Turret;
            Enemies[0] = null;
            Hold = 0;
            CancelInvoke("Shoot");
            Turret.transform.rotation = Quaternion.identity;
            Shooting = false;
        }
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.tag == "Enemy")
        {
            if (Shooting == false)
            {
                InvokeRepeating("Shoot", 0, ShootTime);
                Shooting = true;
            }
            Enemies[Hold] = trigger.gameObject;
            Hold++;
        }
    }

    private void OnTriggerStay(Collider trigger)
    {
        if (trigger.gameObject.tag == "Enemy" && Enemies[0] == null)
        {
            Enemies[0] = trigger.gameObject;
            if (Shooting == false)
            {
                InvokeRepeating("Shoot", 0.1f, ShootTime);
                Shooting = true;
            }
        }
    }

    private void OnTriggerExit(Collider trigger)
    {
        if (trigger.gameObject.tag == "Enemy")
        {
            Hold--;
            Enemies[0] = null;

            if (Enemies[0] == null && (Enemies[1] != null))
                for (int i = 0; i < Enemies.Length - 1; i++)
                    Enemies[i] = Enemies[i + 1];
            else if (Enemies[0] == null && (Enemies[1] != null || Enemies[2] != null || Enemies[3] != null))
                for (int i = 1; i < Enemies.Length - 1; i++)
                    Enemies[i] = Enemies[i + 1];
        }
    }

    public void Upgrade()
    {
        ShootTime = ShootTime / 1.5f;
        UpgradeParts.GetComponent<MeshRenderer>().enabled = true;
        transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y * 1.5f, this.transform.localScale.z);
    }

    void Shoot()
    {
        if (Enemies[0] != null)
            TargetVector = Target.transform.position - this.transform.position;
        Enemies[0].GetComponent<Enemy>().Health--;
        Turret.transform.rotation = Quaternion.LookRotation(TargetVector);
        Instantiate(Particles, ParticleSpawner.transform.position, ParticleSpawner.transform.rotation);
        Audio.PlayOneShot(ShootSound);
    }
}
