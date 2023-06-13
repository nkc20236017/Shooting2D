using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public Text ScoreLabel;     //距離を表示するUI-Textオブジェクトを保存
    public Slider slider;
    int score = 0;      //距離を保存する変数
    void Start()
    {
        slider.value = 1f;
    }

    void Update()
    {
        //残り時間が0になったらリロード
        if (slider.value < 0.1f)
        {
            SceneManager.LoadScene("GameScene");
        }
        ScoreLabel.text = "SCORE " + score.ToString("D6")+"P";
    }
    public void DecreaseHp()    //HPを減らす(敵にぶつかったとき)
    {
        slider.value -= 0.2f;
    }
    public void DecreaseHp2()    //HPを減らす(敵の攻撃にぶつかったとき)
    {
        slider.value -= 0.1f;
    }
    public void IncreaseScore()
    {
        score += 100;
    }
}
