using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Wallet
{
    public int MoneyLimit { get; set; }

    public int Money { get; set; }

    public Wallet(int money, int maxAmount = 1000)
    {
        Money = money;
        MoneyLimit = maxAmount;
    }

    public void Add(int money) {
        //int MoneyLimit = MoneyLimitPerLevel[Level];
        if (Money >= MoneyLimit) return;
        this.Money += money;
        if (Money > MoneyLimit)
        {
            Debug.LogFormat("The amount has exceeded the limit. [Money={0}, MaxAmount={1}]" +
                            "Money that goes over will disappear.", Money, MoneyLimit);
            Money = MoneyLimit;
        }
    } 
    
    public bool TryPay(int payment) {
        bool paymentSuccess = HasEnoughMoney(payment);
        if (paymentSuccess)
        {
            Money -= payment;
        }
        else
        {
            Debug.LogFormat("Not enough money left. payment={0}, Money={1}]", payment, Money);
        }
        return paymentSuccess;
    }
    
    public bool HasEnoughMoney(int payment) {
        return Money >= payment;
    }
}
