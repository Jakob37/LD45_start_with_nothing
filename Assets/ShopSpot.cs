using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpotType {
    Shovel,
    Seed
}

public class ShopSpot : MonoBehaviour
{
    public GameObject shop_seed_prefab;
    public GameObject shop_shovel_prefab;

    public SpotType shop_spot_type;

    private GameObject shop_object;
    public GameObject ShopObject {
        get {
            return shop_object;
        }
    }

    private SpriteRenderer sprite_renderer;

    void Awake() {
        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    void Start() {
        if (shop_spot_type == SpotType.Seed) {
            shop_object = Instantiate(shop_seed_prefab);
        }
        else if (shop_spot_type == SpotType.Shovel) {
            shop_object = Instantiate(shop_shovel_prefab);
        }
        else {
            throw new System.Exception("Unknown spot type: " + shop_spot_type);
        }
        shop_object.transform.position = gameObject.transform.position;
    }

    public bool HasShopSeed() {

        return shop_object != null && shop_object.GetComponent<ShopSeed>() != null;
    }

    public bool HasShopShovel() {
        return shop_object != null && shop_object.GetComponent<ShopShovel>() != null;
    }

    public ShopSeed GetShopSeed() {
        return shop_object.GetComponent<ShopSeed>();
    }

    public ShopShovel GetShopShovel() {
        return shop_object.GetComponent<ShopShovel>();
    }

    public bool HasShovel() {
        return shop_object.GetComponent<ShopShovel>() != null;
    }

    public void BuyObject() {
        shop_object = null;
    }

    void Update() {
        UpdateDisplay();
    }

    private void UpdateDisplay() {
        if (shop_object != null) {
            sprite_renderer.sprite = shop_object.GetComponent<SpriteRenderer>().sprite;
        }
        else {
            sprite_renderer.sprite = null;
        }
    }
}
