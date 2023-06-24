using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DOW
{
    public class ChapterMap : MonoBehaviour
    {
        [SerializeField, Min(1)]
        public int CurrentStage = 1;

        [SerializeField, Range(2, 5)]
        public int MaxDisplayCount = 3; // 최대 표시 갯수

        [SerializeField]
        public GameObject Layout = null; // chapter 카드 레이아웃

        [SerializeField]
        public GameObject ChapterCard = null; // chapter 카드

        [SerializeField]
        public GameObject Chevron = null; // 화살표

        private List<ChapterData> chapterData = null;
        private string currentChapter = null;

        void Start()
        {
            currentChapter = TableManager.GetTable<StageTable>().Get("" + CurrentStage).Chapter;
            chapterData = TableManager.GetTable<ChapterTable>().GetAllList();

            int currentChapterIndex = int.Parse(currentChapter) - 1;
            int windowStartIndex = currentChapterIndex - ((MaxDisplayCount + 1) / 2 - 1);
            int windowEndIndex = currentChapterIndex + MaxDisplayCount / 2;
            if (windowStartIndex < 0)
            {
                windowStartIndex = 0;
                windowEndIndex = MaxDisplayCount - 1;
            }
            else if (windowEndIndex > chapterData.Count - 1)
            {
                windowStartIndex = chapterData.Count - MaxDisplayCount;
                windowEndIndex = chapterData.Count - 1;
            }
            bool prevExist = windowStartIndex > 0;
            bool nextExist = windowEndIndex < chapterData.Count - 1;

            // assinge chapter card
            for (int i = windowStartIndex; i <= windowEndIndex; ++i)
            {
                if (i == windowStartIndex && prevExist)
                {
                    GameObject chevron = Instantiate(Chevron, Layout.transform);
                }

                // instantiate chapter card
                GameObject chapterCard = Instantiate(ChapterCard, Layout.transform);

                // set label
                chapterCard.GetComponentInChildren<Text>().text = chapterData[i].Label;
                
                // set marker
                if (i != chapterData.Count - 1) {
                    GameObject marker = chapterCard.transform.Find("Marker").gameObject;
                    marker.SetActive(false);
                }

                // TODO: set sprite

                if (i < windowEndIndex)
                {
                    GameObject chevron = Instantiate(Chevron, Layout.transform);
                }
                if (i == windowEndIndex && nextExist) {
                    GameObject chevron = Instantiate(Chevron, Layout.transform);
                }
            }

            // disable templates
            ChapterCard.SetActive(false);
            Chevron.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PopupManager.ClosePopup<ChapterMapPopup>();
            }
        }
    }
}