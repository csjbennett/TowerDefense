using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject Enemy;
    float Timer = 0;
    public float SpawnDelay;
    public Text TimerText;
    public bool Spawning;

    // Start is called before the first frame update
    void Start()
    {
        Spawning = true;
        SpawnDelay = 3;
        StartCoroutine(SpawnTimer());
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
        TimerText.text = "Time Remaining: " + (Mathf.RoundToInt(Mathf.Abs((Timer - 90))).ToString());
    }

    void Spawn()
    {
        GameObject NewEnemy = Instantiate(Enemy, this.transform.position, Quaternion.identity);
        Animator anm = NewEnemy.GetComponent<Animator>();
        if (NewEnemy.GetComponent<Enemy>().Health < 8)
        NewEnemy.GetComponent<Enemy>().Health = Mathf.RoundToInt(Timer/20 + 3);
        if (anm.speed <= 4)
            anm.speed = (Timer / 60 + 1);
    }

    IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(SpawnDelay);
        if (Spawning)
        if (SpawnDelay > 2)
            SpawnDelay -= 0.1f;
        if (SpawnDelay > 0.75f)
            SpawnDelay -= 0.05f;
        Spawn();
        StartCoroutine(SpawnTimer());
    }
}
