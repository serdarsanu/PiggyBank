using PiggyBank.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PiggyBank
{
    public enum MoneyType { Coin, Banknote}
    
    public partial class PiggyBank : Form
    {
        public double TotalValue = 0;

        public double PiggyBankValue = 0;
        public double PiggyBankVolume = 0;
        public double PiggyBankCapacity = 15000;
        public int PiggyBankBrokenCount = 0;
        public bool PiggyBankIsIntact = true;


        List<PiggyBankModel> MoneyInPiggyBank = new List<PiggyBankModel>();
        List<PiggyBankModel> BrokenMoneyInPiggyBank = new List<PiggyBankModel>();
        Banknote choosenBanknote = new Banknote();
        Coin choosenCoin = new Coin();
        MoneyType choosenMoneyType = new MoneyType();

        
        public PiggyBank()
        {
            InitializeComponent();
            btnDisabled(btnAdd);
            btnDisabled(btnFold);
            btnDisabled(btnBreak);
            btnDisabled(btnShake);
            btnRepair.Visible = false;
            lblTotalValue.Text = null;
            CalculatePercentage();
            CustomFunction.clearSelectedMoney(this.Controls);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (PiggyBankIsIntact)
            {
                switch (choosenMoneyType)
                {
                    case MoneyType.Coin:
                        addCoin();
                        break;
                    case MoneyType.Banknote:
                        addBanknote();
                        break;
                    default:
                        break;
                }
            }
            else if(!PiggyBankIsIntact && PiggyBankBrokenCount < 2)
            {
                MessageBox.Show("Kumbaranızı onarmanız gerekmektedir!");
            }
            else
            {
                MessageBox.Show("Kumbaranız artık kullanılamamaktadır!");
            }
            
            
        }

        private void btnFold_Click(object sender, EventArgs e)
        {
            choosenBanknote.isFold = true;
            btnDisabled(btnFold);
            MessageBox.Show("Paranız Katlandı.");
        }

        private void clickCoin(object sender, EventArgs e)
        {
            choosenBanknote = new Banknote();
            btnDisabled(btnFold);
            btnEnabled(btnAdd);
            choosenMoneyType = MoneyType.Coin;
            CustomFunction.clearSelectedMoney(this.Controls);
            Button clickedCoin = (Button)sender;

            if (clickedCoin.Name == "btn1kr") { choosenCoin = new Coin() { 
                Name = "1 Kuruş", Value = 0.01, Radius = 8.25, Thickness = 1.35 };
                btn1kr.Text = "✓";
            }
            else if (clickedCoin.Name == "btn5kr") { choosenCoin = new Coin() { 
                Name = "5 Kuruş", Value = 0.05, Radius = 8.75, Thickness = 1.65 };
                btn5kr.Text = "✓";
            }
            else if (clickedCoin.Name == "btn10kr") { choosenCoin = new Coin() { 
                Name = "10 Kuruş", Value = 0.1, Radius = 9.25, Thickness = 1.65 };
                btn10kr.Text = "✓";
            }
            else if (clickedCoin.Name == "btn25kr") { choosenCoin = new Coin() { 
                Name = "25 Kuruş", Value = 0.25, Radius = 10.25, Thickness = 1.65 };
                btn25kr.Text = "✓";
            }
            else if (clickedCoin.Name == "btn50kr") { choosenCoin = new Coin() { 
                Name = "50 Kuruş", Value = 0.5, Radius = 11.925, Thickness = 1.9 };
                btn50kr.Text = "✓";
            }
            else { choosenCoin = new Coin() { 
                Name = "1 Lira", Value = 1, Radius = 13.075, Thickness = 1.9 };
                btn1tl.Text = "✓";
            }
        }

        private void banknoteClick(object sender, EventArgs e)
        {
            choosenCoin = new Coin();
            btnEnabled(btnFold);
            btnEnabled(btnAdd);
            choosenMoneyType = MoneyType.Banknote;
            CustomFunction.clearSelectedMoney(this.Controls);
            Button clickedBanknote = (Button)sender;
            
            if (clickedBanknote.Name == "btn5tl") { 
                choosenBanknote = new Banknote() { Name = "5 Lira", Value = 5, Width = 64, Length = 130, Height = 0.25, isFold = false };
                btn5tl.Text = "✓";
            }
            else if (clickedBanknote.Name == "btn10tl") { choosenBanknote = new Banknote() { 
                Name = "10 Lira", Value = 10, Width = 64, Length = 136, Height = 0.25, isFold = false };
                btn10tl.Text = "✓";
            }
            else if (clickedBanknote.Name == "btn20tl") { choosenBanknote = new Banknote() { 
                Name = "20 Lira", Value = 20, Width = 68, Length = 142, Height = 0.25, isFold = false };
                btn20tl.Text = "✓";
            }
            else if (clickedBanknote.Name == "btn50tl") { choosenBanknote = new Banknote() { 
                Name = "50 Lira", Value = 50, Width = 68, Length = 148, Height = 0.25, isFold = false };
                btn50tl.Text = "✓";
            }
            else if (clickedBanknote.Name == "btn100tl") { choosenBanknote = new Banknote() { 
                Name = "100 Lira", Value = 100, Width = 72, Length = 154, Height = 0.25, isFold = false };
                btn100tl.Text = "✓";
            }
            else { choosenBanknote = new Banknote() { 
                Name = "200 Lira", Value = 200, Width = 72, Length = 160, Height = 0.25, isFold = false };
                btn200tl.Text = "✓";
            }
        }

        private void btnShake_Click(object sender, EventArgs e)
        {
            if (PiggyBankVolume != 0)
            {
                btnDisabled(btnShake);
                PiggyBankVolume = 0;
                foreach (PiggyBankModel money in MoneyInPiggyBank)
                {
                    money.Volume = (money.RawVolume * 1.25);
                    PiggyBankVolume += (money.RawVolume * 1.25);
                }
                CalculatePercentage();
            }
        }

        private void btnRepair_Click(object sender, EventArgs e)
        {
            if(!PiggyBankIsIntact && PiggyBankBrokenCount < 2)
            {
                PiggyBankIsIntact = true;
                choosenBanknote = new Banknote();
                choosenCoin = new Coin();
                PiggyBankValue = 0;
                PiggyBankVolume = 0;
                CustomFunction.clearSelectedMoney(this.Controls);
                MessageBox.Show("Kumbaranız onarıldı.");
                btnRepair.Visible = false;
            }
            else
            {
                MessageBox.Show("Kumbaranız artık onarılamaz.");
            }
        }

        public void CalculatePercentage()
        {
            lblOccupancyRate.Text = "%" + Math.Round(((PiggyBankVolume / PiggyBankCapacity) * 100), 1);
        }

        private void btnBreak_Click(object sender, EventArgs e)
        {
            if (PiggyBankBrokenCount == 0)
            {
                BrokenMoneyInPiggyBank = MoneyInPiggyBank;
                MoneyInPiggyBank = new List<PiggyBankModel>();
                TotalValue += PiggyBankValue;
                lblTotalValue.Text = "Total Value: " + Math.Round(TotalValue, 2) + "₺";
                PiggyBankValue = 0;
                PiggyBankVolume = 0;
                CalculatePercentage();
                PiggyBankBrokenCount++;
                PiggyBankIsIntact = false;
                choosenBanknote = new Banknote();
                choosenCoin = new Coin();
                CustomFunction.clearSelectedMoney(this.Controls);
                MessageBox.Show("Kumbaranız kırıldı.");
                btnDisabled(btnBreak);
                btnRepair.Visible = true;
                btnEnabled(btnRepair);
                
            }
            else if (PiggyBankBrokenCount == 1)
            {
                BrokenMoneyInPiggyBank = MoneyInPiggyBank;
                MoneyInPiggyBank = new List<PiggyBankModel>();
                TotalValue += PiggyBankValue;
                lblTotalValue.Text = "Total Value: " + Math.Round(TotalValue, 2) + "₺";
                PiggyBankValue = 0;
                PiggyBankVolume = 0;
                CalculatePercentage();
                PiggyBankBrokenCount++;
                PiggyBankIsIntact = false;
                choosenBanknote = new Banknote();
                choosenCoin = new Coin();
                CustomFunction.clearSelectedMoney(this.Controls);
                MessageBox.Show("Kumbaranız kırıldı.");
                btnDisabled(btnBreak);
                btnRepair.Visible = true;
                btnEnabled(btnRepair);
            }
        }

        private void addCoin()
        {
            PiggyBankModel addNewMoney = new PiggyBankModel();
            addNewMoney.Name = choosenCoin.Name;
            addNewMoney.Value = choosenCoin.Value;
            addNewMoney.RawVolume = choosenCoin.Volume();
            addNewMoney.Volume = choosenCoin.PiggyBankVolume();
            if ((PiggyBankVolume + addNewMoney.Volume) > PiggyBankCapacity)
            {
                MessageBox.Show("Kumbarada eklemek istediğiniz para kadar yer bulunmamakta.\nKumbarayı sallayıp yer açabilir veya kumbarayı kırıp paranızı alabilirsiniz.");
                return;
            }
            MoneyInPiggyBank.Add(addNewMoney);
            PiggyBankValue = 0;
            PiggyBankVolume = 0;
            foreach (PiggyBankModel money in MoneyInPiggyBank)
            {
                PiggyBankValue += money.Value;
                PiggyBankVolume += money.Volume;
            }
            CalculatePercentage();
            choosenBanknote = new Banknote();
            choosenCoin = new Coin();
            btnEnabled(btnShake);
            btnEnabled(btnBreak);
            CustomFunction.clearSelectedMoney(this.Controls);
        }

        private void addBanknote()
        {
            if (choosenBanknote.isFold)
            {
                PiggyBankModel addNewMoney = new PiggyBankModel();
                addNewMoney.Name = choosenBanknote.Name;
                addNewMoney.Value = choosenBanknote.Value;
                addNewMoney.RawVolume = choosenBanknote.Volume();
                addNewMoney.Volume = choosenBanknote.PiggyBankVolume();
                if ((PiggyBankVolume + choosenBanknote.PiggyBankVolume()) > PiggyBankCapacity)
                {
                    MessageBox.Show("Kumbarada eklemek istediğiniz para kadar yer bulunmamakta.\nKumbarayı sallayıp yer açabilir veya kumbarayı kırıp paranızı alabilirsiniz.");
                    return;
                }
                MoneyInPiggyBank.Add(addNewMoney);
                PiggyBankValue = 0;
                PiggyBankVolume = 0;
                foreach (PiggyBankModel money in MoneyInPiggyBank)
                {
                    PiggyBankValue += money.Value;
                    PiggyBankVolume += money.Volume;
                }
                CalculatePercentage();
                choosenBanknote = new Banknote();
                choosenCoin = new Coin();
                btnEnabled(btnShake);
                btnEnabled(btnBreak);
                CustomFunction.clearSelectedMoney(this.Controls);
            }
            else
            {
                MessageBox.Show("Lütfen parayı katlayınız...");
            }
        }

        private void btnDisabled(Button btn)
        {
            btn.Enabled = false;
            btn.BackColor = Color.Gainsboro; 
            btn.ForeColor = Color.Silver;
        }
        private void btnEnabled(Button btn)
        {
            btn.Enabled = true;
            btn.BackColor = Color.Blue;
            btn.ForeColor = Color.White;
        }
    }
}
