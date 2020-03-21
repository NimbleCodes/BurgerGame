using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 팝업 윈도우를 띄워주는 클래스
/// Ok 팝업, Ok/Cancel 팝업, Yes/No 팝업, Yes/No/Cancel 팝업 
/// </summary>
public class PopupWindowController : MonoBehaviour
{
    public static PopupWindowController Instance; // singleton 변수

    public bool useBackground = true;
    public string DefaultOkName = "Ok";         // 기본 Ok 텍스트
    public string DefaultCancelName = "Cancel"; // 기본 Cancel 텍스트
    public string DefaultYesName = "Yes";       // 기본 Yes 텍스트
    public string DefaultNoName = "No";         // 기본 No 텍스트

    [SerializeField]
    private GameObject background;  // 배경 패널
    [SerializeField]
    private GameObject popupWindow; // 팝업 윈도우 패널

    [SerializeField]
    private Button okButton;        // Ok 버튼
    [SerializeField]
    private Button cancelButton;    // Cancel 버튼
    [SerializeField]
    private Button yesButton;       // Yes 버튼
    [SerializeField]
    private Button noButton;        // No 버튼

    [SerializeField]
    private Text okText;            // Ok 텍스트
    [SerializeField]
    private Text cancelText;        // Cancel 텍스트
    [SerializeField]
    private Text yesText;           // Yes 텍스트
    [SerializeField]
    private Text noText;            // No 텍스트
    [SerializeField]
    private Text titleText;         // Title 텍스트
    [SerializeField]
    private Text messageText;       // Message 텍스트


    private Action okAction;        // Ok 이벤트
    private Action cancelAction;    // Cancel 이벤트
    private Action yesAction;       // Yes 이벤트
    private Action noAction;        // No 이벤트

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
        cancelAction = null;
        yesAction = null;
        noAction = null;

        // 버튼 비활성화
        okButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);

        // 배경 비활성화
        if(useBackground)
            background.SetActive(false);
        // 팝업 윈도우 비활성화
        popupWindow.SetActive(false);

        // 텍스트 초기화
        okText.text = DefaultOkName;
        cancelText.text = DefaultCancelName;
        yesText.text = DefaultYesName;
        noText.text = DefaultNoName;
    }

    private void OpenPopupWindow()
    {
        // 배경 활성화
        if (useBackground)
            background.SetActive(true);
        // 팝업 윈도우 활성화
        popupWindow.SetActive(true);
    }

    /// <summary>
    /// Ok 팝업 윈도우 활성화
    /// </summary>
    /// <param name="title">타이틀</param>
    /// <param name="message">메시지</param>
    /// <param name="okAction">Ok 이벤트</param>
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

    /// <summary>
    /// Ok/Cancel 팝업 윈도우 활성화
    /// </summary>
    /// <param name="title">타이틀</param>
    /// <param name="message">메시지</param>
    /// <param name="okAction">Ok 이벤트</param>
    /// <param name="cancelAction">Cancel 이벤트</param>
    public void ShowOkCancel(string title, string message, Action okAction = null, Action cancelAction = null)
    {
        // 이벤트 등록
        this.okAction = okAction;
        this.cancelAction = cancelAction;

        // 타이틀 및 메시지 설정
        titleText.text = title;
        messageText.text = message;

        // 버튼 활성화
        okButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
        cancelButton.Select();

        // 버튼 네비게이션 설정
        Navigation navigation = new Navigation();

        navigation.selectOnLeft = cancelButton;
        navigation.selectOnRight = cancelButton;
        okButton.navigation = navigation;

        navigation.selectOnLeft = okButton;
        navigation.selectOnRight = okButton;
        cancelButton.navigation = navigation;


        // 팝업 윈도우 활성화
        OpenPopupWindow();
    }

    /// <summary>
    /// Yes/No 팝업 윈도우 활성화
    /// </summary>
    /// <param name="title">타이틀</param>
    /// <param name="message">메시지</param>
    /// <param name="yesAction">Yes 이벤트</param>
    /// <param name="noAction">No 이벤트</param>
    public void ShowYesNo(string title, string message, Action yesAction = null, Action noAction = null)
    {
        // 이벤트 등록
        this.yesAction = yesAction;
        this.noAction = noAction;

        // 타이틀 및 메시지 설정
        titleText.text = title;
        messageText.text = message;

        // 버튼 활성화
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
        noButton.Select();

        // 버튼 네비게이션 설정
        Navigation navigation = new Navigation();

        navigation.selectOnLeft = noButton;
        navigation.selectOnRight = noButton;
        yesButton.navigation = navigation;

        navigation.selectOnLeft = yesButton;
        navigation.selectOnRight = yesButton;
        noButton.navigation = navigation;

        // 팝업 윈도우 활성화
        OpenPopupWindow();
    }

    /// <summary>
    /// Yes/No/Cancel 팝업 윈도우 활성화
    /// </summary>
    /// <param name="title">타이틀</param>
    /// <param name="message">메시지</param>
    /// <param name="yesAction">Yes 이벤트</param>
    /// <param name="noAction">No 이벤트</param>
    /// <param name="cancelAction">Cancel 이벤트</param>
    public void ShowYesNoCancel(string title, string message, Action yesAction = null, Action noAction = null, Action cancelAction = null)
    {
        // 이벤트 등록
        this.yesAction = yesAction;
        this.noAction = noAction;
        this.cancelAction = cancelAction;

        // 타이틀 및 메시지 설정
        titleText.text = title;
        messageText.text = message;

        // 버튼 활성화
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
        cancelButton.gameObject.SetActive(true);
        cancelButton.Select();


        // 버튼 네비게이션 설정
        Navigation navigation = new Navigation();

        navigation.selectOnLeft = cancelButton;
        navigation.selectOnRight = noButton;
        yesButton.navigation = navigation;

        navigation.selectOnLeft = yesButton;
        navigation.selectOnRight = cancelButton;
        noButton.navigation = navigation;

        navigation.selectOnLeft = noButton;
        navigation.selectOnRight = yesButton;
        cancelButton.navigation = navigation;

        // 팝업 윈도우 활성화
        OpenPopupWindow();
    }

    /// <summary>
    /// Ok 텍스트 설정
    /// </summary>
    /// <param name="okName">설정할 이름</param>
    public void SetOkText(string okName)
    {
        okText.text = okName;
    }

    /// <summary>
    /// Cancel 텍스트 설정
    /// </summary>
    /// <param name="cancelName">설정할 이름</param>
    public void SetCancelText(string cancelName)
    {
        cancelText.text = cancelName;
    }

    /// <summary>
    /// Yes 텍스트 설정
    /// </summary>
    /// <param name="yesName">설정할 이름</param>
    public void SetYesText(string yesName)
    {
        yesText.text = yesName;
    }

    /// <summary>
    /// No 텍스트 설정
    /// </summary>
    /// <param name="noName">설정할 이름</param>
    public void SetNoText(string noName)
    {
        noText.text = noName;
    }

    public void OnClickOkButton()
    {
        if (okAction != null)
            okAction();

        ClosePopupWindow();
    }

    public void OnClickCancelButton()
    {
        if (cancelAction != null)
            cancelAction();

        ClosePopupWindow();
    }

    public void OnClickYesButton()
    {
        if (yesAction != null)
            yesAction();

        ClosePopupWindow();
    }

    public void OnClickNoButton()
    {
        if (noAction != null)
            noAction();

        ClosePopupWindow();
    }
}