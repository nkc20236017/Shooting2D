using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public Text ScoreLabel;     //������\������UI-Text�I�u�W�F�N�g��ۑ�
    public Slider slider;
    int score = 0;      //������ۑ�����ϐ�
    void Start()
    {
        slider.value = 1f;
    }

    void Update()
    {
        //�c�莞�Ԃ�0�ɂȂ����烊���[�h
        if (slider.value < 0.1f)
        {
            SceneManager.LoadScene("GameScene");
        }
        ScoreLabel.text = "SCORE " + score.ToString("D6")+"P";
    }
    public void DecreaseHp()    //HP�����炷(�G�ɂԂ������Ƃ�)
    {
        slider.value -= 0.2f;
    }
    public void DecreaseHp2()    //HP�����炷(�G�̍U���ɂԂ������Ƃ�)
    {
        slider.value -= 0.1f;
    }
    public void IncreaseScore()
    {
        score += 100;
    }
}
