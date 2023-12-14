using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
  [SerializeField] private Sprite OpenInventory;
  [SerializeField] private Sprite CloseInventory;
  private Inventory _inventory;
  private UIManager _uiManager;


  private SpriteRenderer _sp;
  // Start is called before the first frame update
  void Start()
  {
    _inventory = GameObject.Find("Player").GetComponent<Inventory>();
    _sp = transform.GetComponent<SpriteRenderer>();
    _uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void HidingInventory()
  {
    _sp.sprite = CloseInventory;

  }
  public void ShowingInventory()
  {
    _sp.sprite = OpenInventory;

  }

  void OnMouseDown()
  {
    _inventory.CheckItemLocation();
    _uiManager.ShowInventory();
  }

}
