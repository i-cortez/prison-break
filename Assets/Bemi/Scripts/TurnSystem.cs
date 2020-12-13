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
    public BattleState state;

    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;
    public GameObject w;

    public Transform spawn1;
    public Transform spawn2;
    public Transform spawn3;
    public Transform spawn4;
    public Transform spawnw;

    public GameObject wardenCam;
    public GameObject redCam;
    public GameObject yellowCam;
    public GameObject greenCam;
    public GameObject blueCam;

    Unit unit1;
    Unit unit2;
    Unit unit3;
    Unit unit4;
    Unit unitw;

    GameObject playerGO1;
    GameObject playerGO2;
    GameObject playerGO3;
    GameObject playerGO4;
    GameObject playerGOw;

    GameObject[] dragScript;
    public GameObject hideButton;
    public GameObject hideInventory;

    public Text currentPlayer;
    public Text movesLeft;

    // Start is called before the first frame update
    void Start()
    {
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
        playerGO1.GetComponent<InventorySystem>().enabled = false;

        // Clones the object original and returns the clone
        playerGO2 = Instantiate(p2, spawn2);
        unit2 = playerGO2.GetComponent<Unit>();
        playerGO2.GetComponent<InventorySystem>().enabled = false;


        playerGO3 = Instantiate (p3, spawn3);
        unit3 = playerGO3.GetComponent<Unit>();
        playerGO3.GetComponent<InventorySystem>().enabled = false;

        playerGO4 = Instantiate (p4, spawn4);
        unit4 = playerGO4.GetComponent<Unit>();
        playerGO4.GetComponent<InventorySystem>().enabled = false;

        playerGOw = Instantiate (w, spawnw);
        unitw = playerGOw.GetComponent<Unit>();
        playerGO4.GetComponent<InventorySystem>().enabled = false;

        currentPlayer.text = "Current Player: ";
        movesLeft.text = "";

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYER1;
        StartCoroutine((wardenSetupTurn(unitw)));
    }

    IEnumerator wardenSetupTurn(Unit unit){
        
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
        yield return new WaitForSeconds(1f);

        // state = BattleState.PLAYER2;
        // playerTurn2();
        // Trade
        // check win condition
        stateChange(unit);
    }

    void playerTurn1()
    {
        // Activate the camera for player 1
        redCam.GetComponent<ActiveCam>().toggleCam();

        currentPlayer.text = "Current Player: " + unit1.playerName;
        movesLeft.text = "" + unit1.movesLeft;

        // Enable the inventory for player 1
        playerGO1.GetComponent<InventorySystem>().enabled = true;
        StartCoroutine(playerMove(unit1));
    }

    void playerTurn2()
    {
        // Activate the camera for player 2
        yellowCam.GetComponent<ActiveCam>().toggleCam();

        currentPlayer.text = "Current Player: " + unit2.playerName;
        movesLeft.text = "" + unit2.movesLeft;

        // Enable the inventory for player 2
        playerGO2.GetComponent<InventorySystem>().enabled = true;
        StartCoroutine(playerMove(unit2));
    }

    void playerTurn3()
    {
        // Activate the camera for player 3
        greenCam.GetComponent<ActiveCam>().toggleCam();

        currentPlayer.text = "Current Player: " + unit3.playerName;
        movesLeft.text = "" + unit3.movesLeft;

        // Enable the inventory for player 3
        playerGO3.GetComponent<InventorySystem>().enabled = true;
        StartCoroutine(playerMove(unit3));
    }

    void playerTurn4()
    {
        // Activate the camera for player 4
        blueCam.GetComponent<ActiveCam>().toggleCam();

        currentPlayer.text = "Current Player: " + unit4.playerName;
        movesLeft.text = "" + unit4.movesLeft;

        playerGO4.GetComponent<InventorySystem>().enabled = true;
        StartCoroutine(playerMove(unit4));
    }

    void playerTurnWarden()
    {
        // Activate the camera for warden
        wardenCam.GetComponent<ActiveCam>().toggleCam();

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
            playerGO1.GetComponent<InventorySystem>().enabled = false;
            playerTurn2();
        }
        else if (state == BattleState.PLAYER2)
        {
            // Change the state from player 2 to player 3
            state = BattleState.PLAYER3;
            yellowCam.GetComponent<ActiveCam>().toggleCam();
            playerGO2.GetComponent<InventorySystem>().enabled = false;
            playerTurn3();

        }
        else if (state == BattleState.PLAYER3)
        {
            // Change the state from player 3 to player 4
            state = BattleState.PLAYER4;
            greenCam.GetComponent<ActiveCam>().toggleCam();
            playerGO3.GetComponent<InventorySystem>().enabled = false;
            playerTurn4();

        }
        else if (state == BattleState.PLAYER4)
        {
            // Change the state from player 4 to warden
            state = BattleState.WARDEN;
            blueCam.GetComponent<ActiveCam>().toggleCam();
            playerGO4.GetComponent<InventorySystem>().enabled = false;
            playerTurnWarden();

        }
        else if (state == BattleState.WARDEN)
        {
            state = BattleState.PLAYER1;
            wardenCam.GetComponent<ActiveCam>().toggleCam();
            playerTurn1();
        }
    }
}
