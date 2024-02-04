using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LoginHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI usernameBox;
    public TextMeshProUGUI passwordBox;
    void Start() {
        StartCoroutine(ApiManager.user(request => {
            if (request.result == UnityWebRequest.Result.Success) {
                SceneManager.LoadScene("SampleScene");
            }
        }));
    }

    public void loginUser() {
        string username = usernameBox.text;
        string password = passwordBox.text;
        StartCoroutine(ApiManager.login(username, password, request => {
            if (request.result == UnityWebRequest.Result.Success) {
                SceneManager.LoadScene("SampleScene");
            }
        }));
    }
    public void registerUser() {
        string username = usernameBox.text;
        string password = passwordBox.text;
        StartCoroutine(ApiManager.register(username, password, request => {
            if (request.result == UnityWebRequest.Result.Success) {
                SceneManager.LoadScene("SampleScene");
            }
        }));
    }
}
