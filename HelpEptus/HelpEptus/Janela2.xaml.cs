using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica interna para Janela2.xaml
    /// </summary>
    public partial class Janela2 : Window
    {
        MySqlConnection conexao;
        MySqlCommand comando;
        MySqlDataAdapter da;
        MySqlDataReader dr;
        string sql;


        public Janela2()
        {
            InitializeComponent();
            conexao = new MySqlConnection("SERVER=localhost ; USER=root ; PASSWORD=18110002 ; DATABASE=sousa ; PORT=3306");
            conexao.Open();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                
               // conexao = new MySqlConnection("SERVER=localhost ; USER=root ; PASSWORD=18110002 ; DATABASE=sousa ; PORT=3306");

                System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("pt-BR", true);

                customCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";

                System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
                System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;

                //var data = dat1.SelectedDate.Value.Date.ToShortDateString().ToString();
                var data = dat1.SelectedDate.Value.Date.ToString();
                var data2 = dat2.SelectedDate.Value.Date.ToString();
                var empresa = EMPRESA.SelectedIndex + 1;
                var mod1 = 0;
                if(combomod.SelectedIndex == 0)
                {
                    mod1 = 55;
                }
                else
                {
                    mod1 = 65;
                }

                
                sql = "select c.no_docto,c.ide_mod,r.no_parcela,r.razao_cliente,r.dt_emissao,r.valor_bruto,r.dt_vencto," +
                    "r.dt_pagto,r.valor_pagto,s.descricao,d.descricao,r.cod_plcontas from venda_nfecab as c,ctas_receber as r," +
                    "portador as d,secao as s where c.no_docto = r.no_documento and c.dt_movto = r.dt_emissao and r.cod_portador = d.codigo " +
                    "and r.cod_secao = s.codigo and c.dt_movto>='"+data+ "' and c.dt_movto<='" + data2 + "' and c.codemp =" + empresa+" and c.ide_mod = "+mod1+
                    " order by r.dt_emissao LIMIT 0,9999 ";


                da = new MySqlDataAdapter(sql, conexao);
                da.SelectCommand.CommandTimeout = 60000;//estende o limite da consulta sql

                DataTable dt = new DataTable();
                
                da.Fill(dt);
                grid1.ItemsSource = dt.DefaultView;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("algum erro de execução" + ex);
            }
            finally {
                MessageBox.Show(" fim da execução");
                conexao.Close(); }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                var empresa = combomod.SelectedItem.ToString();
                var mod1 = 0;
                if (combomod.SelectedIndex == 0)
                {
                    mod1 = 55;
                }
                else
                {
                    mod1 = 65;
                }
                grid1.SelectAllCells();
                grid1.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, grid1);
                String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                String result = (string)Clipboard.GetData(DataFormats.Text);
                grid1.UnselectAllCells();
                System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"C:\relatorio\teste.xls");
                file1.WriteLine(result.Replace(',',' '));
                file1.Close();
                MessageBox.Show("Arquivo gerado com sucesso");
            }catch(Exception ex)
            {
                MessageBox.Show("Erro an execução do codigo \n" + ex);
            }
        }

        private void Grid1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(System.DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        }
    }
}

