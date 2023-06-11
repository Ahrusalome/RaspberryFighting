using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class DatabaseManager : MonoBehaviour
{
    public InputField _nickName;
    public Slider _moveSpeed;
    public Slider _shotSpeed;
    public Slider _dexterity;
    public Slider _attack;
    public Slider _health;
    public InputField _nickNameP2;
    public Slider _moveSpeedP2;
    public Slider _shotSpeedP2;
    public Slider _dexterityP2;
    public Slider _attackP2;
    public Slider _healthP2;
    public int winnerNB;
    public float winnerScore;
    private FirebaseFirestore dbreference;
    private GameManager gameManager;
    public static DatabaseManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        gameManager = GameManager.instance;
        dbreference = FirebaseFirestore.DefaultInstance;
    }
    public async void CreateUser() {
        User player1 = new User 
        {
            PlayerNb = 1,
            Score = 0,
            Nickname = _nickName.text,
            MoveSpeed = _moveSpeed.value,
            ShotSpeed = _shotSpeed.value,
            Attack = _attack.value,
            Dexterity = _dexterity.value,
            Health = _health.value,
            isPlaying = true
        };
        User player2 = new User 
        {
            PlayerNb = 2,
            Score = 0,
            Nickname = _nickNameP2.text,
            MoveSpeed = _moveSpeedP2.value,
            ShotSpeed = _shotSpeedP2.value,
            Attack = _attackP2.value,
            Dexterity = _dexterityP2.value,
            Health = _healthP2.value,
            isPlaying = true
        };
        CollectionReference userRef = dbreference.Collection("Users");
        await userRef.AddAsync(player1).ContinueWithOnMainThread(task => {
            Debug.Log("User1 added");
        });

        await userRef.AddAsync(player2).ContinueWithOnMainThread(task => {
            Debug.Log("User2 added");
        });
        if (gameManager.selectedCharacters.Count == 2)
        {
            SceneManager.LoadSceneAsync("FightScene");
        }
    }

    public async void SetNonPlaying() {
        Query usersQuery = dbreference.Collection("Users").WhereEqualTo("isPlaying", true);
        QuerySnapshot userQuerySnapshot = await usersQuery.GetSnapshotAsync();
        foreach (DocumentSnapshot user in userQuerySnapshot)
        {
            Dictionary<string, object> update = new Dictionary<string, object> {
                {"isPlaying", false}
            };
            await user.Reference.UpdateAsync(update);
        }
    }

    public async void SetScore(int winner) {
        winnerNB = winner;
        // stats difficulty = 90 - sum of stats
        // health Difficulty = (healthWin-healthLoose)/100 * -1
        // total score = (winnerDifficulty - looserDifficult + healthDifficulty) * (1000/Time) + basePoint (=500)
        float winnerDifficulty = 0;
        float looserDifficulty=0;
        float winnerHealth=0;
        float looserHealth=0;
        Query winnerQuery;
        Query looserQuery;
        if (winner == 1 ){
            winnerQuery = dbreference.Collection("Users").WhereEqualTo("isPlaying", true).WhereEqualTo("PlayerNb", 1);
            looserQuery = dbreference.Collection("Users").WhereEqualTo("isPlaying", true).WhereEqualTo("PlayerNb", 2);
        } else {
            winnerQuery = dbreference.Collection("Users").WhereEqualTo("isPlaying", true).WhereEqualTo("PlayerNb", 2);
            looserQuery = dbreference.Collection("Users").WhereEqualTo("isPlaying", true).WhereEqualTo("PlayerNb", 1);           
        }
        QuerySnapshot winnerQuerySnapshot = await winnerQuery.GetSnapshotAsync();
        foreach (DocumentSnapshot user in winnerQuerySnapshot)
        {
            User winnerStats = user.ConvertTo<User>();
            winnerDifficulty = 90 - winnerStats.MoveSpeed + winnerStats.Attack + winnerStats.Dexterity + winnerStats.ShotSpeed;
            winnerHealth = winnerStats.Health;
        }
        QuerySnapshot looserQuerySnapshot = await looserQuery.GetSnapshotAsync();
        foreach (DocumentSnapshot user in looserQuerySnapshot)
        {
            User looserStats = user.ConvertTo<User>();
            looserDifficulty = 90 - looserStats.MoveSpeed + looserStats.Attack + looserStats.Dexterity + looserStats.ShotSpeed;
            looserHealth = looserStats.Health;
        }
        float healthDifficulty = (winnerHealth-looserHealth)/100 *-1;
        winnerScore = (winnerDifficulty - looserDifficulty + healthDifficulty) * (1000/300) + 500;
        foreach (DocumentSnapshot user in winnerQuerySnapshot)
        {
            Dictionary<string, object> update = new Dictionary<string, object> {
                {"Score", winnerScore}
            };
            await user.Reference.UpdateAsync(update);
        }
        SceneManager.LoadScene("EndScene");
    }
}
