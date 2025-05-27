using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{

    private RaycastHit hitInfo;
    public int getCoinCount;
    public Text textUI;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray screenRay = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(screenRay.origin, screenRay.direction * 1000f, out hitInfo))
            {
                if (hitInfo.collider.CompareTag("COIN"))
                {
                    hitInfo.collider.gameObject.SetActive(false);
                    getCoinCount++;

                    textUI.text = "현재까지 획득한 코인의 수 : " + getCoinCount;
                }
            }
        }        
    }
}
