using UnityEngine;
using UnityEngine.UI;

public class RegionFinder : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas myCanvas;
    private GameObject goal = null;
    void Start()
    {
     
    }

void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Search(transform);
            if (goal != null)
                goal.GetComponent<Image>().color = Random.ColorHSV();
            else
                Debug.LogWarning("Error");
            goal = null;
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
    private void Search(Transform parentTransform)
    {   
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            var nextChild = parentTransform.GetChild(i);
            if (IsCursorInsideRegion(nextChild.gameObject))
            {
                goal = nextChild.gameObject;
            }
            Search(nextChild);
        }
        
    }
}
