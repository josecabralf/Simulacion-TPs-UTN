﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using TP2_WF.Entidades;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.Common;

namespace TP2_WF.Presentacion
{
    public partial class PantallaVisualizacion : Form
    {
        CsvReader csvReader;
        decimal[] MinMax;
        DataTable CSV;
        int[] frecObs;

        public PantallaVisualizacion(decimal[] minMax, int cantIntervalos)
        {
            // Inicializa los componentes de la pantalla e instancia
            InitializeComponent();

            csvReader = new CsvReader();
            MinMax = minMax;
            CSV = new DataTable();
            frecObs = new int[cantIntervalos];
        }

        private void PantallaVisualizacion_Load(object sender, EventArgs e)
        {
            // Para el eje x:
            decimal[] arrayLimSup = new decimal[frecObs.Length];

            // Se carga el visualizador de datos y las frecuencias observadas
            csvReader.LoadCsvData(CSV, frecObs, MinMax, arrayLimSup);
            gdw_dataSet.DataSource = CSV;

            // Estetico Columnas
            foreach (DataGridViewColumn columna in gdw_dataSet.Columns)
            {
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            // Establecemos el título del gráfico
            chart1.Titles.Add("Gráfico de Frecuencias Observadas");

            for (int i = 0; i < arrayLimSup.Length; i++)
            {
                Series serie = chart1.Series.Add(arrayLimSup[i].ToString());
                
                serie.Label = frecObs[i].ToString();
                serie.Points.Add(frecObs[i]);
            };

            this.cargarTablaFrecObs(arrayLimSup);
        }

        private void cargarTablaFrecObs(decimal[] arrayLimSup)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Intervalo");
            dt.Columns.Add("Frecuencia Observada");
            

            //Obtiene el ancho de intervalo
            decimal anchoIntervalo = (MinMax[1] - MinMax[0]) / frecObs.Length;


            // Agrega los numeros a la tabla y obtiene la frecuencia observada de los intervalos
            decimal limInf;

            for (int i = 0; i< arrayLimSup.Length;i++)
            {
                limInf = arrayLimSup[i] - anchoIntervalo;
                string intervalo = $"{limInf} <= x < {arrayLimSup[i]}";
                dt.Rows.Add(intervalo, frecObs[i]);
            };

            gdw_frecObs.DataSource = dt;
            foreach (DataGridViewColumn columna in gdw_frecObs.Columns)
            {
                columna.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
    }
}

