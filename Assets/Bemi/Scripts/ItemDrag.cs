using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    TokenSpawn spawnScript;
    TurnSystem tokenCounter;

    public Image ghost;
    Vector3 originSave;
    //public ghost.transform.position originSave;
    
    void Awake() {
        spawnScript = GameObject.Find("Game Manager").GetComponent<TokenSpawn>();
        tokenCounter = GameObject.Find("Game Manager").GetComponent<TurnSystem>();

        ghost = GetComponent<Image>();

        ghost.raycastTarget = true;
        // (just in case you forgot to do that in the Editor)
        ghost.enabled = true;
    }
    
    public void OnBeginDrag(PointerEventData eventData) {
        originSave = GetComponent<Image>().transform.position;

        ghost.transform.position = transform.position;
        ghost.enabled = true;
    }

    public void OnDrag(PointerEventData eventData) {
        ghost.transform.position += (Vector3)eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData) {

        print("Success BOIII!");
        Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
            ghost.enabled = false;
            var hitObject = hit.transform.gameObject;
            print("Dropped on " + hitObject.name);
            TurnSystem.tokensLeft--;
            tokenCounter.wardenTokenCounter();

            spawnScript.spawnToken(ghost.name, hitObject);
            

        }else{
            ghost.transform.position = originSave;
        }
    }
    
    // public void OnDrop(PointerEventData data)
    //     {
    //         print("Success BOIII!");
    //         Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
    //         RaycastHit hit;

    //         if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
    //             var hitObject = hit.transform.gameObject;
    //             print("Dropped on " + hitObject.name);
    //         }
    //     }
    }
