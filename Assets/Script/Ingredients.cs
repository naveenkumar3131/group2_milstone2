using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string IngredientName; // Name of the ingredient (e.g. "Dough", "Water")
    public int tapCountRequired;  // Number of times the player needs to tap this ingredient
    private int currentTapCount = 0; // Counter for taps

    public delegate void OnIngredientTapped(Ingredient ingredient);
    public static event OnIngredientTapped IngredientTapped;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    // Function to handle tapping on the ingredient
    void OnMouseDown()
    {
        currentTapCount++;
        AnimateClick();
        Debug.Log(currentTapCount);

        if (currentTapCount >= tapCountRequired)
        {
            IngredientTapped?.Invoke(this); // Notify that the ingredient has been tapped the required number of times
            currentTapCount = 0; // Reset for next round
        }
    }

    // Function for scaling down/up when clicked (transform animation)
    void AnimateClick()
    {
        transform.localScale = originalScale * 0.9f; // Scale down
        Invoke("ResetScale", 0.1f); // Reset after a short delay
    }

    void ResetScale()
    {
        transform.localScale = originalScale;
    }
}

