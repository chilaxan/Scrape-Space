using System;

[Serializable]
public class LoginUser {
    public LoginUser(string username, string password) {
        this.username = username;
        this.password = password;
    }
    public string username;
    public string password;
}