using DOW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DOW 
{
public class DicePopup : Popup<DicePopup>
{
    [SerializeField]
    //public GameObject dice = null;
    public Text dice = null;

    private long seed = -1;

    private int diceCount = 0;

    private void Start()
    {
        seed = UserInfo.Instance.GetInfo<BattleInfo>().DiceSeed;
        Random.InitState((int)seed);
    }
    private void OnEnable()
    {
        RollDice();
    }

    private void OnDisable() {
        UserInfo.Instance.GetInfo<BattleInfo>().DiceCount = diceCount;
    }

    public void RollDice()
    {
        int die = Random.Range(1, 8);
        dice.text = die.ToString();
        diceCount += 1;
    }
        
    //popup
    public static DicePopup OpenPopup()
        {
            DicePopup popup = PopupManager.OpenPopup<DicePopup>("DicePopup");

            return popup;
        }
        public override void Initialize()
        {

        }

        public override void InitializeUI()
        {

        }

        public override void Refresh()
        {

        }

}
}
