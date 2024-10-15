using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public string ingredientName;  // Name of the ingredient 
    public RecipeManager recipeManager;  // Reference to the RecipeManager script

    private Vector3 originalScale;  // To store the original scale of the ingredient

    void Start()
    {
        // Store the original scale of the ingredient
        originalScale = transform.localScale;
    }

    // Function to handle tapping on the ingredient
    void OnMouseDown()
    {
        // Notify the RecipeManager that this ingredient was tapped
        recipeManager.OnIngredientTapped(this);

        // Start the click animation
        AnimateClick();
    }

    // Function to animate the click (scaling down and up)
    void AnimateClick()
    {
        // Scale down the ingredient when clicked
        transform.localScale = originalScale * 0.9f;

        // Schedule the scale to reset after a short delay
        Invoke("ResetScale", 0.1f);
    }

    // Function to reset the scale back to the original size
    void ResetScale()
    {
        // Reset the ingredient's scale to its original size
        transform.localScale = originalScale;
    }
}
