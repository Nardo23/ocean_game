using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterColor : MonoBehaviour
{

    public Texture2D waterTileTexture;
    public Color oldColor;
    public Color regionColor;
    public Player playerScript;
    Vector3 tempColorV3;

    public float lerpTime;


    private void Start()
    {
        setColor(oldColor);
    }

    void setColor(Color col)
    {
    int y = 0;
        while (y < waterTileTexture.height)
        {
            int x = 0;
            while (x < waterTileTexture.width)
            {
                waterTileTexture.SetPixel(x, y, col);         
                ++x;
            }
            ++y;
        }
        waterTileTexture.Apply();
    }

    IEnumerator LerpColor(Vector3 targetColorVector, float duration)
    {
        float time = 0;
        Color colSample = waterTileTexture.GetPixel(4, 4);
        Vector3 Startcolor = new Vector3(colSample.r, colSample.g, colSample.b);

        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (3f - 2f * t);
            tempColorV3 = Vector3.Lerp(Startcolor, targetColorVector, t);
            setColor(new Color(tempColorV3.x,tempColorV3.y, tempColorV3.z, 255f));

            time += Time.deltaTime;
            //Debug.Log(time);
            yield return null;
        }
        tempColorV3 = targetColorVector;
        setColor(new Color(tempColorV3.x, tempColorV3.y, tempColorV3.z, 255f));

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!playerScript.inUnderworld)
            {
                StartCoroutine(LerpColor(new Vector3(regionColor.r, regionColor.g, regionColor.b), lerpTime));
            }
            
        }
    }
}
