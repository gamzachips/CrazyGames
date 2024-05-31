using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour {
    public Sprite targetImage; // Image ������Ʈ
    public Sprite newImage;   // �ٲ� �̹���
    private Sprite originalImage; // ���� �̹��� ����

    private void Start() {
        // ������ �� ���� �̹����� �����ϴ� �ڵ�
        if (targetImage != null) {
            originalImage = targetImage;
        }
    }

    public void ChangeImage() {
        if (targetImage == originalImage) {
            Debug.Log("OriginalImage");
            GameObject.Find("BugReportButton").GetComponent<Image>().sprite = newImage;
            targetImage = newImage;
        }
        else {
            Debug.Log("NewImage");
            GameObject.Find("BugReportButton").GetComponent<Image>().sprite = originalImage;
            targetImage = originalImage;
        }

        // Button ���¸� ������Ʈ�Ͽ� Highlighted Sprite�� �ݿ��ǵ��� �մϴ�.
        Button button = GameObject.Find("BugReportButton").GetComponentInParent<Button>();
        if (button != null) {
            button.OnPointerExit(null); // �����Ͱ� ��ư�� ���� ���·� ����
            button.OnPointerEnter(null); // �����Ͱ� ��ư�� �� ���·� ����
        }
    }
}
