using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public int Money { 
    	get {
            return this.wallet.Money;
    	} 
    }

    public int MaxMoney { get { return this.wallet.MoneyLimit; } }
    
    public int WalletLevel { get; private set; }

    public int MaxWalletLevel
    {
        get { return MoneyLimitPerWalletLevel.Keys.Max(); }
    }

    private Dictionary<int, int> MoneyLimitPerWalletLevel = new Dictionary<int, int>()
    {
        { 1, 3500},
        { 2, 5500},
        { 3, 6500},
        { 4, 7500},
        { 5, 8500},
        { 6, 9500},
        { 7, 10500},
        { 8, 11500},
        { 9, 12500},
    };

    private Wallet wallet;

    private int Saraly { get; set; }

    public void Initialize(int money=0, int walletLevel = 1, int saraly = 1) {
        if (!MoneyLimitPerWalletLevel.ContainsKey(walletLevel)) {
            Debug.LogFormat("WalletLevel limit exceeded. level=[{0}], LevelLimit=[{1}]", MaxWalletLevel, walletLevel);
            return;
	    }; 
        this.WalletLevel = walletLevel;
        int moneyLimit = MoneyLimitPerWalletLevel[walletLevel];
        wallet = new Wallet(money, moneyLimit);
        this.Saraly = saraly;
    }

    public void Add(int money) {
        this.wallet.Add(money);
    } 

    public void Earn() {
        this.wallet.Add(Saraly);
    } 
    
    public bool TryPay(int payment) {
        return this.wallet.TryPay(payment);
    }
    
    public bool HasEnoughMoney(int payment) {
        return this.wallet.HasEnoughMoney(payment);
    }

}
