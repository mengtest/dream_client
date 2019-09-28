
/***
  * Title:     
  *
  * Created:	zp
  *
  * CreatTime:  2019/09/11 11:22:14
  *
  * Description:
  *
  * Version:    0.1
  *
  *
***/

using UnityEngine.UI;

namespace Assets.Scripts.UI.MeunUI
{
    public class MsgPanel : UIBase
    {
        Button btnClose;
        private void Awake()
        {
            Bind(UIEvent.MSG_PANEL_ACTIVE);
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.MSG_PANEL_ACTIVE:
                    setPanelActive((bool)message);
                    break;
                default:
                    break;
            }
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            btnClose.onClick.RemoveAllListeners();
        }
        // Start is called before the first frame update
        void Start()
        {
            btnClose = transform.Find("bg/BtnClose").GetComponent<Button>();
            btnClose.onClick.AddListener(clickClose);
            setPanelActive(false);
        }
        private void clickClose()
        {
            setPanelActive(false);
        }
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
