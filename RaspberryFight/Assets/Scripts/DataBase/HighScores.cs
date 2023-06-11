using UnityEngine;
using UnityEngine.UI;
using Firebase.Firestore;
public class HighScores : MonoBehaviour
{
    public Text[] scores;
    private FirebaseFirestore dbreference;
    async void Awake()
    {
        dbreference = FirebaseFirestore.DefaultInstance;
        Query highScores = dbreference.Collection("Users").OrderByDescending("Score").Limit(11);
        QuerySnapshot highScoreSnapshot = await highScores.GetSnapshotAsync();
        for (int i = 0; i < highScoreSnapshot.Count; i++) {
            User _user = highScoreSnapshot[i].ConvertTo<User>();
            scores[i].text = i+1 + ". " + _user.Nickname+ "  " +Mathf.Round(_user.Score);
        }
    }
}
