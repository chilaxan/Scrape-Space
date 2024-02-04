using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ApiManager
{
    public static string API = "https://scrape-space.tech/api/";

    public static IEnumerator login(string username, string password, Action<UnityWebRequest> callback) {
        string data = JsonUtility.ToJson(new LoginUser(username, password));
        using (UnityWebRequest www = UnityWebRequest.Post(API + "login", data, "application/json")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log("Logged In");
            }
        }
    }
    
    public static IEnumerator register(string username, string password, Action<UnityWebRequest> callback) {
        string data = JsonUtility.ToJson(new LoginUser(username, password));
        using (UnityWebRequest www = UnityWebRequest.Post(API + "register", data, "application/json")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log("Registered And Logged In");
            }
        }
    }
    
    public static IEnumerator logout(Action<UnityWebRequest> callback) {
        using (UnityWebRequest www = UnityWebRequest.Post(API + "logout", "", "application/json")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log("Logged Out");
            }
        }
    }

    public static IEnumerator user(Action<UnityWebRequest> callback) {
        using (UnityWebRequest www = UnityWebRequest.Get(API + "user")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log("Got Current User");
            }
        }
    }

    public static IEnumerator upgrade(string upgrade_id) {
        using (UnityWebRequest www = UnityWebRequest.Post(API + "upgrade?upgrade_id=" + upgrade_id, "", "application/json")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log("Added Upgrade");
            }
        }
    }

    public static IEnumerator downgrade(string downgrade_id) {
        using (UnityWebRequest www = UnityWebRequest.Post(API + "downgrade?downgrade_id=" + downgrade_id, "", "application/json")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log("Removed Upgrade");
            }
        }
    }

    public static IEnumerator delta(int amount) {
        using (UnityWebRequest www = UnityWebRequest.Post(API + "delta?amount=" + amount.ToString(), "", "application/json")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log("Added delta");
            }
        }
    }

    public static IEnumerator leaderboard(int skip, int limit, Action<UnityWebRequest> callback) {
        using (UnityWebRequest www = UnityWebRequest.Get(API + "leaderboard?skip=" + skip.ToString() + "&limit=" + limit.ToString())) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log("Got Leaderboard");
            }
        }
    }
}
