using Firebase.Firestore;

[FirestoreData]
public struct User
{
    [FirestoreProperty] public int PlayerNb {get; set;}
    [FirestoreProperty] public float Score {get; set;}
    [FirestoreProperty] public string Nickname {get; set;}
    [FirestoreProperty] public float MoveSpeed {get; set;}
    [FirestoreProperty] public float ShotSpeed {get; set;}
    [FirestoreProperty] public float Attack {get; set;}
    [FirestoreProperty] public float Dexterity {get; set;}
    [FirestoreProperty] public float Health {get; set;}
    [FirestoreProperty] public bool isPlaying {get; set;}
}
