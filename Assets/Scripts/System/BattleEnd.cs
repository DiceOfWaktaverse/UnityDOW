using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEnd : MonoBehaviour
{
    public enum Status
    {
        Victory,
        Defeat
    }

    public Text resultText;

    public Transform PlaytimeText;

    public Transform RewardText;
    public Transform Reward;

    public void setEndResult(Status status)
    {
        if(status == Status.Victory)
        {
            resultText.text = "½Â¸®";
            PlaytimeText.GetComponent<RectTransform>().localPosition = new Vector2(0,0);
            RewardText.gameObject.SetActive(true);
            RewardText.gameObject.SetActive(true);
        }
        else if(status == Status.Defeat)
        {
            resultText.text = "ÆÐ¹è";
            PlaytimeText.GetComponent<RectTransform>().localPosition = new Vector2(0,90);
            RewardText.gameObject.SetActive(false);
            RewardText.gameObject.SetActive(false);

        }
    }

}
