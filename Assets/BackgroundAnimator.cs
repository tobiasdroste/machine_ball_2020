using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnimator : MonoBehaviour
{
    private Image image;
    private float i = 0;

    private RectTransform rectTransform;
    private bool background1;



    private void Start() {
        image = transform.GetComponent<UnityEngine.UI.Image>();
        rectTransform = GetComponent<RectTransform>();

        if(name.Equals("Background1")) background1 = true;

        float factor = image.rectTransform.rect.width / image.material.mainTexture.width;
        image.material.mainTextureScale = new Vector2(factor, factor);

    }

    // Update is called once per frame
    void Update()
    {

        i += Time.deltaTime / 2.0f;

        if(background1)
        image.color = new Color(Mathf.Sin(Mathf.PI*i)/2f+0.3f, Mathf.Sin(Mathf.PI*i)/2f+0.3f, 0.7f, 0.4f);
        else
        {
            image.color = new Color(0.4f,0.4f,0.4f, Mathf.Sin(Mathf.PI*i)/2f*0.6f);
        }
if(background1)
        image.material.mainTextureOffset = new Vector2(800.0f/256.0f-i, 600.0f/256.0f+i);
        else
        {
                    image.material.mainTextureOffset = new Vector2(800.0f/256.0f+i, 600.0f/256.0f+i);

        }
        
		
    }
}
