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
        private Text KeyText = null;

        private GameObject IllustLayout = null;

        private GameObject CardBorder = null;
        private GameObject CardBorderPlain = null;

        private GameObject TagLayout = null;
        private GameObject TagTemplate = null;

        private GameObject DiceDescriptionLayout = null;
        private GameObject DiceDescriptionTemplate = null;

        private GameObject DescriptionLayout = null;
        private GameObject DescriptionTemplate = null;

        private GameObject HpSlug = null;

        void Awake()
        {
            // 레퍼런스를 모두 가져옴
            LevelText = transform.Find("Level").GetComponent<Text>();
            LabelText = transform.Find("Label").GetComponent<Text>();
            HpValue = transform.Find("HpValue").GetComponent<Text>();
            KeyText = transform.Find("Key").GetComponent<Text>();

            IllustLayout = transform.Find("IllustLayout").gameObject;

            CardBorder = transform.Find("CardBorder").gameObject;
            CardBorderPlain = transform.Find("CardBorderPlain").gameObject;

            TagLayout = transform.Find("TagLayout").gameObject;
            TagTemplate = TagLayout.transform.Find("Tag").gameObject;

            DiceDescriptionLayout = transform.Find("InformationLayout/DicePanel/DiceDescriptionLayout").gameObject;
            DiceDescriptionTemplate = DiceDescriptionLayout.transform.Find("Text").gameObject;

            DescriptionLayout = transform.Find("InformationLayout/DescriptionLayout").gameObject;
            DescriptionTemplate = DescriptionLayout.transform.Find("Text").gameObject;

            HpSlug = transform.Find("HpSlug").gameObject;
        }

        void Update()
        {

        }

        public void LoadCardData(string key)
        {
            card = CardFactory.Create(key);

            LabelText.text = card.Label;
            KeyText.text = card.Key;
            loadIllust();

            if (card is CharacterCard characterCard)
            {
                setCharacterCardLayout();
                LevelText.text = characterCard.Level;
                HpValue.text = characterCard.Hp.ToString();
            } else { 
                setPlainCardLayout();
            } 
        }

        private void loadIllust()
        {
            if (card == null) return;
            if (IllustLayout == null) return;

            // drop all children of illust layout
            foreach (Transform child in IllustLayout.transform)
            {
                Destroy(child.gameObject);
            }

            var illust = ResourceManager.GetResource<GameObject>(eResourcePath.ILLUST, card.Illust);
            
            var illust1 = Instantiate(illust, IllustLayout.transform);
            var illust2 = Instantiate(illust, IllustLayout.transform);

            // set illust1 rect transform to stretch top, height 180, posy -90
            illust1.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            illust1.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            illust1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -90);
            illust1.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 180);

            // set illust2 rect transform to stretch bottom, height 180, posy 90
            illust2.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            illust2.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
            illust2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 90);
            illust2.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 180);

            // set illust2 to y scale -1
            illust2.GetComponent<RectTransform>().localScale = new Vector3(1, -1, 1);
        }

        private void loadTag()
        {
            if (card == null) return;
            if (TagLayout == null) return;

            // drop all children of tag layout
            foreach (Transform child in TagLayout.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (var tag in card.Tags)
            {
                var tagObject = Instantiate(TagTemplate, TagLayout.transform);
                tagObject.GetComponent<Text>().text = tag.ToString();
            }
        }

        private void loadSkill() 
        {

        }

        private void loadDiceSkill() 
        {

        }

        private void setCharacterCardLayout()
        {
            LevelText.gameObject.SetActive(true);
            HpValue.gameObject.SetActive(true);
            HpSlug.SetActive(true);
            CardBorder.SetActive(true);
            CardBorderPlain.SetActive(false);

            LabelText.rectTransform.anchoredPosition = new Vector2(120, LabelText.rectTransform.anchoredPosition.y);
        }

        private void setPlainCardLayout() {
            LevelText.gameObject.SetActive(false);
            HpValue.gameObject.SetActive(false);
            HpSlug.SetActive(false);
            CardBorder.SetActive(false);
            CardBorderPlain.SetActive(true);

            LabelText.rectTransform.anchoredPosition = new Vector2(85, LabelText.rectTransform.anchoredPosition.y);
        }
    }
}