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

        private GameObject DiceInfoPanel = null;
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

            DiceInfoPanel = transform.Find("InformationLayout/DiceInfoPanel").gameObject;
            DiceDescriptionLayout = transform.Find("InformationLayout/DiceInfoPanel/DiceDescriptionLayout").gameObject;
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
            Debug.Log(card);

            LabelText.text = card.Label;
            KeyText.text = card.Key;
            loadIllust();
            loadTag();
            loadSkill();

            if (card is LevelingCard characterCard)
            {
                loadDiceSkill();

                setCharacterCardLayout();

                LevelText.text = characterCard.Level;
                HpValue.text = characterCard.Hp.ToString();
            }
            else
            {
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
            GameObject illust1, illust2;

            if (illust == null)
            {
                illust1 = ResourceManager.GetResource<GameObject>(eResourcePath.ILLUST, "9999");
                illust2 = ResourceManager.GetResource<GameObject>(eResourcePath.ILLUST, "9999");
            }
            else
            {
                illust1 = Instantiate(illust, IllustLayout.transform);
                illust2 = Instantiate(illust, IllustLayout.transform);
            }

            // set illust1 rect transform to stretch top, height 180, posy -90
            illust1.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            illust1.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            illust1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -90);
            illust1.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 180);

            // set illust1 to y scale 1
            illust1.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

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

            TagTemplate.SetActive(true);

            // drop all children of tag layout
            foreach (Transform child in TagLayout.transform)
            {
                if (ReferenceEquals(child.gameObject, TagTemplate)) continue;
                Destroy(child.gameObject);
            }

            if (card.CardPack != null)
            {
                var cardPackTag = Instantiate(TagTemplate, TagLayout.transform);
                cardPackTag.transform.Find("Text").GetComponent<Text>().text = card.CardPack.Label;
            }

            if (card.Type != eCardType.NONE)
            {
                var cardTypeTag = Instantiate(TagTemplate, TagLayout.transform);
                string slug = "";

                switch (card.Type)
                {
                    case eCardType.CHAR:
                        slug = "캐릭터카드";
                        break;
                    case eCardType.FIELD:
                        slug = "필드카드";
                        break;
                    case eCardType.INST:
                        slug = "즉발카드";
                        break;
                    case eCardType.ITEM:
                        slug = "아이템카드";
                        break;
                    case eCardType.ENEMY:
                        slug = "적";
                        break;
                    default:
                        slug = "알수없음";
                        break;
                }

                cardTypeTag.transform.Find("Text").GetComponent<Text>().text = slug;
            }

            foreach (var tag in card.Tags)
            {
                var tagObject = Instantiate(TagTemplate, TagLayout.transform);
                tagObject.transform.Find("Text").GetComponent<Text>().text = tag.Label;
            }

            TagTemplate.SetActive(false);
        }

        private void loadSkill()
        {
            if (card == null) return;
            if (DescriptionLayout == null) return;

            DescriptionTemplate.SetActive(true);

            // drop all children of description layout
            foreach (Transform child in DescriptionLayout.transform)
            {
                if (ReferenceEquals(child.gameObject, DescriptionTemplate)) continue;
                Destroy(child.gameObject);
            }

            foreach (var skill in card.Skills)
            {
                if (skill.Type.Contains(eSkillType.DICE)) continue;
                var description = Instantiate(DescriptionTemplate, DescriptionLayout.transform);
                description.GetComponent<Text>().text = skill.DescriptionText(card);
            }

            DescriptionTemplate.SetActive(false);
        }

        private void loadDiceSkill()
        {
            if (card == null) return;
            if (DiceDescriptionLayout == null) return;

            DiceDescriptionTemplate.SetActive(true);

            // drop all children of dice description layout
            foreach (Transform child in DiceDescriptionLayout.transform)
            {
                if (ReferenceEquals(child.gameObject, DiceDescriptionTemplate)) continue;
                Destroy(child.gameObject);
            }

            foreach (var skill in (card as LevelingCard).Skills)
            {
                if (!skill.Type.Contains(eSkillType.DICE)) continue;
                var diceDescription = Instantiate(DiceDescriptionTemplate, DiceDescriptionLayout.transform);
                diceDescription.GetComponent<Text>().text = skill.DescriptionText(card);
            }

            DiceDescriptionTemplate.SetActive(false);
        }

        private void setCharacterCardLayout()
        {
            LevelText.gameObject.SetActive(true);
            HpValue.gameObject.SetActive(true);
            HpSlug.SetActive(true);
            CardBorder.SetActive(true);
            CardBorderPlain.SetActive(false);
            DiceInfoPanel.SetActive(true);

            LabelText.rectTransform.anchoredPosition = new Vector2(120, LabelText.rectTransform.anchoredPosition.y);
        }

        private void setPlainCardLayout()
        {
            LevelText.gameObject.SetActive(false);
            HpValue.gameObject.SetActive(false);
            HpSlug.SetActive(false);
            CardBorder.SetActive(false);
            CardBorderPlain.SetActive(true);
            DiceInfoPanel.SetActive(false);

            LabelText.rectTransform.anchoredPosition = new Vector2(85, LabelText.rectTransform.anchoredPosition.y);
        }
    }
}