using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState {START, PLAYER1, PLAYER2, PLAYER3, PLAYER4, WARDEN, TRADE, ESCAPED, PRISONERSWON, WARDENWON}

public class TurnSystem : MonoBehaviour
{
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

    public Text currentPlayer;
    public Text movesLeft;

    // Start is called before the first frame update
    void Start()
    {
        wardenCam.GetComponent<ActiveCam>().toggleOff();
        redCam.GetComponent<ActiveCam>().toggleOff();
        yellowCam.GetComponent<ActiveCam>().toggleOff();
        greenCam.GetComponent<ActiveCam>().toggleOff();
        blueCam.GetComponent<ActiveCam>().toggleOff();

        state = BattleState.START;
        StartCoroutine(setupRound());
    }

    IEnumerator setupRound() {

        wardenCam.GetComponent<ActiveCam>().toggleCam();

        GameObject playerGO1 = Instantiate (p1, spawn1);
        unit1 = playerGO1.GetComponent<Unit>();

        GameObject playerGO2 = Instantiate (p2, spawn2);
        unit2 = playerGO2.GetComponent<Unit>();
        
        GameObject playerGO3 = Instantiate (p3, spawn3);
        unit3 = playerGO3.GetComponent<Unit>();

        GameObject playerGO4 = Instantiate (p4, spawn4);
        unit4 = playerGO4.GetComponent<Unit>();

        GameObject playerGOw = Instantiate (w, spawnw);
        unitw = playerGOw.GetComponent<Unit>();

        currentPlayer.text = "Current Player: " + unitw.playerName;
        movesLeft.text = "" + unit1.movesLeft;

        yield return new WaitForSeconds(3f);

        wardenCam.GetComponent<ActiveCam>().toggleCam();
        
        state = BattleState.PLAYER1;
        playerTurn1();
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
        stateChange();
    }

    void playerTurn1() {

        redCam.GetComponent<ActiveCam>().toggleCam();

        currentPlayer.text = "Current Player: " + unit1.playerName;
        movesLeft.text = "" + unit1.movesLeft;

        StartCoroutine(playerMove(unit1));
    }

    void playerTurn2() {

        yellowCam.GetComponent<ActiveCam>().toggleCam();

        currentPlayer.text = "Current Player: " + unit2.playerName;
        movesLeft.text = "" + unit2.movesLeft;

        StartCoroutine(playerMove(unit2));
    }

    void playerTurn3() {

        greenCam.GetComponent<ActiveCam>().toggleCam();

        currentPlayer.text = "Current Player: " + unit3.playerName;
        movesLeft.text = "" + unit3.movesLeft;

        StartCoroutine(playerMove(unit3));
    }

    void playerTurn4() {

        blueCam.GetComponent<ActiveCam>().toggleCam();

        currentPlayer.text = "Current Player: " + unit4.playerName;
        movesLeft.text = "" + unit4.movesLeft;

        StartCoroutine(playerMove(unit4));
    }

    void playerTurn5() {

        wardenCam.GetComponent<ActiveCam>().toggleCam();

        currentPlayer.text = "Current Player: " + unitw.playerName;
        movesLeft.text = "" + unitw.movesLeft;

        StartCoroutine(playerMove(unitw));
    }

    void stateChange() {
        if (state == BattleState.PLAYER1) {
            state = BattleState.PLAYER2;
            redCam.GetComponent<ActiveCam>().toggleCam();
            playerTurn2();

        }else if (state == BattleState.PLAYER2) {
            state = BattleState.PLAYER3;
            yellowCam.GetComponent<ActiveCam>().toggleCam(); 
            playerTurn3();

        }else if (state == BattleState.PLAYER3) {
            state = BattleState.PLAYER4;
            greenCam.GetComponent<ActiveCam>().toggleCam();
            playerTurn4();

        }else if (state == BattleState.PLAYER4) {
            state = BattleState.WARDEN;
            blueCam.GetComponent<ActiveCam>().toggleCam();
            playerTurn5();

        }else if (state == BattleState.WARDEN) {
            state = BattleState.PLAYER1;
            wardenCam.GetComponent<ActiveCam>().toggleCam();
            playerTurn1();
        }
    }

}
