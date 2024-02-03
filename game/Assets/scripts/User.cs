using System;
using JetBrains.Annotations;

[Serializable]
public class User {
    public string username;
    public int score;
    public int[] upgrades;
}