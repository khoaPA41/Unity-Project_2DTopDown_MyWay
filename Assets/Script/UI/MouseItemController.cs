using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class MouseItemController : MonoBehaviour
{
    public static MouseItemController Instance;

    Image image;
    void Awake()
    {
        Instance = this;
        image = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void ShowGhostItem(Sprite sprite)
    {
        image.sprite = sprite;
        gameObject.SetActive(true);
    }

    public void HideGhostItem()
    {
        gameObject.SetActive(false);
    }



    void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.position = Mouse.current.position.ReadValue();
        }
    }
}
