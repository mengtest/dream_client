using Assets.Scripts.Audio;
using Assets.Scripts.Framework;
using Assets.Scripts.Net;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuUI
{
    /// <summary>
    /// 设置面板
    /// </summary>
    public class SetPanel : UIBase
    {
        private GameObject MusicClik;               //点击状态的音效按钮
        private GameObject SecutiryClik;            //点击状态的安全设置按钮
        private GameObject HelpClik;                //点击状态的帮助按钮
        private GameObject PanelSound;              //音效设置面板
        private GameObject panelSecutiry;           //安全设置面板
        private GameObject PanelHelp;               //帮助设置面板
        private GameObject SetTrading;              //设置交易密码面板
        private GameObject ChangeTrading;           //修改交易密码面板
        private Button btnSecutiry;                 //设置按钮  
        private Button SoundBtn;                    //音效开关
        private Button MusicBtn;                    //背景音乐开关
        private Image SoundImg;                     //声音图
        private Image MusicImg;                     //背景音乐图
        private Sprite[] switchSprite = new Sprite[2];              //开关图
        private Text soundTxt;                      //音效提示 
        private Text musicTxt;                      //背景音乐提示
        private Text TransactionCode;                //交易码设置
        private Text ChangeLoginPassword;            //修改登录密码
        private Text LogOut;                         //退出登录
        private int setUp;                           //是否设置了交易吗(默认是设置)

        Button btnPanelMusic;
        Button btnHelp;
        Button btnChangePW;
        Button btnChangeExPW;
        Button btnExit;
        Button btnClose;

        private bool IsOpenSound = false;          //是否开启音效
        private bool IsOpenMuisc = false;          //是否开启背景音效
        private bool isVoice = true;
        private void Awake()
        {
            Bind(UIEvent.SET_PANEL_ACTIVE);
        }

        protected internal override void Execute(int eventCode, object message)
        {
            switch (eventCode)
            {
                case UIEvent.SET_PANEL_ACTIVE:
                    setPanelActive((bool)message);
                    break;
                default:
                    break;
            }
        }

        private void Start()
        {
            btnPanelMusic = transform.Find("bg/BtnPanelMusic").GetComponent<Button>();
            btnSecutiry = transform.Find("bg/BtnSecutiry").GetComponent<Button>();
            btnHelp = transform.Find("bg/BtnHelp").GetComponent<Button>();
            btnChangeExPW = transform.Find("panelSecutiry/BtnChangeExPW").GetComponent<Button>();
            btnChangePW = transform.Find("panelSecutiry/BtnChangePW").GetComponent<Button>();
            btnExit = transform.Find("panelSecutiry/BtnExit").GetComponent<Button>();
            btnClose = transform.Find("bg/BtnClose").GetComponent<Button>();
            SetTrading = transform.parent.Find("SetExPwPanel").gameObject;
            ChangeTrading = transform.parent.Find("ChangeExPwPanel").gameObject;
            btnClose.onClick.AddListener(clickClose);
            btnPanelMusic.onClick.AddListener(clickMusic);
            btnExit.onClick.AddListener(clickExit);
            btnSecutiry.onClick.AddListener(ClickSecutiry);
            btnHelp.onClick.AddListener(ClickHelp);


            MusicClik = transform.Find("bg/MusicClik").gameObject;
            SecutiryClik = transform.Find("bg/SecutiryClik").gameObject;
            SecutiryClik.SetActive(false);
            HelpClik = transform.Find("bg/HelpClik").gameObject;
            HelpClik.SetActive(false);
            PanelSound = transform.Find("PanelSound").gameObject;
            panelSecutiry = transform.Find("panelSecutiry").gameObject;
            PanelHelp = transform.Find("PanelHelp").gameObject;
            panelSecutiry.SetActive(false);
            PanelHelp.SetActive(false);
            SoundBtn = PanelSound.transform.Find("BtnGameMusic ").GetComponent<Button>();
            MusicBtn = PanelSound.transform.Find("BtnBgMusic").GetComponent<Button>();
            SoundBtn.onClick.AddListener(SoundClick);
            MusicBtn.onClick.AddListener(MuiscClick);
            SoundImg = SoundBtn.GetComponent<Image>();
            MusicImg = MusicBtn.GetComponent<Image>();
            for (int i = 0; i < switchSprite.Length; i++)
            {
                switchSprite[i] = Resources.Load<Sprite>("UI/Switch" + i);
            }
            ChangeLoginPassword = panelSecutiry.transform.Find("BtnChangePW/Text").GetComponent<Text>();
            LogOut = panelSecutiry.transform.Find("BtnExit/Text").GetComponent<Text>();
            TransactionCode = panelSecutiry.transform.Find("BtnChangeExPW/Text").GetComponent<Text>();
            switch (setUp)
            {
                case 0:
                    TransactionCode.text = "设置交易密码";
                    break;
                case 1:
                    TransactionCode.text = "修改交易密码";
                    break;
            }
            btnChangeExPW.onClick.AddListener(clickChangeTradePassword);
            btnChangePW.onClick.AddListener(clickChangeLoginPassword);

            setPanelActive(false);
            ChangeImg();
        }

        /// <summary>
        /// 多语系换图片
        /// </summary>
        private void ChangeImg()
        {
            string language = PlayerPrefs.GetString("language");
            MusicClik.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu" + language + "/SoundBig");
            SecutiryClik.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu" + language + "/SecuritySettingsBig");
            HelpClik.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu" + language + "/HelpBig");
            btnSecutiry.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu" + language + "/SecuritySettings");
            btnPanelMusic.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu" + language + "/Sound");
            btnHelp.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu" + language + "/Help");
            btnChangePW.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu" + language + "/ChangeLoginPassword");
            btnChangeExPW.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu" + language + "/ChangeTradePassword");
            btnExit.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/menu" + language + "/LoggedOut");
            SoundImg.sprite = Resources.Load<Sprite>("UI/menu" + language + "/Switch0");
            MusicImg.sprite = Resources.Load<Sprite>("UI/menu" + language + "/Switch1");
            soundTxt = PanelSound.transform.Find("Effect/Text").GetComponent<Text>();
            musicTxt = PanelSound.transform.Find("music/Text").GetComponent<Text>();
            ChangeLoginPassword.text = "";
            LogOut.text = "";

        }
        /// <summary>
        /// 音效设置
        /// </summary>
        private void clickMusic()
        {
            Dispatch(AreaCode.UI, UIEvent.VOICE_PANEL_ACTIVE, true);
            PanelSound.SetActive(true);
            panelSecutiry.SetActive(false);
            PanelHelp.SetActive(false);
            MusicClik.SetActive(true);
            SecutiryClik.SetActive(false);
            HelpClik.SetActive(false);
        }
        /// <summary>
        /// 安全设置按钮
        /// </summary>
        private void ClickSecutiry()
        {
            panelSecutiry.SetActive(true);
            PanelHelp.SetActive(false);
            PanelSound.SetActive(false);
            SecutiryClik.SetActive(true);
            HelpClik.SetActive(false);
            MusicClik.SetActive(false);
        }
        /// <summary>
        /// 帮助按钮
        /// </summary>
        private void ClickHelp()
        {
            panelSecutiry.SetActive(false);
            PanelHelp.SetActive(true);
            PanelSound.SetActive(false);
            SecutiryClik.SetActive(false);
            HelpClik.SetActive(true);
            MusicClik.SetActive(false);
        }
        /// <summary>
        /// 设置音效
        /// </summary>
        private void SoundClick()
        {
            if (!IsOpenSound)
            {
                SoundImg.sprite = switchSprite[1];
                IsOpenSound = !IsOpenSound;
                //把所有音效开启
                Dispatch(AreaCode.AUDIO, AudioEvent.PLAY_EFFECT_AUDIO, "");
            }
            else
            {
                SoundImg.sprite = switchSprite[0];
                IsOpenSound = !IsOpenSound;
                //把所有音效关闭
            }
        }
        /// <summary>
        /// 设置背景音乐
        /// </summary>
        private void MuiscClick()
        {
            if (!IsOpenMuisc)
            {
                MusicImg.sprite = switchSprite[1];
                IsOpenMuisc = !IsOpenMuisc;
                //把背景音乐开启
            }
            else
            {
                MusicImg.sprite = switchSprite[0];
                IsOpenMuisc = !IsOpenMuisc;
                //把背景音乐关闭
            }
        }
        private void clickChangeTradePassword()
        {
            switch (setUp)
            {
                case 0:
                    Dispatch(AreaCode.UI, UIEvent.SETTRANSACT_ACTIVE, true);
                    break;
                case 1:
                    Dispatch(AreaCode.UI, UIEvent.SETTRANSACT_ACTIVE, true);
                    break;
            }
        }
        private void clickChangeLoginPassword()
        {
            Dispatch(AreaCode.UI, UIEvent.CHANGELONG_ACTIVE, true);
        }
        /// <summary>
        /// 退出按钮
        /// </summary>
        private void clickExit()
        {
            Dispatch(AreaCode.NET, ReqEventType.exit, null);
            Application.Quit();
        }
        /// <summary>
        /// 关闭设置按钮
        /// </summary>
        private void clickClose()
        {
            Dispatch(AreaCode.UI, UIEvent.VOICE_PANEL_ACTIVE, false);
            setPanelActive(false);
        }
        public override void OnDestroy()
        {
            base.OnDestroy();
            btnClose.onClick.RemoveAllListeners();
            //btnMusic.onClick.RemoveAllListeners();
            //btnHelp.onClick.RemoveAllListeners();
            //btnChangeExPW.onClick.RemoveAllListeners();
            //btnChangePW.onClick.RemoveAllListeners();
        }

    }
}
