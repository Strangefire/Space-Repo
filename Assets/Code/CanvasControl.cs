using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour {
    #region Properties
    public static CanvasControl Global;
    public Transform receivingTransform;
    public UIText receiveItemPrefab;
    public UIText crosshairText;
    [Header("Canvas Faders")]
    public CanvasFader inventoryFader;
    public CanvasFader mainGameFader;
    public CanvasFader deathFader;
    [Header("Inventory")]
    public Transform inventoryItemTransform;
    public Scrollbar inventoryItemScroll;
    int selectedItem;
    public Image inventoryItemPrefab;
    public UIText inventoryText;
    [Header("Death")]
    public UIText deathText;
    #endregion
    public int SelectedItem
    {
        get
        {
            return selectedItem;
        }
        set
        {
            inventoryText.Text(Utilities.PlayerItems[value].Information);
            selectedItem = value;
        }
    }
    private void Awake()
    {
        Global = this;
    }
    private void OnDestroy()
    {
        Global = null;
    }
    public void RefreshInventory()
    {
        inventoryText.Text(string.Empty);
        for (int i = 0; i < inventoryItemTransform.childCount; i++)
        {
            Destroy(inventoryItemTransform.GetChild(i).gameObject, Time.deltaTime);
        }
        for (int i = 0; i < Player.Global.playerInventory.inventoryItems.Count; i++)
        {
            Image nImage = Instantiate(inventoryItemPrefab);
            nImage.transform.SetParent(inventoryItemTransform);
            nImage.transform.localScale = Vector3.one;
        }
    }
    private void Update()
    {
        if(Player.Global.blockMovement)
        {
            if (Player.Global.playerInventory.inventoryItems.Count == 0)
            {
                InventoryOff();
                return;
            }
            InventoryOn();
            inventoryItemScroll.value += Input.GetAxis("Mouse X") / 8f;
            RectTransform tTrans = inventoryItemTransform.GetComponent<RectTransform>();
            if (tTrans.rect.width < 1f) SelectedItem = 0;
            else
            {
                Vector2[] ranges = new Vector2[Player.Global.playerInventory.inventoryItems.Count];
                for (int i = 0; i < ranges.Length; i++)
                {
                    if (i == 0) ranges[i] = new Vector2(30f, -30f);
                    else ranges[i] = new Vector2(ranges[i - 1].y, ranges[i].x - 60f);
                    if (inventoryItemTransform.localPosition.x > ranges[i].y && inventoryItemTransform.localPosition.x < ranges[i].x)
                    {
                        SelectedItem = i;
                        break;
                    }
                }
            }
            if(KeepInput.KeyDown("Use Item"))
            {
                Player.Global.UseItem(Utilities.PlayerItems[SelectedItem]);
                RefreshInventory();
            }
        }
        else InventoryOff();
    }
    void InventoryOn()
    {
        inventoryFader.Toggle(true);
        mainGameFader.Toggle(false);
    }
    void InventoryOff()
    {
        inventoryFader.Toggle(false);
        mainGameFader.Toggle(true);
    }
    //Item Side Notifications
    UIText SpawnReceiveTextPrefab()
    {
        UIText theText = Instantiate(Global.receiveItemPrefab);
        theText.transform.SetParent(Global.receivingTransform);
        theText.transform.localScale = Vector3.one;
        Destroy(theText.gameObject, 2f);
        return theText;
    }
    public static void ReceivedItem(Item itemRef)
    {
        Global.SpawnReceiveTextPrefab().Text(string.Format("Received {0}", itemRef.itemName));
    }
    public static void ConsumedItem(Item itemRef)
    {
        Global.SpawnReceiveTextPrefab().Text(string.Format("Consumed {0}", itemRef.itemName));
    }
}
