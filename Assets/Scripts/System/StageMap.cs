using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DOW
{
    public class StageMap : MonoBehaviour
    {
        private List<StageData> stageDatas = null;

        // Start is called before the first frame update
        void Start()
        {
            stageDatas = TableManager.GetTable<StageTable>().GetAllList();

            stageDatas.ForEach((data) => {
                Debug.Log(data);
            });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}