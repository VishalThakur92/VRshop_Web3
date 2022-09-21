using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ProductInteractionCanvas : MonoBehaviour,IInteractable
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerEnter()
    {
    }
    public void OnPointerExit()
    {
        DisableAfterSeconds(3);
    }
    public void OnPointerClick ()
    {
    }


    async void DisableAfterSeconds(int seconds) {
        await Task.Delay(seconds * 1000);
        gameObject.SetActive(false);
    }
}
