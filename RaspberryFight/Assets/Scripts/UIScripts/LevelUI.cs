using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public Text TextLine1;
    public Slider[] healthSliders;
    public GameObject[] winIndicatorGrids;
    public GameObject winIndicator;
    public static LevelUI instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    public void AddWinIndicator(int player) {
        GameObject winIndicatorToAdd = Instantiate(winIndicator, transform.position, Quaternion.identity);
        winIndicatorToAdd.transform.SetParent(winIndicatorGrids[player].transform);
    }
}
