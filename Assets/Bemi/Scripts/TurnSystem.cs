using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState {START, WARDENSETUP, PLAYER1, PLAYER2, PLAYER3, PLAYER4, WARDEN, TRADE, ESCAPED, PRISONERSWON, WARDENWON}
   

public class TurnSystem : MonoBehaviour
{
    public static int tokensLeft = 24;
    public static bool tokensHidden = false;
    public static bool moved = false;
    public static bool trade = false;

    //public static int sus = 0;
    public BattleState state;
    [Space]
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    public GameObject w;
    [Space]
    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    public Transform spawn4;
    public Transform spawnw;
    [Space]
    public GameObject wardenCam;
    public GameObject redCam;
    public GameObject yellowCam;
    public GameObject greenCam;
    public GameObject blueCam;
    [Space]
    public GameObject cell1;
    public GameObject cell2;
    public GameObject cell3;
    public GameObject cell4; 

    Unit unit1;
    Unit unit2;
    Unit unit3;
    Unit unit4;
    Unit unitw;
    Unit currentUnit;

    Inventory inv1;
    Inventory inv2;
    Inventory inv3;
    Inventory inv4;
    Inventory invw;

    GameObject playerGO1;
    GameObject playerGO2;
    GameObject playerGO3;
    GameObject playerGO4;
    GameObject playerGOw;

    GameObject[] dragScript;

    [Space]
    public GameObject hideButton;
    public GameObject hideInventory;

    public Text currentPlayer;
    public Text movesLeft;
    public Slider sus;
    //public bool wardenSus = false;

    // Start is called before the first frame update
    void Start()
    {
        //sus = 
        hideButton = GameObject.Find("Hide Tokens Button");
        hideButton.SetActive(false); //hide "hide" button

        dragScript = GameObject.FindGameObjectsWithTag("Tokens");
        foreach(GameObject script in dragScript) {
            script.GetComponent<ItemDrag>().enabled = false;
        }

        wardenCam.GetComponent<ActiveCam>().toggleOff();
        redCam.GetComponent<ActiveCam>().toggleOff();
        yellowCam.GetComponent<ActiveCam>().toggleOff();
        greenCam.GetComponent<ActiveCam>().toggleOff();
        blueCam.GetComponent<ActiveCam>().toggleOff();

        state = BattleState.START;
        StartCoroutine(setupRound());
    }

    IEnumerator setupRound()
    {
        // Activate the Warden's camera (birds eye view)
        wardenCam.GetComponent<ActiveCam>().toggleCam();

        // Clones the object original and returns the clone
        playerGO1 = Instantiate (p1, spawn1);
        unit1 = playerGO1.GetComponent<Unit>();
        inv1 = playerGO1.GetComponent<Inventory>();
        //playerGO1.GetComponent<InventorySystem>().enabled = false;
        // Transform temp = GameObject.Find("P1 Slot (1)").GetComponent<Transform>();
        // Instantiate(redToken, temp);

        // Clones the object original and returns the clone
        playerGO2 = Instantiate(p2, spawn2);
        unit2 = playerGO2.GetComponent<Unit>();
        inv2 = playerGO2.GetComponent<Inventory>();
        //playerGO2.GetComponent<InventorySystem>().enabled = false;

        playerGO3 = Instantiate (p3, spawn3);
        unit3 = playerGO3.GetComponent<Unit>();
        inv3 = playerGO3.GetComponent<Inventory>();
        //playerGO3.GetComponent<InventorySystem>().enabled = false;

        playerGO4 = Instantiate (p4, spawn4);
        unit4 = playerGO4.GetComponent<Unit>();
        inv4 = playerGO4.GetComponent<Inventory>();
        //playerGO4.GetComponent<InventorySystem>().enabled = false;

        playerGOw = Instantiate (w, spawnw);
        unitw = playerGOw.GetComponent<Unit>();
        invw = playerGO4.GetComponent<Inventory>();
        //playerGO4.GetComponent<InventorySystem>().enabled = false;

        currentPlayer.text = "Current Player: ";
        movesLeft.text = "";

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYER1;
        StartCoroutine((wardenSetupTurn(unitw)));
    }

