using System;
using UnityEngine;

public class ShopSceneManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private Transform openShopText;
    [SerializeField] private StoreManager storeManager;
    bool open = false;

    private void Update()
    {
        if (open && Input.GetKeyDown(KeyCode.F))
        {
            storeManager.OpenShop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player")) return;
        animator.SetBool("Open", true);
        open = true;
        openShopText.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.gameObject.CompareTag("Player")) return;
        animator.SetBool("Open", false);
        open = false;
        openShopText.gameObject.SetActive(false);
    }
}
