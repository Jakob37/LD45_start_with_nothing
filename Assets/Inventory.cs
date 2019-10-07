using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool start_with_shovel;
    public int start_tomatoes;
    public int start_seeds;


    private InventoryText inv_text;

    private bool has_shovel;
    public bool HasShovel {
        get {
            return has_shovel;
        }
    }

    private int seeds;
    public int Seeds {
        get {
            return seeds;
        }
    }

    private int tomatoes;
    public int Tomatoes {
        get {
            return tomatoes;
        }
    }

    void Awake() {
        inv_text = FindObjectOfType<InventoryText>();
    }

    void Start() {
        seeds = start_seeds;
        tomatoes = start_tomatoes;

        if (start_with_shovel) {
            has_shovel = true;
        }
    }

    void Update() {

        string inventory_text = "Tomatoes: " + tomatoes + "\n" + "Seeds: " + seeds;
        if (has_shovel) {
            inventory_text += "\n Has shovel";
        }

        inv_text.SetText(inventory_text);
    }

    public void BuySeed(int price) {
        seeds += 1;
        tomatoes -= price;
    }

    public void AddTomato() {
        tomatoes += 1;
    }

    public void AddSeed() {
        seeds += 1;
    }

    public void PlantSeed() {
        seeds -= 1;
    }

    public void BuyShovel(int price) {

        if (price > tomatoes || has_shovel) {
            throw new System.Exception("Invalid request, either too few tomatoes or you already have the shovel. Tomatoes: " + tomatoes + " has shovel: " + has_shovel);
        }

        has_shovel = true;
        tomatoes -= price;
    }

    public void BreakShovel() {
        has_shovel = false;
    }

    public void OnDestroy() {
        Score.Tomatoes = tomatoes;
    }
}
