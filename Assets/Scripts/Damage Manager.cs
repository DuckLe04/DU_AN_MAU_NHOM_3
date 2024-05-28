using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DamgeManager : MonoBehaviour
{
    private Checkzone checkzone;

    [SerializeField] public List<int> listBigDamage = new List<int>(); // Danh sách lưu CountDamage
    [SerializeField] public List<int> listSmallDamage = new List<int>(); // Danh sách lưu CountDamage

    [SerializeField] public bool bigDamage = false;
    [SerializeField] public bool smallDamaged = false;
    public int CountBigDamage = 0; // Biến đếm số lượng damage
    public int CountSmallDamage = 0; // Biến đếm số lượng damage

    private void Start()
    {
        checkzone = GetComponent<Checkzone>();
    }
    private void Update()
    {
        changeListDamage();
    }
    public void changeListDamage()
    {
        if (bigDamage == true) // add list big damage
        {
            CountBigDamage = checkzone.listBigZoneDamage.Count;
            if (!listBigDamage.Contains(CountBigDamage)) // kiem tra xem count trong listDamage co = countDamage hay khong
            {
                listBigDamage.Add(CountBigDamage);
            }
            Debug.Log("\t + max damage ");
            Debug.Log("Count in list big damage: " + listBigDamage.Count);
        }
        if (smallDamaged == true) // add list small damage
        {
            CountSmallDamage = checkzone.listSmallZoneDamage.Count;
            if (!listSmallDamage.Contains(CountSmallDamage)) // kiem tra xem count trong listDamage co = countDamage hay khong
            {
                listSmallDamage.Add(CountSmallDamage);
            }
            Debug.Log("\t + 1 damage ");
            Debug.Log("Count in list small damage: " + listSmallDamage.Count);
        }
    }
}
