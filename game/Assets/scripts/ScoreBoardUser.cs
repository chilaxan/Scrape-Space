using System;
using System.Collections.Generic;

public class ScoreBoardUser {
    public string username;
    public int score;
}

[Serializable]
public class Board {
    public List<ScoreBoardUser> board;
}
