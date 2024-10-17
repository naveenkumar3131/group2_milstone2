using UnityEngine;

[CreateAssetMenu(fileName = "TankScriptableObjects", menuName = "ScriptableObjects/TankScriptables/NewTankScriptableObject")]
public class RecipeScriptable : ScriptableObject
{
    public string RecipeType;
    public Sprite IngridientImage;
    public string IngridientName;
    
}
