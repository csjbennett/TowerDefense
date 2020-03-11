using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Health;
    public GameObject ExplosionEffect;

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            Instantiate(ExplosionEffect, this.transform.position, Quaternion.identity);
            GameObject ScriptFind = GameObject.FindGameObjectWithTag("Manager");
            ResourceManager.Currency = ResourceManager.Currency + 15;
            ScriptFind.GetComponent<ResourceManager>().TextUpdate();
            Destroy(this.gameObject);
        }
    }

    void Destination()
    {
        Instantiate(ExplosionEffect, this.transform.position, Quaternion.identity);
        GameObject ScriptFind = GameObject.FindGameObjectWithTag("Manager");
        ResourceManager.BaseHealth -= 20;
        ScriptFind.GetComponent<ResourceManager>().TextUpdate();
        ScriptFind.GetComponent<ResourceManager>().HurtEffects();
        Destroy(this.gameObject);
    }
}
