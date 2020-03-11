using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResourceManager : MonoBehaviour
{
    public Text CurrencyDisplay;
    public Text HealthDisplay;
    public static float Currency;
    public static float BaseHealth;
    private float Timer;
    public Camera Cam;
    private float ShakeTime;
    public Vector3 OrigPos;
    public float HurtDuration;
    public float ShakeAmplitude;
    private float FadeOut;
    public Image Fade;
    private bool start;
    private bool end;
    private bool endWin;

    void Start()
    {
        endWin = false;
        end = false;
        start = true;
        FadeOut = 1.5f;
        Timer = 0;
        Currency = 150;
        BaseHealth = 100;
        TextUpdate();
        OrigPos = Cam.transform.position;
    }

    private void Update()
    {
        if (FadeOut > 0 & start == true)
        {
            FadeOut -= Time.deltaTime;
            Fade.color = new Color(0, 0, 0, FadeOut);
        }
        else
            start = false;

        Timer += Time.deltaTime;
        if (Timer >= 90 && !end)
        {
            endWin = true;
        }

        if (ShakeTime > 0)
        {
            Cam.transform.position = OrigPos + Random.insideUnitSphere * ShakeAmplitude;
            ShakeTime -= Time.deltaTime;
        }

        if (end)
        {
            FadeOut += Time.deltaTime;
            Fade.color = new Color(0, 0, 0, FadeOut);
            ShakeTime = 1;
            if (FadeOut > 1.25f)
                SceneManager.LoadScene(3);
        }
        if (endWin)
        {
            if (GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>().Spawning)
                GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>().Spawning = false;
            FadeOut += Time.deltaTime;
            Fade.color = new Color(0, 0, 0, FadeOut);
            ShakeTime = 1;
            if (FadeOut > 1.25f)
                SceneManager.LoadScene(2);
        }
    }
    // Update is called once per frame

    public void TextUpdate()
    {
        CurrencyDisplay.text = "Currency: " + Currency.ToString();
        HealthDisplay.text = "Health: " + BaseHealth.ToString();
        if (BaseHealth <= 0)
            end = true;
    }

    public void HurtEffects()
    {
        ShakeTime = HurtDuration;
    }
}
