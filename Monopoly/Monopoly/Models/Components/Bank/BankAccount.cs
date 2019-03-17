using Monopoly.Models.Components.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Bank
{
    public class BankAccount
    {
        #region Variables
        /// <summary>
        /// Amount of the bank account
        /// </summary>
        private double _amount { get; set; }
        #endregion

        #region Constructeurs
        /// <summary>
        /// Create an instance of the class
        /// </summary>
        public BankAccount()
        {
            this._amount = 1500;
        }

        /// <summary>
        ///  Create an instance of the class with the amount value
        /// </summary>
        /// <param name="amount">Initail amount value</param>
        public BankAccount(int amount)
        {
            this._amount = amount;
        }
        #endregion
        #region Private methods
        /// <summary>
        /// Add an amount to the bank balance
        /// </summary>
        /// <param name="amount">Amount value</param>
        private void AddAmount(double amount)
        {
            this._amount += amount;
        }

        /// <summary>
        /// Remove an amount from the bank balance
        /// </summary>
        /// <param name="amount">Amount value</param>
        private void RemoveAmount(double amount)
        {
            this._amount -= amount;
        }

        /// <summary>
        /// Check if the bank balance is sufficient
        /// </summary>
        /// <param name="amount">Amount value</param>
        /// <returns>Boolean : is there enougth or not</returns>
        private bool CheckIfBankBalanceIsEnougth(double amount)
        {
            if ((this._amount - amount) >= 0)
            {
                return true;
            }
            else
            {
                throw new BankBalanceIsNotEnougth();
            }
        }

        /// <summary>
        ///  Withdraw an amount from the bank balance
        /// </summary>
        /// <param name="amount">Amount value</param>
        private double Withdraw(double amount)
        {
            if (CheckIfBankBalanceIsEnougth(amount))
            {
                RemoveAmount(amount);
                return amount;
            }
            return 0;
        }
        #endregion

        #region Public methods 
        /// <summary>
        /// Return the bank balance value
        /// </summary>
        /// <returns>Amount value</returns>
        public double GetAmount() { return this._amount; }


        /// <summary>
        /// Makes a transfer to the target bank account
        /// </summary>
        /// <param name="bankAccount">Target bank account</param>
        /// <param name="amount">Amount value</param>
        public void BankTransfer(BankAccount bankAccount, double amount)
        {
            double money = this.Withdraw(amount);
            bankAccount.AddAmount(money);

        }

        #endregion

    }
}
