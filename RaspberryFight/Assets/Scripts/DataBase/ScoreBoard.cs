using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;

public class ScoreBoard : MonoBehaviour
{
    public Text[] scores;
    private FirebaseFirestore dbreference;
    float winnerScore;
    int index = 0;
    async void Awake()
    {
        int winnerNB = DatabaseManager.instance.winnerNB;
        scores[0].text = "Player " + winnerNB + " wins !";
        winnerScore = DatabaseManager.instance.winnerScore;
        dbreference = FirebaseFirestore.DefaultInstance;
        Query userScores = dbreference.Collection("Users").OrderBy("Score");
        QuerySnapshot userScoreQuerySnapshot = await userScores.GetSnapshotAsync();
        foreach (DocumentSnapshot user in userScoreQuerySnapshot)
        {
            index++;
            User _user = user.ConvertTo<User>();
            if (_user.Score == winnerScore) {
                break;
            }
        }
        scores[5].text = index + ".  YOU  " + winnerScore;
        Query previousScores = dbreference.Collection("Users").OrderByDescending("Score").WhereLessThan("Score", winnerScore).Limit(5);
        QuerySnapshot prevScoreSnapshot = await previousScores.GetSnapshotAsync();
        for (int i = 0; i< 4; i++) {
            User _user = prevScoreSnapshot[i+1].ConvertTo<User>();
            scores[i+6].text = index + i+1 + ".   " + _user.Nickname+"   " +Mathf.Round(_user.Score);
        }
        Query nextScores = dbreference.Collection("Users").OrderBy("Score").WhereGreaterThan("Score", winnerScore).Limit(5);
        QuerySnapshot nextScoreSnapshot = await nextScores.GetSnapshotAsync();
        for (int i = 4; i> 0; i--) {
            User _user = nextScoreSnapshot[i].ConvertTo<User>();
            scores[i].text = index-1 + ".   " + _user.Nickname+"   "+ Mathf.Round(_user.Score);
            index--;
        }
    }
}
