using Monopoly.Models.Components.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Models.Bank
{
    public class BankAccount: INotifyPropertyChanged
    {
        #region Variables
        /// <summary>
        /// Amount of the bank account
        /// </summary>
        private double _amount { get; set; }
        public double Amount
        {
            get
            {
                return _amount;
            }
            set
            {
                if(_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged();
                }
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Create an instance of the class
        /// </summary>
        public BankAccount()
        {
            this.Amount = 1500;
        }

        /// <summary>
        ///  Create an instance of the class with the amount value
        /// </summary>
        /// <param name="amount">Initail amount value</param>
        public BankAccount(double amount)
        {
            this.Amount = amount;
        }
        
        #endregion

        #region NotifyPropertyChanged

        /// <summary>
        /// Notify Property Changed
        /// </summary>
        /// <param name="propertyName">Name of property</param>
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Add an amount to the bank balance
        /// </summary>
        /// <param name="amount">Amount value</param>
        private void AddAmount(double amount)
        {
            this.Amount += amount;
        }

        /// <summary>
        /// Remove an amount from the bank balance
        /// </summary>
        /// <param name="amount">Amount value</param>
        private void RemoveAmount(double amount)
        {
            CheckIfBankBalanceIsEnougth(amount);
            this.Amount -= amount;
        }

        /// <summary>
        /// Check if the bank balance is sufficient
        /// </summary>
        /// <param name="amount">Amount value</param>
        /// <returns>Boolean : is there enougth or not</returns>
        private bool CheckIfBankBalanceIsEnougth(double amount)
        {
            if ((this.Amount - amount) >= 0)
            {
                return true;
            }
            else
            {                
                return false;
            }
        }

        /// <summary>
        ///  Withdraw an amount from the bank balance
        /// </summary>
        /// <param name="amount">Amount value</param>
        private double Withdraw(double amount)
        {
            RemoveAmount(amount);
            return amount;
            
        }
        #endregion

        #region Public methods 
        /// <summary>
        /// Return the bank balance value
        /// </summary>
        /// <returns>Amount value</returns>
        public double GetAmount() { return this.Amount; }


        /// <summary>
        /// Makes a transfer to the target bank account
        /// </summary>
        /// <param name="bankAccount">Target bank account</param>
        /// <param name="amount">Amount value</param>
        public void BankTransfer(BankAccount bankAccount, double amount)
        {
            if(CheckIfBankBalanceIsEnougth(amount))
            {
                double money = this.Withdraw(amount);
                bankAccount.AddAmount(money);
            }
            else
            {
                throw new BankBalanceIsNotEnougth(amount);
            }
        }
        

        #endregion

    }
}
