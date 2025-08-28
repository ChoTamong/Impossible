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

        // ������ �� ���� �гθ� �Ѱ� �������� ����
        MainPanel.SetActive(true);
        DepositPanel.SetActive(false);
        WithdrawPanel.SetActive(false);
        LackOfBalance.SetActive(false);

        depositInputField.onValidateInput += ValidateNumeric;
        withdrawInputField.onValidateInput += ValidateNumeric;
    }
    private char ValidateNumeric(string text, int charIndex, char addedChar)
    {
        return char.IsDigit(addedChar) ? addedChar : '\0'; // ���ڰ� �ƴϸ� ����
    }

    // �Է�â ������ �Ա�
    // �Է�â ������ �Ա�
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
        depositInputField.ActivateInputField();  // �ٽ� ��Ŀ�� ��� ���� �Է� ����
    }

    // �Է�â ������ ���
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
        withdrawInputField.ActivateInputField(); // �ٽ� ��Ŀ�� ��� ���� �Է� ����
    }

    // �Ա� ��ư Ŭ��
    public void OnClickDeposit()
    {
        CloseAllPanels();
        DepositPanel.SetActive(true);
    }

    // ��� ��ư Ŭ��
    public void OnClickWithdraw()
    {
        CloseAllPanels();
        WithdrawPanel.SetActive(true);
    }

    // �ܾ� ���� �˸� Ȯ��
    public void OnClickLackOfBalance()
    {
        CloseAllPanels();
        MainPanel.SetActive(true);
    }

    // ���� ���� �˸� Ȯ��
    public void OnClickLackOfCash()
    {
        CloseAllPanels();
        MainPanel.SetActive(true);
    }

    // �ڷΰ��� ��ư Ŭ��
    public void OnClickBack()
    {
        CloseAllPanels();
        MainPanel.SetActive(true);
    }

    // �г� ���� ����
    private void CloseAllPanels()
    {
        MainPanel.SetActive(false);
        DepositPanel.SetActive(false);
        WithdrawPanel.SetActive(false);
        LackOfBalance.SetActive(false);
    }

    // ��ư���� �Ա�
    public void DepositMoney(int amount)
    {
        GameManager.instance.userData.Deposit(amount);
        Refresh();
    }

    // ��ư���� ���
    public void WithdrawMoney(int amount)
    {
        GameManager.instance.userData.Withdraw(amount);
        Refresh();
    }
}
