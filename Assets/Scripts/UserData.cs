using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class UserData
{   
    //유저 정보 데이터를 담기 위한 UserData 클래스 작성

    public string userName; //유저 이름
    public int cashAmount; //유저의 현금
    public int balanceAmount; //유저의 동장 잔액

    public UserData(string userName, int cashAmount, int balanceAmount)
    
    {
        this.userName = userName;
        this.cashAmount = cashAmount;
        this.balanceAmount = balanceAmount;
    }
    public void Deposit(int amount) // 입금
    {
        if (amount > 0 && amount <= cashAmount)
        {
            cashAmount -= amount;       // 현금 줄고
            balanceAmount += amount;    // 계좌 늘어남
            GameManager.instance.popupBank.Refresh();
            GameManager.instance.SaveUserData();   // JSON 저장
        }
        else
        {
            Debug.Log("잔액 부족!");
            GameManager.instance.popupBank.LackOfBalance.SetActive(true);
        }
    }

    public void Withdraw(int amount) // 출금
    {
        if (amount > 0 && amount <= balanceAmount)
        {
            balanceAmount -= amount;    // 계좌 줄고
            cashAmount += amount;       // 현금 늘어남
            GameManager.instance.popupBank.Refresh();
            GameManager.instance.SaveUserData();   // JSON 저장
        }
        else
        {
            Debug.Log("잔액 부족!");
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
