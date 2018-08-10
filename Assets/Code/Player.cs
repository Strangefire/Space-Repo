using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IDamageable
{
    #region Properties
    public bool blockMovement;
    bool isGrounded;
    Coroutine pickRoutine;
    Pickup currentPickup;
    public static Player Global;
    Vector2 health = new Vector2(50f, 50f);
    Vector2 hunger = new Vector2(0f, 50f);
    Vector2 thirst = new Vector2(0f, 50f);
    Vector2 energy = new Vector2(50f, 50f);
    Quaternion carryHandRotation;
    public float movementSpeed = 5f;
    public bool isDead;
    public float increaseHungerPerSecond = 0.1f;
    public float increaseThirstPerSecond = 0.15f;
    public float drainEnergyPerSecond = 0.25f;
    public Inventory playerInventory;
    private Interactable carryInteractable;
    float horizontalInput;
    float verticalInput;
    bool jump;
    public float jumpForce = 50f;
    public Transform carryPoint;
    public float lookRange = 10f;
    float mouseyAxis;
    float mousexAxis;
    Rigidbody myBody;
    Camera myCamera;
    public UIText crosshairText;
    #endregion
    #region Properties Set Get
    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
        set
        {
            if (value && !isGrounded)
            {
                //Land
                myCamera.transform.localPosition -= Vector3.up * 0.2f;
            }
            isGrounded = value;
        }
    }
    public Pickup CurrentPickup
    {
        get { return currentPickup; }
        set
        {
            if (pickRoutine != null) StopCoroutine(pickRoutine);
            if(currentPickup != null)
            {
                currentPickup.TogglePickup(true);
                currentPickup.transform.SetParent(null);
            }
            currentPickup = value;
            if(value != null)
            {
                pickRoutine = StartCoroutine(PickupRoutine(value));
                value.TogglePickup(false);
                value.transform.SetParent(carryPoint);
            }
        }
    }
    public float CurrentHealth
    {
        get { return health.x; }
        set
        {
            if (isDead) return;
            health.x = Mathf.Clamp(value, 0f, MaxHealth);
            if (value <= 0f) Death();
        }
    }
    public float MaxHealth
    {
        get { return health.y; }
        set
        {
            value = Mathf.Clamp(value, 0f, float.MaxValue);
            if (value < CurrentHealth) value = CurrentHealth;
            health.y = value;
        }
    }
    public float CurrentHunger
    {
        get { return hunger.x; }
        set
        {
            hunger.x = Mathf.Clamp(value, 0f, 50f);
            if (value <= 0f) Death();
        }
    }
    public float MaxHunger
    {
        get { return hunger.y; }
        set
        {
            value = Mathf.Clamp(value, 0f, float.MaxValue);
            if (value < CurrentHunger) value = CurrentHunger;
            hunger.y = value;
        }
    }
    public float CurrentThirst
    {
        get { return thirst.x; }
        set
        {
            thirst.x = Mathf.Clamp(value, 0f, 50f);
            if (value <= 0f) Death();
        }
    }
    public float MaxThirst
    {
        get { return thirst.y; }
        set
        {
            value = Mathf.Clamp(value, 0f, float.MaxValue);
            if (value < CurrentThirst) value = CurrentThirst;
            thirst.y = value;
        }
    }
    public float CurrentEnergy
    {
        get { return energy.x; }
        set
        {
            energy.x = Mathf.Clamp(value, 0f, 50f);
            if (value <= 0f) Death();
        }
    }
    public float MaxEnergy
    {
        get { return energy.y; }
        set
        {
            value = Mathf.Clamp(value, 0f, float.MaxValue);
            if (value < CurrentEnergy) value = CurrentEnergy;
            energy.y = value;
        }
    }
    #endregion
    #region IDamageable
    public void Damage(DamagePack dPack)
    {
        if (isDead) return;
        CurrentHealth -= dPack.damageAmount;
        Debug.Log(string.Format("<color=yellow>[{0}]</color> Player Received <color=red>{1}</color> damage", Time.time.ToString("F1"), dPack.damageAmount));
    }
    public void Death()
    {
        if (isDead) return;
        isDead = true;
        Debug.Log(string.Format("<color=yellow>[{0}]</color> Player Died", Time.time.ToString("F1")));
    }
    #endregion
    #region Updates
    void InventoryUpdate()
    {
        if(KeepInput.KeyDown("Choose Item"))
        {
            CanvasControl.Global.RefreshInventory();
        }
        if(KeepInput.KeyUp("Choose Item"))
        {
            if (playerInventory.inventoryItems.Count > 0) TakeItemToHand(playerInventory.inventoryItems[CanvasControl.Global.SelectedItem]);
        }
        blockMovement = playerInventory.inventoryItems.Count > 0 && KeepInput.Key("Choose Item");
    }
    void CameraUpdate()
    {
        while(blockMovement) { blockMovement = true; }
        //Naujas komentaras  2
        //Papildomas komentaras
        if(!blockMovement)
        {
            GameObject lookingAtObject = null;
            RaycastHit hit;
            Ray ray = myCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (Physics.Raycast(ray, out hit, lookRange)) lookingAtObject = hit.collider.gameObject;
            if (CurrentPickup != null)
            {
                if (KeepInput.KeyDown("Pick Item"))
                {
                    PickupPickup(CurrentPickup);
                    CurrentPickup = null;
                }
                if(KeepInput.KeyDown("Use Item"))
                {
                    CanvasControl.ConsumedItem(CurrentPickup.pickupItem);
                    if (CurrentPickup.pickupItem.UseItem())
                    {
                        Destroy(CurrentPickup.gameObject, Time.deltaTime);
                    }
                }
                if (KeepInput.KeyDown("Throw")) ThrowCarryItem();
            }
            if (lookingAtObject != null)
            {
                Interactable findInteractable = lookingAtObject.GetComponent<Interactable>();
                if (findInteractable)
                {
                    string information = string.Empty;
                    if (findInteractable.canBeUsed)
                    {
                        information = KeepInput.ReturnKeyName("Interact") + " Interact";
                        if (KeepInput.KeyDown("Interact"))
                        {
                            findInteractable.Use();
                            return;
                        }
                    }
                    if (findInteractable is Pickup)
                    {
                        Pickup pickupReference = findInteractable as Pickup;
                        if (pickupReference.takeToInventory)
                        {
                            information += string.Format("\n{0} Take {1}", KeepInput.ReturnKeyName("Interact"), pickupReference.pickupItem.itemName);
                            if (KeepInput.KeyDown("Interact")) PickupPickup(pickupReference);
                        }
                        information += string.Format("\n{0} Carry {1}", KeepInput.ReturnKeyName("Pick Item"), pickupReference.pickupItem.itemName);
                        if (KeepInput.KeyDown("Pick Item")) CurrentPickup = pickupReference;
                    }
                    crosshairText.Text(information);
                }
                else crosshairText.Text(string.Empty);
            }
            else crosshairText.Text(string.Empty);
        }
        else crosshairText.Text(string.Empty);
        if((myBody.velocity.x != 0f || myBody.velocity.z != 0f) && IsGrounded) myCamera.transform.localPosition = Vector3.Lerp(Vector3.up * 1.7f, Vector3.up * 1.9f, Mathf.Abs(Mathf.Sin(Time.time*5f)));
        else myCamera.transform.localPosition = Vector3.Lerp(myCamera.transform.localPosition, Vector3.up * 1.8f, Time.deltaTime);
        Quaternion handRotateTarget = carryHandRotation;
        if(!blockMovement)handRotateTarget = Quaternion.Euler(carryHandRotation.eulerAngles + new Vector3(Input.GetAxis("Mouse Y") * 5f, 0f, Input.GetAxis("Mouse X")*5f));
        carryPoint.transform.localRotation = Quaternion.Slerp(carryPoint.transform.localRotation, handRotateTarget, Time.deltaTime * 5f);
    }
    void InputUpdate()
    {
        InventoryUpdate();
        verticalInput = 0f;
        horizontalInput = 0f;
        mouseyAxis = Mathf.Clamp(mouseyAxis, -80f, 80f);
        if (!blockMovement)
        {
            if (KeepInput.Key("Forward")) verticalInput += 1f;
            if (KeepInput.Key("Backwards")) verticalInput -= 1f;
            if (KeepInput.Key("Strafe Left")) horizontalInput -= 1f;
            if (KeepInput.Key("Strafe Right")) horizontalInput += 1f;
            if (KeepInput.KeyDown("Jump") && IsGrounded) jump = true;
            mousexAxis = Input.GetAxis("Mouse X") * Options.mouseSensitivity * Time.deltaTime;
        }
    }
    void Update()
    {
        if(Application.isEditor && Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        DrainVariables();
        CameraUpdate();
        if(!blockMovement)
        {
            if (!Options.invertMouse) mouseyAxis -= Input.GetAxis("Mouse Y") * Options.mouseSensitivity * Time.deltaTime;
            else mouseyAxis += Input.GetAxis("Mouse Y") * Options.mouseSensitivity * Time.deltaTime;
        }
        InputUpdate();
    }
    void FixedUpdate()
    {
        Collider[] collisions = Physics.OverlapSphere(transform.position, 0.2f);
        bool grounded = false;
        for (int i = 0; i < collisions.Length; i++)
        {
            if (collisions[i].gameObject != gameObject)
            {
                grounded = true;
                break;
            }
        }
        IsGrounded = grounded;
        Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput);
        moveDirection = transform.TransformDirection(moveDirection.normalized*( movementSpeed * Time.deltaTime));
        moveDirection.y = myBody.velocity.y;
        if (jump)
        {
            moveDirection.y = jumpForce;
            jump = false;
        }
        myBody.velocity = moveDirection;
    }
    void LateUpdate()
    {
        if (blockMovement) return;
        transform.Rotate(Vector3.up, mousexAxis);
        myCamera.transform.localEulerAngles = new Vector3(mouseyAxis, 0f, 0f);
    }
    void DrainVariables()
    {
        CurrentHunger += increaseHungerPerSecond * Time.deltaTime;
        CurrentThirst += increaseThirstPerSecond * Time.deltaTime;
        CurrentEnergy -= drainEnergyPerSecond * Time.deltaTime;
    }
    #endregion
    #region Inventory
    void PickupPickup(Pickup pickupReference)
    {
        playerInventory.AddItem(pickupReference.pickupItem);
        Destroy(pickupReference.gameObject);
    }
    void TakeItemToHand(Item theItem)
    {
        Pickup thePickup = GameControl.SpawnPickup(theItem,carryPoint.transform.position,carryPoint.transform.rotation);
        CurrentPickup = thePickup;
        playerInventory.RemoveItem(theItem);
    }
    public void UseItem(Item useItem)
    {
        useItem.UseItem();
        if (useItem.consumeOnUse)
        {
            CanvasControl.ConsumedItem(useItem);
            playerInventory.RemoveItem(useItem);
        }
    }
    void ThrowCarryItem()
    {
        if (CurrentPickup != null)
        {
            Pickup tempPickup = CurrentPickup;
            CurrentPickup = null;
            Ray ray = myCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            if (!Physics.Raycast(ray, 1.8f)) tempPickup.myBody.AddForce(myCamera.transform.forward * 25f, ForceMode.Impulse);
        }
    }
    IEnumerator PickupRoutine(Pickup pickupObject)
    {
        float normalTime = 0f;
        while (normalTime < 1f)
        {
            normalTime += Time.deltaTime;
            if (normalTime > 1f) normalTime = 1f;
            pickupObject.transform.localRotation = Quaternion.Slerp(pickupObject.transform.localRotation, Quaternion.identity, normalTime);
            pickupObject.transform.localPosition = Vector3.Lerp(pickupObject.transform.localPosition, Vector3.zero, normalTime);
            yield return new WaitForEndOfFrame();
        }
        pickRoutine = null;
    }
    #endregion
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (playerInventory == null) playerInventory = Instantiate(new Inventory());
        playerInventory.OnItemReceive += CanvasControl.ReceivedItem;
    }
    void Awake()
    {
        Global = this;
        myBody = GetComponent<Rigidbody>();
        myCamera = GetComponentInChildren<Camera>();
        carryHandRotation = carryPoint.transform.localRotation;
        crosshairText = CanvasControl.Global.crosshairText;
    }
    void OnDestroy()
    {
        Global = null;
        playerInventory.OnItemReceive -= CanvasControl.ReceivedItem;
    }
    //Cheats
    public void FullHealth() { CurrentHealth = MaxHealth; }
    public void FullEnergy() { CurrentEnergy = MaxEnergy; }
    public void FullThirst() { CurrentThirst = 0f; }
    public void FullHunger() { CurrentHunger = 0f; }
}
