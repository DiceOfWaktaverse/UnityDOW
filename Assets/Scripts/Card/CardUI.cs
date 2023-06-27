using UnityEngine;
using UnityEngine.UI;

namespace DOW
{
    public class CardUI : MonoBehaviour
    {
        private Card card = null;

        // 컨트롤 해야 할 컴포넌트들
        private Text LevelText = null;
        private Text LabelText = null;
        private Text HpValue = null;

        private Image CardImage = null;
        private Image CardImageMirror = null;

        private GameObject TagLayout = null;
        private GameObject TagTemplate = null;

        private GameObject DiceDescriptionLayout = null;
        private GameObject DiceDescriptionTemplate = null;

        private GameObject DescriptionLayout = null;
        private GameObject DescriptionTemplate = null;

        void Start()
        {
            // 레퍼런스를 모두 가져옴
            LevelText = transform.Find("Level").GetComponent<Text>();
            LabelText = transform.Find("Label").GetComponent<Text>();
            HpValue = transform.Find("HpValue").GetComponent<Text>();

            CardImage = transform.Find("Illust").GetComponent<Image>();
            CardImageMirror = transform.Find("IllustMirror").GetComponent<Image>();

            TagLayout = transform.Find("TagLayout").gameObject;
            TagTemplate = TagLayout.transform.Find("Tag").gameObject;

            DiceDescriptionLayout = transform.Find("DescriptionPanel/DicePanel/DiceDescriptionLayout").gameObject;
            DiceDescriptionTemplate = DiceDescriptionLayout.transform.Find("Text").gameObject;

            DescriptionLayout = transform.Find("DescriptionLayout/DescriptionPanel").gameObject;
            DescriptionTemplate = DescriptionLayout.transform.Find("Text").gameObject;
        }

        void Update()
        {

        }

        public void LoadCardData(string key)
        {
            card = CardFactory.Create(key);

            if (card is CharacterCard)
            {
                LevelText.text = ((CharacterCard)card).Level;
                HpValue.text = ((CharacterCard)card).Hp.ToString();
                return;
            }
        }
    }
}