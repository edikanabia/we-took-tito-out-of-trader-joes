using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class Items : MonoBehaviour, IPointerDownHandler, IPointerClickHandler,
    IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
    
{
    GameManager gameManager;
    PlayerController tito;

    public int itemNumber;
    public string itemName;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        tito = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        

        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        gameManager.UnlistItem(itemNumber);
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Mouse Down: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exit");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Mouse Up");
    }




}
