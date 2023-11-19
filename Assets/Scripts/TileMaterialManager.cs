using UnityEngine;
using System.Collections.Generic;

public class TileMaterialManager : MonoBehaviour
{
    [SerializeField] private int[] tileValues; // Array for tile values like 2, 4, 8, etc.
    [SerializeField] private Material[] tileMaterials; // Array for corresponding materials

    private Dictionary<int, Material> tileMaterialDictionary = new Dictionary<int, Material>();

    void Awake()
    {
        // Check if both arrays are the same length to avoid errors
        if (tileValues.Length != tileMaterials.Length)
        {
            Debug.LogError("Tile values and materials arrays must be of the same length.");
            return;
        }

        // Populate the dictionary
        for (int i = 0; i < tileValues.Length; i++)
        {
            tileMaterialDictionary.Add(tileValues[i], tileMaterials[i]);
        }
    }

    public Material GetMaterialForValue(int value)
    {
        // Get the material based on tile value
        if (tileMaterialDictionary.TryGetValue(value, out Material material))
        {
            return material;
        }

        // Return null or a default material if the value is not found
        return null;
    }
}
