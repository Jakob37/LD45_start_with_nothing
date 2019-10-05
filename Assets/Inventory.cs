using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private InventoryText inv_text;

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
        seeds = 0;
        tomatoes = 0;
    }

    void Update() {
        inv_text.SetText("Tomatoes: " + tomatoes + "\n" + "Seeds: " + seeds);
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
}
