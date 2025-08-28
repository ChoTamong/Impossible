using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class UserData
{   
    //���� ���� �����͸� ��� ���� UserData Ŭ���� �ۼ�

    public string userName; //���� �̸�
    public int cashAmount; //������ ����
    public int balanceAmount; //������ ���� �ܾ�

    public UserData(string userName, int cashAmount, int balanceAmount)
    
    {
        this.userName = userName;
        this.cashAmount = cashAmount;
        this.balanceAmount = balanceAmount;
    }
    public void Deposit(int amount) // �Ա�
    {
        if (amount > 0 && amount <= cashAmount)
        {
            cashAmount -= amount;       // ���� �ٰ�
            balanceAmount += amount;    // ���� �þ
            GameManager.instance.popupBank.Refresh();
            GameManager.instance.SaveUserData();   // JSON ����
        }
        else
        {
            Debug.Log("�ܾ� ����!");
            GameManager.instance.popupBank.LackOfBalance.SetActive(true);
        }
    }

    public void Withdraw(int amount) // ���
    {
        if (amount > 0 && amount <= balanceAmount)
        {
            balanceAmount -= amount;    // ���� �ٰ�
            cashAmount += amount;       // ���� �þ
            GameManager.instance.popupBank.Refresh();
            GameManager.instance.SaveUserData();   // JSON ����
        }
        else
        {
            Debug.Log("�ܾ� ����!");
            GameManager.instance.popupBank.LackOfBalance.SetActive(true);
        }
    }

    public void ResetData(string name, int cash, int balance)
    {
        userName = name;
        cashAmount = cash;
        balanceAmount = balance;
        GameManager.instance.popupBank.Refresh();
    }
}
