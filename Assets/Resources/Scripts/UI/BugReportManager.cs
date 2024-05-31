using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BugReportManager : MonoBehaviour {
    public Button bugReportButton;
    public GameObject bugReport;
    public Sprite defaultImage;
    public Sprite hoverImage;

    private bool isBugReportActive = false;
    private Image buttonImage;

    void Start() {
        buttonImage = bugReportButton.GetComponent<Image>();
        buttonImage.sprite = defaultImage;

        bugReportButton.onClick.AddListener(ToggleBugReport);
        bugReportButton.gameObject.AddComponent<HoverListener>().Init(buttonImage, defaultImage, hoverImage);

        // CloseButton �ʱ�ȭ
        Button closeButton = GameObject.Find("BugReport").GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(CloseBugReport);

        // ������ �� BugReport ��Ȱ��ȭ
        bugReport.SetActive(false);
    }

    private void ToggleBugReport() {
        isBugReportActive = !isBugReportActive;
        bugReport.SetActive(isBugReportActive);
    }

    private void CloseBugReport() {
        isBugReportActive = false;
        bugReport.SetActive(false);
        buttonImage.sprite = defaultImage;
    }
}

public class HoverListener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private Image targetImage;
    private Sprite defaultSprite;
    private Sprite hoverSprite;

    public void Init(Image target, Sprite defaultSpr, Sprite hoverSpr) {
        targetImage = target;
        defaultSprite = defaultSpr;
        hoverSprite = hoverSpr;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (targetImage != null && targetImage.sprite == defaultSprite) {
            targetImage.sprite = hoverSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (targetImage != null && targetImage.sprite == hoverSprite) {
            targetImage.sprite = defaultSprite;
        }
    }
}
