using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{

    //Inventory management
    public List<string> titoItems;
    public int maxInventory = 4;
    public int itemsCount = 0;
    public int groceries = 0;
    public int treats = 0;

    //advancing dialogue
    public TMP_Text dialogue;
    public TMP_Text nametag;
    public string currentText;
    Image textbox;
    Image namebox;
    public Image portrait;
    public GameObject dialogueBoxObj;
    public bool speaking; //is speaking currently, deactivates on textbox finish
    public bool spoken; //has already spoken, deactivates on collision exits

    public TextAsset lines;

    public KeyCode advanceText;

    //lists
    [SerializeField] List<GameObject> slots;
    public List<GameObject> groundItems;
    public List<Image> slotImages;
    public List<Items> itemSlots; //every item script on the image slots

    //Tito's information
    PlayerController tito;

    public bool gameEnd = false;

    // Start is called before the first frame update
    void Start()
    {
        //shoppingList = GameObject.Find("shopping list").GetComponent<TMP_Text>();
        dialogue = GameObject.Find("dialogue").GetComponent<TMP_Text>();
        tito = GameObject.Find("Player").GetComponent<PlayerController>();

        for (int i = 1; i < 5; i++)
        {
            slots.Add(GameObject.Find("Slot" + i));
            
        }

        for (int i = 0; i < 4; i++)
        {
            slotImages.Add(slots[i].GetComponent<Image>());
        }
        for (int i = 0; i < 4; i++)
        {
            slotImages[i].enabled = false;
        }

        for (int i = 0; i < 4; i++)
        {
            itemSlots.Add(slots[i].GetComponent<Items>());
        }

        groundItems.AddRange(GameObject.FindGameObjectsWithTag("Grocery"));
        groundItems.AddRange(GameObject.FindGameObjectsWithTag("Treat"));

        
        dialogueBoxObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnd)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(0);
            }
        }
 
        
    }

    public void ListItem()
    {
        slotImages[itemsCount].enabled = true;
        slotImages[itemsCount].preserveAspect = true;
        itemSlots[itemsCount].itemName = titoItems[itemsCount];
    }

    public void UnlistItem(int itemIndex)
    {

        /*
         * we get the number of the item clicked, corresponding to an index
         * in Tito Items AND in Slot Images. We REMOVE that index. If there 
         * is something in the index above the one we just removed, we set 
         * the info from that one into the one we're removing. Otherwise,
         * we deactivate the Slot and reactivate the gameobject in ground
         * items whose name matches the one in the item we clicked on.
        */

        int nextIndex = itemIndex + 1;

        foreach (GameObject item in groundItems)
        {
            if (item.name == itemSlots[itemIndex].itemName)
            {
                item.SetActive(true);
                if (item.CompareTag("Grocery"))
                {
                    groceries--;
                }
                else if (item.CompareTag("Treat"))
                {
                    treats--;
                }
            }
        }

        //find gameobject by name and reactivate

        if (itemIndex != maxInventory - 1) //"3" is max inventory - 1
        {
            while (nextIndex < maxInventory && slotImages[nextIndex] != null)
            {
                slotImages[nextIndex - 1].sprite = slotImages[nextIndex].sprite; //UI elements shift
                itemSlots[nextIndex - 1].itemName = itemSlots[nextIndex].itemName; //name shifts slots

                if (nextIndex < titoItems.Count - 1) //checks to see if tito items index exists
                {
                    titoItems[nextIndex - 1] = titoItems[nextIndex]; //shifts over name in tito items list
                }

                nextIndex++;

            }

        }
        
        itemSlots[itemsCount - 1].itemName = null; //blanks out name
        slotImages[itemsCount - 1].enabled = false; //empties last slot
        titoItems.RemoveAt(titoItems.Count - 1);

        itemsCount--;

    }

    public void SayDialogue()
    {
        speaking = true;
        int currentLine = 0;
        string[] dialogueLines;
        dialogueLines = lines.text.Split(" /n", System.StringSplitOptions.None);
        dialogue.text = dialogueLines[currentLine];
        dialogueBoxObj.SetActive(true); //activates the dialogue box

        //while (currentLine < dialogueLines.Length)
        //{
        //    if (Input.GetKeyDown(advanceText))
        //    {
        //        currentLine++;
        //    }
        //}

        //speaking = false;
        //spoken = true;



    }

}
