using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegionFinder : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas myCanvas;
    private GameObject goal = null;
    public GameObject lastChildObject;
    List<GameObject> regionList = new List<GameObject>();

    void Start()
    {
        ListGen(transform);
        regionList.Reverse();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            foreach (GameObject region in regionList)
            {
                if (IsCursorInsideRegion(region))
                {
                    region.GetComponent<Image>().color = Random.ColorHSV();
                    break;
                }
            }
        }
    }
    private bool IsCursorInsideRegion(GameObject region)
    {
        Vector3 mousePos = region.transform.InverseTransformPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        Rect rect = region.GetComponent<RectTransform>().rect;
        if (mousePos.x >= rect.xMin && mousePos.x <= rect.xMax && mousePos.y >= rect.yMin && mousePos.y <= rect.yMax)
            return true;
        else
            return false;
    }

    private void ListGen(Transform parentTransform)
    {
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            var nextChild = parentTransform.GetChild(i);
            regionList.Add(nextChild.gameObject);
            ListGen(nextChild);
        }
    }
}
