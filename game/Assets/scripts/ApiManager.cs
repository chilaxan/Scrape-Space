using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ApiManager : MonoBehaviour {
    public static string API = "https://scrape-space.tech/api/";

    public static IEnumerator login(string username, string password, Action<UnityWebRequest> callback) {
        string data = JsonUtility.ToJson(new LoginUser(username, password));
        using (UnityWebRequest www = UnityWebRequest.Post(API + "login", data, "application/json")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
                callback.Invoke(www);
            }
            else {
                Debug.Log("Logged In");
                callback.Invoke(www);
            }
        }
    }
    
    public static IEnumerator register(string username, string password, Action<UnityWebRequest> callback) {
        username = username.Substring(0, username.Length - 1);
        password = password.Substring(0, password.Length - 1);
        string data = JsonUtility.ToJson(new LoginUser(username, password));
        
        using (UnityWebRequest www = UnityWebRequest.Post(API + "register", data, "application/json")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
                Debug.Log(www.downloadHandler.text);
                callback.Invoke(www);
            }
            else {
                Debug.Log("Registered And Logged In");
                callback.Invoke(www);
            }
        }
    }
    
    public static IEnumerator logout(Action<UnityWebRequest> callback) {
        using (UnityWebRequest www = UnityWebRequest.Post(API + "logout", "", "application/json")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
                callback.Invoke(www);
            }
            else {
                Debug.Log("Logged Out");
                callback.Invoke(www);
            }
        }
    }

    public static IEnumerator user(Action<UnityWebRequest> callback) {
        using (UnityWebRequest www = UnityWebRequest.Get(API + "user")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
                callback.Invoke(www);
            }
            else {
                Debug.Log("Got Current User");
                callback.Invoke(www);
            }
        }
    }

    public static IEnumerator upgrade(string upgrade_id, Action<UnityWebRequest> callback) {
        using (UnityWebRequest www = UnityWebRequest.Post(API + "upgrade?upgrade_id=" + upgrade_id, "", "application/json")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
                callback.Invoke(www);
            }
            else {
                Debug.Log("Added Upgrade");
                callback.Invoke(www);
            }
        }
    }

    public static IEnumerator downgrade(string downgrade_id, Action<UnityWebRequest> callback) {
        using (UnityWebRequest www = UnityWebRequest.Post(API + "downgrade?downgrade_id=" + downgrade_id, "", "application/json")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
                callback.Invoke(www);
            }
            else {
                Debug.Log("Removed Upgrade");
                callback.Invoke(www);
            }
        }
    }

    public static IEnumerator delta(int amount, Action<UnityWebRequest> callback) {
        using (UnityWebRequest www = UnityWebRequest.Post(API + "delta?amount=" + amount.ToString(), "", "application/json")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
                callback.Invoke(www);
            }
            else {
                callback.Invoke(www);
                Debug.Log("Added delta");
            }
        }
    }

    public static IEnumerator leaderboard(int skip, int limit, Action<UnityWebRequest> callback) {
        using (UnityWebRequest www = UnityWebRequest.Get(API + "leaderboard?skip=" + skip.ToString() + "&limit=" + limit.ToString())) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
                callback.Invoke(www);
            }
            else {
                callback.Invoke(www);
                Debug.Log("Got Leaderboard");
            }
        }
    }
}
