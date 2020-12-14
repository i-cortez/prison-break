using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keister : MonoBehaviour
{
    // List with Prefabs of all the available items
    public PickItem[] availableItems;

    // Available items slots
    int[] itemSlots = new int[4];
    bool showKeister = false;
    float windowAnimation = 1;
    float animationTimer = 0;

    // UI Drag & Drop variables
    // (-1) represents an invalid index
    int hoveringOverIndex = -1;
    int itemIndexToDrag = -1;
    Vector2 dragOffset = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Item Slots
        for (int i = 0; i < itemSlots.Length; ++i)
        {
            // Here -1 represents an empty slot
            itemSlots[i] = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Show or Hide Keister UI by pressing the 'K' key
        if (Input.GetKeyDown(KeyCode.K))
        {
            showKeister = !showKeister;
            animationTimer = 0;
        }

        if (animationTimer < 1)
        {
            animationTimer += Time.deltaTime;
        }

        if (showKeister)
        {
            // Open the inventory menu and freeze movement
            windowAnimation = Mathf.Lerp(windowAnimation, 0, animationTimer);
        }

        else
        {
            // Close the inventory menu and allow movement
            windowAnimation = Mathf.Lerp(windowAnimation, 1f, animationTimer);
        }

        // Begin the item drag
        if (Input.GetMouseButtonDown(0) && hoveringOverIndex > -1 && itemSlots[hoveringOverIndex] > -1)
        {
            itemIndexToDrag = hoveringOverIndex;
        }

        // Release the dragged item
        // We could add code here to handle the dragging and dropping into keister or another player's inventory
        if (Input.GetMouseButtonUp(0) && itemIndexToDrag > -1)
        {
            if (hoveringOverIndex < 0)
            {
                // Drop the item outside
                // Instantiate(availableItems[itemSlots[itemIndexToDrag]], playerController.playerCamera.transform.position + (playerController.playerCamera.transform.forward), Quaternion.identity);
                itemSlots[itemIndexToDrag] = -1;
            }

            else
            {
                // Switch items between the selected slot and the one we are hovering on
                int itemIndexTmp = itemSlots[itemIndexToDrag];
                itemSlots[itemIndexToDrag] = itemSlots[hoveringOverIndex];
                itemSlots[hoveringOverIndex] = itemIndexTmp;
            }

            // Clear the index
            itemIndexToDrag = -1;
        }
    }

    void OnGUI()
    {
        // Keister UI message
        GUI.Label(new Rect(5, 30, 200, 25), "Press 'K' to open Keister");

        // Draw the Keister window
        if(windowAnimation < 1)
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 200, 10 - (430 * windowAnimation), 400, 150), GUI.skin.GetStyle("box"));
            GUILayout.Label("Keister", GUILayout.Height(25));

            // Begin a vertical control group.
            // All controls rendered inside this element will be placed vertically below each other
            GUILayout.BeginVertical();
            for(int i = 0; i < itemSlots.Length; i += 4)
            {
                // Begin a horizontal control group
                // All controls rendered inside this element will be placed horizontally next to each other
                GUILayout.BeginHorizontal();

                // For the keister display 4 items in a row
                for(int j = 0; j < 4; ++j)
                {
                    if(i + j < itemSlots.Length)
                    {
                        if(itemIndexToDrag == i + j || (itemIndexToDrag > -1 && hoveringOverIndex == i + j))
                        {
                            // All controls will be draw semi-transparently, and will not respond to user input
                            GUI.enabled = false;
                        }

                        if(itemSlots[i + j] > -1)
                        {
                            if(availableItems[itemSlots[i + j]].itemPreview)
                            {
                                // Show the texture
                                GUILayout.Box(availableItems[itemSlots[i + j]].itemPreview, GUILayout.Width(95), GUILayout.Height(95));
                            }

                            else
                            {
                                // Show the item name if no texture is available
                                GUILayout.Box(availableItems[itemSlots[i + j]].itemName, GUILayout.Width(95), GUILayout.Height(95));
                            }
                        }

                        else
                        {
                            // Empty slot
                            GUILayout.Box("", GUILayout.Width(95), GUILayout.Height(95));
                        }

                        // Detect if the mouse cursor is hovering over an item
                        Rect lastRect = GUILayoutUtility.GetLastRect();
                        Vector2 eventMousePosition = Event.current.mousePosition;
                        if (Event.current.type == EventType.Repaint && lastRect.Contains(eventMousePosition))
                        {
                            hoveringOverIndex = i + j;
                            if(itemIndexToDrag < 0)
                            {
                                dragOffset = new Vector2(lastRect.x - eventMousePosition.x, lastRect.y - eventMousePosition.y);
                            }
                        }

                        GUI.enabled = true;
                    }
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();

            if(Event.current.type == EventType.Repaint && !GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
            {
                hoveringOverIndex = -1;
            }

            GUILayout.EndArea();
        }

        if(itemIndexToDrag > -1)
        {
            if(availableItems[itemSlots[itemIndexToDrag]].itemPreview)
            {
                // Show the image
                GUI.Box(new Rect(Input.mousePosition.x + dragOffset.x, Screen.height - Input.mousePosition.y + dragOffset.y, 95, 95), availableItems[itemSlots[itemIndexToDrag]].itemPreview);
            }

            else
            {
                // Show the text
                GUI.Box(new Rect(Input.mousePosition.x + dragOffset.x, Screen.height - Input.mousePosition.y + dragOffset.y, 95, 95), availableItems[itemSlots[itemIndexToDrag]].itemName);
            }
        }

        if(hoveringOverIndex > -1 && itemSlots[hoveringOverIndex] > -1 && itemIndexToDrag < 0)
        {
            // Show a tooltip to the user
            GUI.Box(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y - 30, 100, 25), availableItems[itemSlots[hoveringOverIndex]].itemName);
        }
    }
}