    IEnumerator wardenSetupTurn(Unit unit){
        
        trade = true;
        
        currentPlayer.text = "Current Player: " + unitw.playerName;
        wardenTokenCounter();

        foreach(GameObject script in dragScript) {
            script.GetComponent<ItemDrag>().enabled = true;
        }

        yield return new WaitUntil(() => TurnSystem.tokensLeft == 0);

        movesLeft.text = "" + TurnSystem.tokensLeft;

        if (TurnSystem.tokensLeft == 0) {
            print("DONE PLACING TOKENS");
            hideButton.SetActive(true);

            foreach(GameObject script in dragScript) {
                script.GetComponent<ItemDrag>().enabled = true;
            }
        }

        yield return new WaitUntil(() => TurnSystem.tokensHidden == true);
        yield return new WaitForSeconds(.5f);

        hideInventory = GameObject.Find("Warden Inventory");
        hideInventory.SetActive(false);
        hideButton.SetActive(false);

        yield return new WaitForSeconds(1f);

        trade = false;
        wardenCam.GetComponent<ActiveCam>().toggleCam();
        playerTurn1();
    }

    public void wardenTokenCounter() {
        movesLeft.text = "" + TurnSystem.tokensLeft;
    }

    IEnumerator playerMove(Unit unit) {
        
        while (unit.movesLeft > 0) {
            unit.movePlayer(unit);
            yield return new WaitUntil(() => TurnSystem.moved == true);
            // print("Return Check");
            // print(unit.playerName + " " + unit.movesLeft);

            TurnSystem.moved = false;

            movesLeft.text = "" + unit.movesLeft;
        }

        //unit.movesLeft--;
        // movesLeft.text = "" + unit.movesLeft;
        yield return new WaitUntil(() => Input.GetKeyDown("q"));

        // state = BattleState.PLAYER2;
        // playerTurn2();
        // Trade
        // check win condition
        //StartCoroutine(tradeTurn(unit));
        stateChange(unit);
    }

    // IEnumerator tradeTurn(Unit unit) {

    //     TurnSystem.trade = true;


    //     yield return new WaitUntil(() => (Input.GetKeyDown("q")) == true);

    //     TurnSystem.trade = false;
    //     stateChange(unit);
    // }

    void playerTurn1()
    {
        // Activate the camera for player 1
        redCam.GetComponent<ActiveCam>().toggleCam();
        
        currentPlayer.text = "Current Player: " + unit1.playerName;
        movesLeft.text = "" + unit1.movesLeft;

        currentUnit = unit1;

        // Enable the inventory for player 1
        //playerGO1.GetComponent<InventorySystem>().enabled = true;
        StartCoroutine(playerMove(unit1));
    }

    void playerTurn2()
    {
        // Activate the camera for player 2
        yellowCam.GetComponent<ActiveCam>().toggleCam();

        currentPlayer.text = "Current Player: " + unit2.playerName;
        movesLeft.text = "" + unit2.movesLeft;

        currentUnit = unit2;

        // Enable the inventory for player 2
        //playerGO2.GetComponent<InventorySystem>().enabled = true;
        StartCoroutine(playerMove(unit2));
    }

    void playerTurn3()
    {
        // Activate the camera for player 3
        greenCam.GetComponent<ActiveCam>().toggleCam();

        currentPlayer.text = "Current Player: " + unit3.playerName;
        movesLeft.text = "" + unit3.movesLeft;

        currentUnit = unit3;

        // Enable the inventory for player 3
        //playerGO3.GetComponent<InventorySystem>().enabled = true;
        StartCoroutine(playerMove(unit3));
    }

    void playerTurn4()
    {
        // Activate the camera for player 4
        blueCam.GetComponent<ActiveCam>().toggleCam();

        currentPlayer.text = "Current Player: " + unit4.playerName;
        movesLeft.text = "" + unit4.movesLeft;

        currentUnit = unit4;

        //playerGO4.GetComponent<InventorySystem>().enabled = true;
        StartCoroutine(playerMove(unit4));
    }

    void playerTurnWarden()
    {
        // Activate the camera for warden
        wardenCam.GetComponent<ActiveCam>().toggleCam();

        currentUnit = unitw;
        trade = false;
        

        currentPlayer.text = "Current Player: " + unitw.playerName;
        movesLeft.text = "" + unitw.movesLeft;

        StartCoroutine(playerMove(unitw));
    }

