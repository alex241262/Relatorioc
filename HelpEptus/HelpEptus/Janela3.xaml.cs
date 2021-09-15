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
using System.Windows.Shapes;

namespace HelpEptus
{
    /// <summary>
    /// Lógica interna para Janela3.xaml
    /// </summary>
    public partial class Janela3 : Window
    {

        MySqlConnection conexao;
        MySqlCommand comando;
        MySqlDataAdapter da;
        MySqlDataReader dr;
        string sql;


        public Janela3()
        {
            InitializeComponent();
            conexao = new MySqlConnection("SERVER=localhost ; USER=root ; PASSWORD=18110002 ; DATABASE=sousa ; PORT=3306");

           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var data1 = dat1.SelectedDate.Value.Date.ToString();
            var data2 = dat2.SelectedDate.Value.Date.ToString();
            try { 
            sql = "update venda_cab set dif_docto=0.00 where dt_movto>='"+data1+"' and dt_movto<='"+data2+"' and dif_docto>=0.01";
            conexao.Open();
            comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("algum erro de execução" + ex);
            }
            finally
            { //mudar cor de botao
                lv.Background = Brushes.Green;
            }
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("pt-BR", true);

            customCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;

            var data1 = dat1.SelectedDate.Value.Date.ToString();
            var documento = doc.Text.ToString();
            var vlb = txtbruto.Text.ToString();
            var vlq = txtliquido.Text.ToString();
            var vlp = txtpago.Text.ToString();
            try
            {
                conexao = new MySqlConnection("SERVER=localhost ; USER=root ; PASSWORD=18110002 ; DATABASE=sousa ; PORT=3306");
                sql = "UPDATE ctas_receber set valor_bruto="+vlb+",valor_liquido="+vlq+",valor_pagto="+vlp+ " where dt_emissao='" + data1 + "' and no_documento=" + documento;
                comando = new MySqlCommand(sql, conexao);

                conexao.Open();
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("algum erro de execução" + ex);
            }
            finally
            { //mudar cor de botao
                aj.Background = Brushes.Green;
            }
        }

        private void Bs_Click(object sender, RoutedEventArgs e)
        {
            System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("pt-BR", true);

            customCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;

            var data1 = dat1.SelectedDate.Value.Date.ToString();
            var data2 = dat2.SelectedDate.Value.Date.ToString();
            var documento = doc.Text.ToString();
            try
            {
                conexao = new MySqlConnection("SERVER=localhost ; USER=root ; PASSWORD=18110002 ; DATABASE=sousa ; PORT=3306");
                sql = "select * from ctas_receber where no_documento="+documento+" and dt_emissao='"+data1+"'";
                comando = new MySqlCommand(sql, conexao);

                conexao.Open();
                dr = comando.ExecuteReader();
                while (dr.Read())
                {
                    txtbruto.Text = Convert.ToString(dr["valor_bruto"]);
                    txtliquido.Text = Convert.ToString(dr["valor_liquido"]);
                    txtpago.Text = Convert.ToString(dr["valor_pagto"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("algum erro de execução" + ex);
            }
            finally { conexao.Close(); }
        }
    }
}
