using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    private void Start() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            throw new Exception("GameManager instance already exists");
        }

        // StartCoroutine(ApiManager.register("testaccount", "password"));
        fleetSizeCost.text = getFleetSizePrice().ToString();
        shipCapacityCost.text = getShipCapacityPrice().ToString();
        shipSpeedCost.text = getShipSpeedPrice().ToString();
        carrierSpeedCost.text = getCarrierSpeedPrice().ToString();
        carrierCountCost.text = getCarrierCountPrice().ToString();
    }

    public int totalScrap = 0;

    public int fleetSize = 0;
    public int shipCapacity = 0;
    public int shipSpeed = 0;
    public int carrierSpeed = 0;
    public int carrierCount = 0;

    public TextMeshProUGUI fleetSizeCost;
    public TextMeshProUGUI shipCapacityCost;
    public TextMeshProUGUI shipSpeedCost;
    public TextMeshProUGUI carrierSpeedCost;
    public TextMeshProUGUI carrierCountCost;
    
    public TextMeshProUGUI currentScrap;
    

    public int getFleetSizePrice() {
        return (int)(100f * Mathf.Pow(1.05f, fleetSize));
    }
    
    public int getShipCapacityPrice() {
        return (int)(10f * Mathf.Pow(1.05f, shipCapacity));
    }
    
    public int getShipSpeedPrice() {
        return (int)(10f * Mathf.Pow(1.05f, shipSpeed));
    }
    
    public int getCarrierSpeedPrice() {
        return (int)(10f * Mathf.Pow(1.05f, carrierSpeed));
    }
    
    public int getCarrierCountPrice() {
        return (int)(10000f * Mathf.Pow(1.05f, carrierCount));
    }

    public void addScrap() {
        // TODO: Upgrade that adds more scrap value per scrap.
        totalScrap += 1;
    }
    
    
    public int getFleetSize() {
        return fleetSize;
    }
    public int getShipCapacity() {
        return shipCapacity;
    }
    public float getShipSpeedMultiplier() {
        return 1.0f + (shipSpeed * 0.1f);
    }

    private void Update() {
        currentScrap.text = totalScrap.ToString() + "kg Scrap";
    }
    
    public void tryBuyFleetSize() {
        int price = getFleetSizePrice();
        if (totalScrap >= price) {
            totalScrap -= price;
            fleetSize += 1;
            fleetSizeCost.text = getFleetSizePrice().ToString();
        }
    }
    
    public void tryBuyShipCapacity() {
        int price = getShipCapacityPrice();
        if (totalScrap >= price) {
            totalScrap -= price;
            shipCapacity += 1;
            shipCapacityCost.text = getShipCapacityPrice().ToString();
        }
    }
    
    public void tryBuyShipSpeed() {
        int price = getShipSpeedPrice();
        if (totalScrap >= price) {
            totalScrap -= price;
            shipSpeed += 1;
            shipSpeedCost.text = getShipSpeedPrice().ToString();
        }
    }
    
    public void tryBuyCarrierSpeed() {
        int price = getCarrierSpeedPrice();
        if (totalScrap >= price) {
            totalScrap -= price;
            carrierSpeed += 1;
            carrierSpeedCost.text = getCarrierSpeedPrice().ToString();
        }
    }
    
    public void tryBuyCarrierCount() {
        int price = getCarrierCountPrice();
        if (totalScrap >= price) {
            totalScrap -= price;
            carrierCount += 1;
            carrierCountCost.text = getCarrierCountPrice().ToString();
        }
    }
    
}
