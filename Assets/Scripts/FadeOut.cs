using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Image Fade;
    private float FadeVal;

    // Start is called before the first frame update
    void Start()
    {
        FadeVal = 1.25f;
        Fade.color = new Color(0, 0, 0, FadeVal);
    }

    // Update is called once per frame
    void Update()
    {
        if (Fade)
        {
            FadeVal -= 0.01f;
            Fade.color = Fade.color = new Color(0, 0, 0, FadeVal);
            if (FadeVal <= 0)
                Destroy(Fade);
        }
    }
}
