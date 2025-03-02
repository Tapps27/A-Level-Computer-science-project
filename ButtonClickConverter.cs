using UnityEngine;
using UnityEngine.UI;

public class ButtonKeyBinder : MonoBehaviour
{
    public Button button1; // Assign in Inspector
    public Button button2; // Assign in Inspector
    public Button button3; // Assign in Inspector

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) // Press '1' for first button
        {
            button1.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) // Press '2' for second button
        {
            button2.onClick.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) // Press '3' for third button
        {
            button3.onClick.Invoke();
        }
    }
}
