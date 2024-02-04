using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    public Board scoreBoard = null;
    private User user = null;
    

    private void Start() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            throw new Exception("GameManager instance already exists");
        }
        StartCoroutine(ApiManager.user(request => {
            if (request.result == UnityWebRequest.Result.Success) {
                user = JsonUtility.FromJson<User>(request.downloadHandler.text);
                Debug.Log(request.downloadHandler.text);
                totalScrap = user.score;
            }
        }));

        // StartCoroutine(ApiManager.register("testaccount", "password"));
        fleetSizeCost.text = getFleetSizePrice().ToString();
        shipCapacityCost.text = getShipCapacityPrice().ToString();
        shipSpeedCost.text = getShipSpeedPrice().ToString();
        carrierSpeedCost.text = getScrapSpawnRateCost().ToString();
        carrierCountCost.text = getScrapWorthCost().ToString();
    }
    

    public int totalScrap = 0;

    public int fleetSize = 0;
    public int shipCapacity = 0;
    public int shipSpeed = 0;
    public int scrapSpawnRate = 0;
    public int scrapWorth = 0;

    public TextMeshProUGUI fleetSizeCost;
    public TextMeshProUGUI shipCapacityCost;
    public TextMeshProUGUI shipSpeedCost;
    public TextMeshProUGUI carrierSpeedCost;
    public TextMeshProUGUI carrierCountCost;
    
    public TextMeshProUGUI currentScrap;
    
    public GameObject shipPrefab;
    public GameObject planetContainer;
    public TextMeshProUGUI leaderboardText;
    

    public int getFleetSizePrice() {
        if (fleetSize == 0) {
            return 10;
        }
        return (int)(100f * Mathf.Pow(1.05f, fleetSize));
    }
    
    public int getShipCapacityPrice() {
        return (int)(10f * Mathf.Pow(1.2f, shipCapacity));
    }
    
    public int getShipSpeedPrice() {
        return (int)(10f * Mathf.Pow(1.05f, shipSpeed));
    }
    
    public int getScrapSpawnRateCost() {
        return (int)(10f * Mathf.Pow(1.05f, scrapSpawnRate));
    }
    
    public int getScrapWorthCost() {
        return (int)(10000f * Mathf.Pow(1.05f, scrapWorth));
    }

    public void addScrap() {
        // TODO: Upgrade that adds more scrap value per scrap.
        StartCoroutine(ApiManager.delta(1 + getScrapWorth(), _ => {}));
        totalScrap += 1 + getScrapWorth();
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
    
    public float getScrapSpawnRate() {
        return 1 + (scrapSpawnRate * 0.1f);
    }
    
    public int getScrapWorth() {
        return scrapWorth;
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
            GameObject ship = Instantiate(shipPrefab, planetContainer.transform, true);
            ship.transform.position = planetContainer.transform.position;

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
    
    public void tryBuyScrapSpawnRate() {
        int price = getScrapSpawnRateCost();
        if (totalScrap >= price) {
            totalScrap -= price;
            scrapSpawnRate += 1;
            carrierSpeedCost.text = getScrapSpawnRateCost().ToString();
        }
    }
    
    public void tryBuyScrapWorth() {
        int price = getScrapWorthCost();
        if (totalScrap >= price) {
            totalScrap -= price;
            scrapWorth += 1;
            carrierCountCost.text = getScrapWorthCost().ToString();
        }
    }
    public void getLeaderboard() {
        StartCoroutine(ApiManager.leaderboard(0, 10, response => {
            if (response != null) {
                // Serailize the response into a list of ScoreBoardUser
                leaderboardText.text = response.downloadHandler.text;
            }
            
        }));
    }
    
}
