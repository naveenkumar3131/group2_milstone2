using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class RecipeManager : MonoBehaviour
{
    public List<Ingredient> recipeIngredients; // List of ingredients in the current recipe
    private int currentIngredientIndex = 0; // Tracks which ingredient in the recipe we are on

    public GameObject diamondPrefab; // Prefab to spawn when recipe is completed successfully
    public GameObject squarePrefab;  // Prefab to spawn when wrong ingredient is tapped

    // References to the last spawned objects
    private GameObject spawnedDiamond;
    private GameObject spawnedSquare;

    // Define the three recipes
    private List<Ingredient> recipe1;
    private List<Ingredient> recipe2;
    private List<Ingredient> recipe3;
    private string RecipeString = "";
    
    public TextMeshProUGUI RecipeText;
    
    void Start()
    {
        // Define the recipes
        DefineRecipes();

        // Set a random recipe at the start of the game
        SetRandomRecipe();

        if (recipeIngredients != null)
        {
            for (int i = 0; i < recipeIngredients.Count; i++)
            {
                RecipeString += $" {i + 1}. ";
                RecipeString += recipeIngredients[i].ingredientName;
                
            }
        }
        
        RecipeText.text = RecipeString;

    }

    // Function to define the recipes
    void DefineRecipes()  //make a class for recipes and make a scriptable object for recipe
    {
        // Get references to the ingredients (ensure these objects have an Ingredient script attached)
        Ingredient pinkIngredient = GameObject.Find("Pink").GetComponent<Ingredient>();   //use public variable replace find
        Ingredient limeIngredient = GameObject.Find("Lime").GetComponent<Ingredient>();
        Ingredient greenIngredient = GameObject.Find("Green").GetComponent<Ingredient>();
        Ingredient yellowIngredient = GameObject.Find("Yellow").GetComponent<Ingredient>();
        Ingredient brownIngredient = GameObject.Find("Brown").GetComponent<Ingredient>();

        // Assign the RecipeManager reference to each ingredient
        pinkIngredient.recipeManager = this;
        limeIngredient.recipeManager = this;
        greenIngredient.recipeManager = this;
        yellowIngredient.recipeManager = this;
        brownIngredient.recipeManager = this;

        // Define the 3 recipes
        recipe1 = new List<Ingredient> { pinkIngredient, limeIngredient, brownIngredient };
        recipe2 = new List<Ingredient> { limeIngredient, greenIngredient, yellowIngredient };
        recipe3 = new List<Ingredient> { pinkIngredient, limeIngredient, greenIngredient };
    }

    // Function to set a random recipe
    void SetRandomRecipe()
    {
        // Before setting a new recipe, clear any previously spawned objects
        ClearSpawnedObjects();

        int randomRecipeIndex = Random.Range(0, 3); // Generate a random number between 0 and 2

        // Clear the previous recipe and set a new one based on the random index
        recipeIngredients = new List<Ingredient>();

        if (randomRecipeIndex == 0)
        {
            recipeIngredients = recipe1;
            Debug.Log("Random Recipe Selected: Pink + Lime + Brown");
        }
        else if (randomRecipeIndex == 1)
        {
            recipeIngredients = recipe2;
            Debug.Log("Random Recipe Selected: Lime + Green + Yellow");
        }
        else if (randomRecipeIndex == 2)
        {
            recipeIngredients = recipe3;
            Debug.Log("Random Recipe Selected: Pink + Lime + Green");
        }

        currentIngredientIndex = 0; // Start with the first ingredient in the recipe
    }

    // Function that gets called when an ingredient is tapped
    public void OnIngredientTapped(Ingredient tappedIngredient)
    {
        // Check if the tapped ingredient is the one we expect in the recipe
        if (tappedIngredient == recipeIngredients[currentIngredientIndex])
        {
            Debug.Log("Correct ingredient tapped: {tappedIngredient.ingredientName}");

            // Move to the next ingredient in the recipe
            currentIngredientIndex++;

            // Check if the recipe is complete
            if (currentIngredientIndex >= recipeIngredients.Count)
            {
                Debug.Log("Recipe complete!");
                SpawnDiamond(); // Spawn a diamond on successful completion of the recipe

                // Start a random recipe after 2 seconds 
                Invoke("SetRandomRecipe", 2);//this will happen after 2 second
            }//write a timer intead of invoke
        }
        else
        {
            // If the wrong ingredient is tapped, reset and spawn a square
            Debug.Log("Wrong ingredient tapped, resetting.");
            SpawnSquare(); // Spawn a square for wrong ingredient tapped
            currentIngredientIndex = 0;

            // Start a random recipe after 2 seconds 
            Invoke("SetRandomRecipe", 2);//this will happen after 2 second
        }
    }

    // Function to spawn a diamond when the recipe is completed successfully
    void SpawnDiamond()
    {
        if (diamondPrefab != null)
        {
            // Destroy previous diamond if it exists
            if (spawnedDiamond != null)
            {
                Destroy(spawnedDiamond);
            }


            spawnedDiamond = Instantiate(diamondPrefab, new Vector3(0, 0, 0), diamondPrefab.transform.rotation);
            Debug.Log("Diamond spawned for completing the recipe.");
        }
        else
        {
            Debug.LogError("Diamond prefab is not assigned in the inspector.");
        }
    }

    // Function to spawn a square when the wrong ingredient is tapped
    void SpawnSquare()
    {
        if (squarePrefab != null)
        {
            // Destroy previous square if it exists
            if (spawnedSquare != null)
            {
                Destroy(spawnedSquare);
            }


            spawnedSquare = Instantiate(squarePrefab, new Vector3(0, 0, 0), squarePrefab.transform.rotation);
            Debug.Log("Square spawned for wrong ingredient.");
        }
        else
        {
            Debug.LogError("Square prefab is not assigned in the inspector.");
        }
    }

    // Function to clear any spawned objects (diamond or square) before a new recipe starts
    void ClearSpawnedObjects()
    {
        if (spawnedDiamond != null)
        {
            Destroy(spawnedDiamond);
            Debug.Log("Previous diamond destroyed.");
        }

        if (spawnedSquare != null)
        {
            Destroy(spawnedSquare);
            Debug.Log("Previous square destroyed.");
        }
    }


    public void OnPinkIngredientTap()
    {
        Debug.Log("Pink ingredient Tapped");
    }
}


