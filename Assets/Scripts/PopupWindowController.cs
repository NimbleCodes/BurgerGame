using System;
using UnityEngine;
using UnityEngine.UI;


/// 팝업 윈도우를 띄워주는 클래스
/// Ok 팝업, Ok/Cancel 팝업, Yes/No 팝업, Yes/No/Cancel 팝업 

public class PopupWindowController : MonoBehaviour
{
    public static PopupWindowController Instance; // singleton 변수
    public string ScenetoLoad;

    public bool useBackground = true;
    public string DefaultOkName = "Ok";         // 기본 Ok 텍스트
    [SerializeField]
    private GameObject background;  // 배경 패널
    [SerializeField]
    private GameObject popupWindow; // 팝업 윈도우 패널

    [SerializeField]
    private Button okButton;        // Ok 버튼
    [SerializeField]
    private Text okText;            // Ok 텍스트
    [SerializeField]
    private Text titleText;         // Title 텍스트
    [SerializeField]
    private Text messageText;       // Message 텍스트


    private Action okAction;        // Ok 이벤트
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != null)
            Destroy(gameObject);
    }

    private void Start()
    {
        ClosePopupWindow();
    }

    private void ClosePopupWindow()
    {
        // 이벤트 초기화
        okAction = null;
        // 버튼 비활성화
        okButton.gameObject.SetActive(false);
        // 배경 비활성화
        if(useBackground)
            background.SetActive(false);
        // 팝업 윈도우 비활성화
        popupWindow.SetActive(false);

        // 텍스트 초기화
        okText.text = DefaultOkName;
    }

    private void OpenPopupWindow()
    {
        // 배경 활성화
        if (useBackground)
            background.SetActive(true);
        // 팝업 윈도우 활성화
        popupWindow.SetActive(true);
    }

    public void ShowOk(string title, string message, Action okAction = null)
    {
        // 이벤트 등록
        this.okAction = okAction;

        // 타이틀 및 메시지 설정
        titleText.text = title;
        messageText.text = message;

        // 버튼 활성화
        okButton.gameObject.SetActive(true);
        okButton.Select();

        // 버튼 네비게이션 설정
        Navigation navigation = new Navigation();

        navigation.selectOnLeft = null;
        navigation.selectOnRight = null;
        okButton.navigation = navigation;

        // 팝업 윈도우 활성화
        OpenPopupWindow();
    }
    public void SetOkText(string okName)
    {
        okText.text = okName;
    }

  
    public void OnClickOkButton()
    {
        if (okAction != null)
            okAction();

        ClosePopupWindow();
    }
}