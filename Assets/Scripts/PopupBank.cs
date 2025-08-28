using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    public TextMeshProUGUI Balance;
    public TextMeshProUGUI Cash;
    public TextMeshProUGUI Name;

    [Header("Panels")]
    public GameObject DepositPanel;
    public GameObject WithdrawPanel;
    public GameObject MainPanel;

    [Header("Alerts")]
    public GameObject LackOfBalance;

    [Header("InputFields")]
    public InputField depositInputField;
    public InputField withdrawInputField;

    // Start is called before the first frame update
    public void Refresh()
    {
        Balance.text = "Balance " + String.Format("{0:#,###; -#,###;0}", GameManager.instance.userData.balanceAmount);
        Cash.text = String.Format("{0:#,###; -#,###;0}", GameManager.instance.userData.cashAmount);
        Name.text = (GameManager.instance.userData.userName);
    }
    public void Start()
    {
        Refresh();

        // 시작할 땐 메인 패널만 켜고 나머지는 꺼둠
        MainPanel.SetActive(true);
        DepositPanel.SetActive(false);
        WithdrawPanel.SetActive(false);
        LackOfBalance.SetActive(false);

        depositInputField.onValidateInput += ValidateNumeric;
        withdrawInputField.onValidateInput += ValidateNumeric;
    }
    private char ValidateNumeric(string text, int charIndex, char addedChar)
    {
        return char.IsDigit(addedChar) ? addedChar : '\0'; // 숫자가 아니면 무시
    }

    // 입력창 값으로 입금
    // 입력창 값으로 입금
    public void OnClickCustomDeposit()
    {
        if (depositInputField != null && int.TryParse(depositInputField.text, out int amount))
        {
            if (amount > 0)
            {
                GameManager.instance.userData.Deposit(amount);
                Refresh();
            }
        }
        depositInputField.text = "";
        depositInputField.ActivateInputField();  // 다시 포커스 줘야 연속 입력 가능
    }

    // 입력창 값으로 출금
    public void OnClickCustomWithdraw()
    {
        if (withdrawInputField != null && int.TryParse(withdrawInputField.text, out int amount))
        {
            if (amount > 0)
            {
                GameManager.instance.userData.Withdraw(amount);
                Refresh();
            }
        }
        withdrawInputField.text = "";
        withdrawInputField.ActivateInputField(); // 다시 포커스 줘야 연속 입력 가능
    }

    // 입금 버튼 클릭
    public void OnClickDeposit()
    {
        CloseAllPanels();
        DepositPanel.SetActive(true);
    }

    // 출금 버튼 클릭
    public void OnClickWithdraw()
    {
        CloseAllPanels();
        WithdrawPanel.SetActive(true);
    }

    // 잔액 부족 알림 확인
    public void OnClickLackOfBalance()
    {
        CloseAllPanels();
        MainPanel.SetActive(true);
    }

    // 현금 부족 알림 확인
    public void OnClickLackOfCash()
    {
        CloseAllPanels();
        MainPanel.SetActive(true);
    }

    // 뒤로가기 버튼 클릭
    public void OnClickBack()
    {
        CloseAllPanels();
        MainPanel.SetActive(true);
    }

    // 패널 전부 끄기
    private void CloseAllPanels()
    {
        MainPanel.SetActive(false);
        DepositPanel.SetActive(false);
        WithdrawPanel.SetActive(false);
        LackOfBalance.SetActive(false);
    }

    // 버튼으로 입금
    public void DepositMoney(int amount)
    {
        GameManager.instance.userData.Deposit(amount);
        Refresh();
    }

    // 버튼으로 출금
    public void WithdrawMoney(int amount)
    {
        GameManager.instance.userData.Withdraw(amount);
        Refresh();
    }
}
