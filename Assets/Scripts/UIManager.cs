using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public TMP_InputField blocksXInputField;
    public TMP_InputField blocksYInputField;

    public TMP_InputField widthInputField;
    public TMP_InputField heightInputField;

    public Button rebuildRoadButton;

    public TMP_InputField carInputField;
    public TMP_InputField busInputField;
    public TMP_InputField truckInputField;
    public TMP_InputField superCarInputField;

    public Button respawnCarsButton;

    public TMP_InputField pedestriansInputField;
    public Button respawnPedestriansButton;
}
