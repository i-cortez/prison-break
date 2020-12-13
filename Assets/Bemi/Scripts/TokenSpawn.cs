using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenSpawn : MonoBehaviour
{

    public GameObject redToken;
    public GameObject yellowToken;
    public GameObject greenToken;
    public GameObject blueToken;


    public void spawnToken(string color, GameObject pos){

        // GameObject tempToken;

        switch (color) {
            case "Red Token":
                // tempToken = redToken;
                // tempToken.gameObject.tag = "Tokens To Hide";
                redToken.gameObject.tag = "Tokens To Hide";
                Instantiate (redToken, pos.transform);
                break;

            case "Yellow Token":
                // tempToken = yellowToken;
                // tempToken.gameObject.tag = "Tokens To Hide";
                yellowToken.gameObject.tag = "Tokens To Hide";
                Instantiate (yellowToken, pos.transform);
                break;

            case "Green Token":
                // tempToken = greenToken;
                // tempToken.gameObject.tag = "Tokens To Hide";
                greenToken.gameObject.tag = "Tokens To Hide";
                Instantiate (greenToken, pos.transform);
                break;

            case "Blue Token":
                // tempToken = blueToken;
                // tempToken.gameObject.tag = "Tokens To Hide";
                blueToken.gameObject.tag = "Tokens To Hide";
                Instantiate (blueToken, pos.transform);
                break;
        }
    }

    public void HideButtonClick() {
        GameObject[] taggedTokens;
        taggedTokens = GameObject.FindGameObjectsWithTag("Tokens To Hide");

        foreach(GameObject token in taggedTokens) {
            token.gameObject.tag = "Token";
            token.GetComponent<Renderer>().enabled = false;
        }

        TurnSystem.tokensHidden = true;
    }
}
