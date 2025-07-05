using UnityEngine;
using TMPro; // Importing TextMeshPro for UI text display
using UnityEngine.UI; // Importing Unity UI for Image component

public class PlayerBehaviour : MonoBehaviour
{
    CollectableBehaviour currentCollectable; // Reference to the currently detected collectable object

    [SerializeField]
    TextMeshProUGUI partsLeftText; // Text to display the player's parts left

    [SerializeField]
    TextMeshProUGUI intructionText; // Text to display the introduction message

    [SerializeField]
    Image MapIcon; // UI Image to track the number of parts collected

    [SerializeField]
    int collectedParts = 0; // Number of parts collected by the player

    int totalParts = 3; // Total number of parts available in the game 

    // This script handles the player's behavior, including collecting items
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        partsLeftText.text = "Parts left: " + (totalParts - collectedParts); // Initialize parts collected text
        // intructionText.text = "Keybinds: W to move forward, A to move left, D to move right, S to move backward, Space bar to jump, Shift to sprint. Walk into the objects to collect them. Collect all parts to get off this planet!"; // Set the initial instruction text
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Player collided with: " + collision.gameObject.name);
        // If the player collides with a collectable object, we can interact with it
        // This will allow the player to collect the object
        // The CollectableBehaviour script should handle the logic for collecting the object
        if (collision.gameObject.GetComponent<CollectableBehaviour>() != null)
        {
            currentCollectable = collision.gameObject.GetComponent<CollectableBehaviour>();
            currentCollectable.Collect(this); // Call the Collect method on the collectable object, passing this PlayerBehaviour instance
            AudioSource.PlayClipAtPoint(currentCollectable.collectSound, transform.position); // Play the collect sound if it is assigned
            currentCollectable = null; // Reset current collectable after interaction
        }
    }

    public void ModifyScore(int amount)
    {
        collectedParts += amount;
        partsLeftText.text = "Parts Left: " + (totalParts - collectedParts); // Update parts left text
    }
}
