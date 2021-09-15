using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HelpEptus
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {

        MySqlConnection conexao;
        MySqlCommand comando;
        MySqlDataAdapter da;
        MySqlDataReader dr;
        string sql;

        public MainWindow()
        {
            InitializeComponent();

            conexao = new MySqlConnection("SERVER=localhost ; USER=root ; PASSWORD=18110002 ; DATABASE=sousa ; PORT=3306");
            conexao.Open();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Janela2 janela2 = new Janela2();
            janela2.Show();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Janela3 janela3 = new Janela3();
            janela3.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                sql = "update venda_nfefat set dup_vdup = fat_vliq where fat_vliq<>dup_vdup and dt_movto='2021-01-02' and dup_ndup like '%-1%'";
                comando = new MySqlCommand(sql, conexao);
                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("erro em processo " + ex);
            }
            finally { MessageBox.Show("parabens"); }
        }
    }
}
