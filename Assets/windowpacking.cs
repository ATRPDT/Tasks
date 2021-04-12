using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

// Creates a custom Label on the inspector for all the scripts named ScriptName
// Make sure you have a ScriptName script in your
// project, else this will not work.
[CustomEditor(typeof(windowpacking))]
public class TestOnInspector : Editor
{
    public new void OnInspectorGUI()
    {
        GUILayout.Label("This is a Label in a Custom Editor");
    }
}

public class windowpacking : MonoBehaviour
{
    // Start is called before the first frame update
    public float boardWidth, boardHeight;
    public float boardPaddingX=10, boardPaddingY=10;
    public int n;
    public float minSpacing=5;
    private float minSquare = float.MaxValue;
    private Vector2 config = new Vector2();
    private float w = 0;
    private float h = 0;
    void Start()
    {
        WindowPacking();
    }
   
    void Update()
    { 
       

    }

    void GUIDrawRectangle(float xpos, float ypos, float width, float height)
    {
        GameObject NewObj = new GameObject("ImageCh");
        NewObj.transform.SetParent(this.transform);
        Image NewImage = NewObj.AddComponent<Image>();
        NewImage.color = Random.ColorHSV();
        NewObj.GetComponent<RectTransform>().sizeDelta = new Vector2(width, height);
        NewObj.GetComponent<RectTransform>().position = new Vector3(xpos, ypos) + gameObject.GetComponent<RectTransform>().position;
    }
    void WindowPacking()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(boardWidth, boardHeight);
        
        for (int hori = 0; hori < n; hori++)
        {
            for (int vert = n  - 1; vert >= 0; vert--)
            {
                
                if ((vert + 1) * (hori + 1) < n)
                {
                    continue;
                }
                else
                {
                    Debug.Log(new Vector2(hori, vert));
                    if ((vert * (boardHeight - boardPaddingY * 2) * minSpacing) + (hori * (boardWidth - boardPaddingX * 2) * minSpacing) - (vert * hori * minSpacing * minSpacing) + ((vert + 1) * (hori + 1) - n) * h * w < minSquare)
                    {
                        minSquare = (vert * (boardHeight - boardPaddingY * 2) * minSpacing) + (hori * (boardWidth - boardPaddingX * 2) * minSpacing) - (vert * hori * minSpacing * minSpacing) + ((vert + 1) * (hori + 1) - n) * h * w;
                        config.x = hori;
                        config.y = vert;
                        h = (boardHeight - boardPaddingY * 2 - minSpacing * hori) / (hori + 1);
                        w = (boardWidth - boardPaddingX * 2 - minSpacing * vert) / (vert + 1);
                        Debug.Log(config);
                        Debug.Log((vert * (boardHeight - boardPaddingY * 2) * minSpacing) + (hori * (boardWidth - boardPaddingX * 2) * minSpacing) - (vert * hori * minSpacing * minSpacing) + ((vert + 1) * (hori + 1) - n) * h * w);
                    }
                }
            }
        }

        Debug.Log(config);

        Debug.Log(new Vector2(h, w));
        Vector2 leftUpCorner = new Vector2(-boardWidth / 2 + boardPaddingX + w / 2, boardHeight / 2 - boardPaddingY - h / 2);
        var count = 0;
        for (int i = 0; i < config.x + 1; i++)
        {
            if (count == n) break;
            for (int j = 0; j < config.y + 1; j++)
            {
                //if(count == n) break;
                GUIDrawRectangle(leftUpCorner.x + j * (w + minSpacing), leftUpCorner.y - i * ( h + minSpacing), w, h);
                count++;
            }
        }
    }
}
    