    void stateChange(Unit myUnit)
    {
        // Reset the number of moves for the player
        myUnit.movesLeft = 2;

        if (state == BattleState.PLAYER1)
        {
            // Change the state from player 1 to player 2
            state = BattleState.PLAYER2;
            redCam.GetComponent<ActiveCam>().toggleCam();
            //playerGO1.GetComponent<InventorySystem>().enabled = false;
            playerTurn2();
        }
        else if (state == BattleState.PLAYER2)
        {
            // Change the state from player 2 to player 3
            state = BattleState.PLAYER3;
            yellowCam.GetComponent<ActiveCam>().toggleCam();
            //playerGO2.GetComponent<InventorySystem>().enabled = false;
            playerTurn3();

        }
        else if (state == BattleState.PLAYER3)
        {
            // Change the state from player 3 to player 4
            state = BattleState.PLAYER4;
            greenCam.GetComponent<ActiveCam>().toggleCam();
            //playerGO3.GetComponent<InventorySystem>().enabled = false;
            playerTurn4();;

        }
        else if (state == BattleState.PLAYER4)
        {
            // Change the state from player 4 to warden
            state = BattleState.WARDEN;
            blueCam.GetComponent<ActiveCam>().toggleCam();
            //playerGO4.GetComponent<InventorySystem>().enabled = false;
            playerTurnWarden();;

        }
        else if (state == BattleState.WARDEN)
        {
            state = BattleState.PLAYER1;
            wardenCam.GetComponent<ActiveCam>().toggleCam();
            trade = false;
            playerTurn1();;
        }
    }

    void Update() {
        
        if (Input.GetKeyDown(KeyCode.E)) {
            if (currentUnit.movesLeft == 2) {
                currentUnit.movesLeft = 3;
            }else if (currentUnit.movesLeft == 3) {
                currentUnit.movesLeft = 2;
        }

            movesLeft.text = "" + currentUnit.movesLeft;
        }

        if (Input.GetKeyDown(KeyCode.Z)) {
            if (sus.value > 0) {
                sus.value -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.X)) {
            if (sus.value < 12) {
                sus.value += 1;
            }
        }

        if (trade == true) {
            if (Input.GetKeyDown(KeyCode.S)) {
                sus4(unitw);
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            cell2.GetComponent<MeshRenderer>().enabled = !cell2.GetComponent<MeshRenderer>().enabled;
            cell3.GetComponent<MeshRenderer>().enabled = !cell3.GetComponent<MeshRenderer>().enabled;
            cell4.GetComponent<MeshRenderer>().enabled = !cell4.GetComponent<MeshRenderer>().enabled;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cell1.GetComponent<MeshRenderer>().enabled = !cell1.GetComponent<MeshRenderer>().enabled;
            cell3.GetComponent<MeshRenderer>().enabled = !cell3.GetComponent<MeshRenderer>().enabled;
            cell4.GetComponent<MeshRenderer>().enabled = !cell4.GetComponent<MeshRenderer>().enabled;

        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cell1.GetComponent<MeshRenderer>().enabled = !cell1.GetComponent<MeshRenderer>().enabled;
            cell2.GetComponent<MeshRenderer>().enabled = !cell2.GetComponent<MeshRenderer>().enabled;
            cell4.GetComponent<MeshRenderer>().enabled = !cell4.GetComponent<MeshRenderer>().enabled;

        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            cell1.GetComponent<MeshRenderer>().enabled = !cell1.GetComponent<MeshRenderer>().enabled;
            cell2.GetComponent<MeshRenderer>().enabled = !cell2.GetComponent<MeshRenderer>().enabled;
            cell3.GetComponent<MeshRenderer>().enabled = !cell3.GetComponent<MeshRenderer>().enabled;

        }

    }
    // public void toggleMovesLeft(Unit unit) {
    //     print("CHECK!!!!");
    //     if (unit.movesLeft == 2) {
    //         unit.movesLeft = 3;
    //     }else if (unit.movesLeft == 3) {
    //         unit.movesLeft = 2;
    //     }
    //     print(unit.movesLeft);
    // }
        void sus4(Unit unit){
            trade = true;
        }

    void OnGUI()
    {
        // Inventory UI
        GUI.Label(new Rect(5, 225, 200, 25), "Press 'I' to toggle Inventory");
        // Next turn
        GUI.Label(new Rect(5, 250, 200, 25), "Press 'Q' to finish turn");
        // Inventory UI
        GUI.Label(new Rect(5, 275, 200, 25), "Press 'E' to toggle movement");
        // Inventory UI
        GUI.Label(new Rect(5, 300, 300, 25), "Press '1' to toggle Prisoner 1 Isolation");
        // Inventory UI
        GUI.Label(new Rect(5, 325, 300, 25), "Press '2' to toggle Prisoner 2 Isolation");
        // Inventory UI
        GUI.Label(new Rect(5, 350, 300, 25), "Press '3' to toggle Prisoner 3 Isolation");
        // Inventory UI
        GUI.Label(new Rect(5, 375, 300, 25), "Press '4' to toggle Prisoner 4 Isolation");
    }

}
