using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager {
    public static UnityWebRequest webSession = new UnityWebRequest();

    public static int TOTAL_SCRAP;

    public static void login(string username, string password) {
        string data = JsonUtility.ToJson(new LoginUser(username, password));
        
        using (UnityWebRequest www = UnityWebRequest.Post("https://scrape-space.tech/api/login", data, "application/json"))
        {
            www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
        }
    }



}
