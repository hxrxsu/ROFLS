using Microsoft.Win32;
using NAudio.Wave;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace ROFLS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Person character = new Person()
        {
            _health = 100,
            _stamina = 100,
            _money = 0,
            _itemVova = false,
            _itemBara = false,
            _itemGoku = false
        };

        Enemy enemy = new Enemy()
        {
            _ehealth = 100,
            _edamage = 5,
        };

        public MainWindow()
        {
            InitializeComponent();

            RefreshData();
        }

        private void RefreshData()
        {
            TB_health.Text = character._health.ToString();
            TB_stamina.Text = character._stamina.ToString();
            TB_damage.Text = character._damage.ToString();
            TB_money.Text = character._money.ToString();

            TB_enemyhealth.Text = enemy._ehealth.ToString();
            TB_enemydamage.Text = enemy._edamage.ToString();

            L_stats.Content = $"u have items vova - {character._itemVova}, goku - {character._itemGoku}, bara - {character._itemBara}";
        }

        public void BN_attack_Click(object sender, RoutedEventArgs e)
        {
            Attack();
            RefreshGame();
        }
        public void RefreshGame()
        {
            RefreshData();

            if (enemy._ehealth <= 0)
            {
                MessageBox.Show("u win");
                enemy._ehealth = 100;
                enemy._edamage = 33;
                character._money += 5;

            else if (character._health <= 0)
            {
                MessageBox.Show("u lose");
                character._money = 0;
                character._health = 100;
                character._stamina = 100;
                enemy.Rofls();
            }
        }
        public void Attack()
        {
            MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory);
            enemy._ehealth -= character._damage;
            TB_enemyhealth.Text = enemy._ehealth.ToString();

            character._health -= enemy._edamage;
            RefreshData();

            if (character._stamina <= 0)
            {
                character._stamina = 0;
                character._health -= enemy._edamage * 2;
                MessageBox.Show("u need to rest... u gain x2 damage from enemy");
            }
            else
            {
                character._stamina -= 30;
            }
        }

        private void BN_rest_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();


            if(character._money >= _costrest)
            {
                character._money -= _costrest;
                character._health += 50;
                character._stamina += 50;
                MessageBox.Show("u rest and ready to fight");
            }
            else
            {
                MessageBox.Show("enough money to rest");
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window _window = new Window1();
            this.Close();
            _window.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string data = GetSavePath();
            string savedata = JsonConvert.SerializeObject(character);
            File.WriteAllText(data, savedata);
           
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            character = JsonConvert.DeserializeObject<Person>(saveData);
            RefreshGame();
        }

        private string GetSavePath()
        {
            if (CB_data.IsChecked == true)
            {
                CB_data.Content = "save 1";

            }
            else
            {
                MessageBox.Show("Pls choose a save");
            }
            return data;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            RefreshGame();

            var _costitem = 30;

            if (character._money >= _costitem) 
            {
                MessageBox.Show("u bought item");
                character._money -= _costitem;

                if (CB_vova.IsChecked == true)
                {
                    character._itemVova = true;
                    character._damage -= 30;
                }
                else if (CB_Goku.IsChecked == true)
                {
                    character._itemGoku = true;
                    character._damage += 50;
                }
                else if (CB_Bara.IsChecked == true)
                {
                    character._itemBara = true;
                    character._money += 9999;
                }
            }
            else
            {
                MessageBox.Show("enough money. every item cost 30 money");
            }
        }
    }
}