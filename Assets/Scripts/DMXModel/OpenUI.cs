using UnityEngine;
using UnityEngine.EventSystems;

//this goes onto the object we want to open the UI
//Example the sphere
public class OpenUI : MonoBehaviour, IPointerClickHandler
{
    public GameObject panel;
    private bool isActive;

    public void Awake()
    {
        panel.SetActive(false);
        // isActive = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        panel.SetActive(true);

        /*if (isActive)
        {
            panel.SetActive(false);
            isActive = false;
        }*/

        /*this will not work since the UI is open therefore clicking on the object again at the moment will not work
        however this can come in handy when we need to click on it again later in the future
        */
        
       
        
    }

}
