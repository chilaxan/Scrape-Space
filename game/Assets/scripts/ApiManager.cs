using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ApiManager
{
    public static string API = "https://scrape-space.tech/api/";

    public static IEnumerable login(string username, string password) {
        string data = JsonUtility.ToJson(new LoginUser(username, password));
        using (UnityWebRequest www = UnityWebRequest.Post(API + "login", data, "application/json")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log('Logged In');
            }
        }
    }
    
    public static IEnumerable register(string username, string password) {
        string data = JsonUtility.ToJson(new LoginUser(username, password));
        using (UnityWebRequest www = UnityWebRequest.Post(API + "register", data, "application/json")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log('Registered And Logged In');
            }
        }
    }
    
    public static IEnumerable logout() {
        using (UnityWebRequest www = UnityWebRequest.Post(API + "logout", "", "application/json")) {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success) {
                Debug.Log(www.error);
            }
            else {
                Debug.Log('Logged Out');
            }
        }
    }
}
