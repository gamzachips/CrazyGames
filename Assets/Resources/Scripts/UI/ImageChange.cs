using UnityEngine;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour {
    public Sprite targetImage; // Image 컴포넌트
    public Sprite newImage;   // 바뀔 이미지
    private Sprite originalImage; // 원래 이미지 저장

    private void Start() {
        // 시작할 때 원래 이미지를 저장하는 코드
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

        // Button 상태를 업데이트하여 Highlighted Sprite가 반영되도록 합니다.
        Button button = GameObject.Find("BugReportButton").GetComponentInParent<Button>();
        if (button != null) {
            button.OnPointerExit(null); // 포인터가 버튼을 떠난 상태로 설정
            button.OnPointerEnter(null); // 포인터가 버튼에 들어간 상태로 설정
        }
    }
}
