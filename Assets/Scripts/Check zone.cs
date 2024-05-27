using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Checkzone : MonoBehaviour
{
    private Player plS;
    private DamgeManager damageManager;
    private HeathManager heathManager;

    private Coroutine damageCoroutine;
    [SerializeField] public List<int> listBigZoneDamage = new List<int>(); // Danh sách lưu CountBigDamage
    [SerializeField] public List<int> listSmallZoneDamage = new List<int>(); // Danh sách lưu CountSmallDamage

    [SerializeField] public bool damageZone = false;
    [SerializeField] public bool gaiZone = false;
    [SerializeField] public bool waterZone = false;
    [SerializeField] public bool dieZone = false;
    [SerializeField] public bool dangerZone = false;
    
    void Start()
    {
        plS = GetComponent<Player>();
        damageManager = GetComponent<DamgeManager>();
        heathManager = GetComponent <HeathManager>();
    }

    void Update()
    {
        
    }
    private void LapManager()
    {
        #region lap damage
        if (damageZone == true)
        {
            Debug.Log(" start lap + damage");
            damageCoroutine = StartCoroutine(lapCongDamage()); // gọi hàm lặp + damage
        }
        //if (damageZone == false)
        //{
        //    Debug.Log(" stop lap + damage");
        //    StopCoroutine(damageCoroutine);
        //}
        #endregion

    }
    private void isDamageZone()
    {
        if (plS.isLifepPlayer == true && damageZone == true)
        {
            if (dieZone == true)
            {
                plS.isDamagePlayer = true;
                damageManager.bigDamage = true;
                Debug.Log(" player is GAI damage");
                LapManager();
            }
            if (dangerZone == true)
            {
                plS.isDamagePlayer = true;
                damageManager.smallDamaged = true;
                Debug.Log(" player is water damage");
                LapManager();
            }
        }
        else // nguoc lai => all false
        {
            dieZone = false;
            dangerZone = false;
            plS.isDamagePlayer = false;
            damageManager.bigDamage = false;
            damageManager.smallDamaged = false;
            heathManager.bigHeath = false;
            heathManager.smallHeath = false;
            Debug.Log(" player not dieDamage");
        }
    }
    private void checkTypeDamageManager()
    {
        if (gaiZone == true)
        {
            dieZone = true;
        }
        if (waterZone == true)
        {
            dangerZone = true;
        }
        isDamageZone();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("gai"))
        {
            damageZone = true;
            gaiZone = true;
            Debug.Log(" dinh gai");
            checkTypeDamageManager();
            //isDamageZone();
        }
        if (collision.gameObject.CompareTag("water"))
        {
            damageZone = true;
            waterZone = true;
            Debug.Log(" dinh water");
            checkTypeDamageManager();
            //isDamageZone();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("gai"))
        {
            damageZone = false;
            gaiZone = false;
            Debug.Log("KO dinh gai");
        }
        if (collision.gameObject.CompareTag("water"))
        {
            damageZone = false;
            waterZone = false;
            Debug.Log("KO dinh water");
        }
    }
    #region logic lặp
    private IEnumerator lapCongDamage() // lặp + damage
    {   
        if (plS.isDamagePlayer == true)
        {

            if (damageManager.bigDamage == true)
            {
                heathManager.bigHeath = true;
                int countBigZoneDamage = listBigZoneDamage.Count + 1;// tang count trong list die damage len 1
                listBigZoneDamage.Add(countBigZoneDamage);
                Debug.Log("CountDamage in list CHECKZONE: " + listBigZoneDamage.Count);
            }
            if (damageManager.smallDamaged == true) // lap small damage
            {
                heathManager.smallHeath = true;
                int countSmallZoneDamage = listSmallZoneDamage.Count + 1;// tang count trong list heath damage len 1
                listSmallZoneDamage.Add(countSmallZoneDamage);

                Debug.Log("CountDamage in list CHECKZONE: " + listSmallZoneDamage.Count);
            }
        }

        yield return new WaitForSeconds(1f); // lap lai sau 1s
        checkTypeDamageManager();
    }
    #endregion
}
