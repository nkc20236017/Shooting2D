using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class GameDirector : MonoBehaviour
{
    public Text ScoreLabel;     //������\������UI-Text�I�u�W�F�N�g��ۑ�
    public Text GameScore;
    public Text HighScore;
    public GameObject ResultPanel;
    public GameObject BosHP;
    public GameObject MainCamera;
    public GameObject Player;
    public Slider playerslider;
    public Slider powerslider;
    public Slider bosenemyslider;
    int score = 0;      //������ۑ�����ϐ�
    int highscore = 0;
    public bool judge = true;
    public bool BosCreate = false;
    int BosCreatescore = 100;
    void Start()
    {
        playerslider.value = 1f;
        powerslider.value = 0f;
        bosenemyslider.value = 0f;
        highscore = PlayerPrefs.GetInt("SCORE", 0);
    }

    void Update()
    {
        powerslider.value = Player.GetComponent<PlayerController>().timer;
        if (score >= BosCreatescore)
        {
            BosCreate = true;
            BosHP.SetActive(true);
            bosenemyslider.value = 1f;
            BosCreatescore += BosCreatescore + 10000;
        }
        playerslider.value = Mathf.Clamp(playerslider.value, 0f, 1f);
        //�c��HP��0�ɂȂ����烊���[�h
        if (playerslider.value == 0f)
        {
            judge = false;
            ResultPanel.SetActive(true);
        }
        ScoreLabel.text = "SCORE " + score.ToString("D6")+"P";

        GameScore.text = "�X�R�A\n\n" + score.ToString("D6") + "P";
        if (judge==false)
        {
            if (highscore < score)
            {
                highscore = score;
                PlayerPrefs.SetInt("SCORE", highscore);
                PlayerPrefs.Save();
            }
            HighScore.text = "�n�C�X�R�A\n\n" + highscore.ToString("D6") + "P";
        }
    }
    public void DecreaseHp()    //HP�����炷(�G�ɂԂ������Ƃ�)
    {
        MainCamera.GetComponent<CameraController>().Shake(0.25f, 0.2f);
        playerslider.value -= 0.2f;
    }
    public void DecreaseHp2()    //HP�����炷(�G�̍U���ɂԂ������Ƃ�)
    {
        MainCamera.GetComponent<CameraController>().Shake(0.25f, 0.2f);
        playerslider.value -= 0.1f;
    }
    public void DecreasebosHp()    //HP�����炷
    {
        bosenemyslider.value -= 0.05f;
    }
    public void DecreasebosHp2()    //HP�����炷
    {
        bosenemyslider.value -= 0.2f;
    }
    public void IncreaseScore()
    {
        score += 100;
    }
    public void IncreasebosScore()
    {
        score += 1000;
    }
    public void RetryButton()
    {
        SceneManager.LoadScene("GameScene");
    }
}
