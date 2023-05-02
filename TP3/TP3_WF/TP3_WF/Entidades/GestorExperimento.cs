﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TP3_WF.Entidades;

namespace TP3_WF.Entidades
{
    internal class GestorExperimento
    {
        private string _archivoCSV = @"./datos.csv";
        Experimento experimento;
        private string CondExito;

        public GestorExperimento()
        {
            experimento = new Experimento();
            CondExito = "Definitivamente si";
        }

        private static double Truncar(double rnd)
        {
            return Math.Truncate(rnd * 10000) / 10000;
        }

        private static dynamic[] AsignarResLinea(int i, double rnd1, bool rec, double rnd2, string comp, int exitos)
        {
            return new dynamic[] {i, rnd1, rec, rnd2, comp, exitos};
        }

        public void RealizarExperimento(int nroExpemimentos, int desde, int cant)
        {
            // Se abre archivo CSV
            CsvWriter csv = new CsvWriter(_archivoCSV);

            // Inicializan los rnds
            Random rndRecuerda = new Random(Guid.NewGuid().GetHashCode());
            Random rndComprara = new Random(Guid.NewGuid().GetHashCode());

            // Variables de iteracion

            // Recuerda: recuerda el cliente la publicidad?
            double rnd1;
            bool recuerda;

            // Comprara: comprará el cliente el producto?
            double rnd2;
            string comprara;

            // Contador de Éxitos
            int exitos = 0;

            // Línea de resultados: [NroExp, rndRecuerda, Recuerda, rndComprara, Comprara, AC Exitos]
            dynamic[] res = new dynamic[6];

            for(int i = 1; i < nroExpemimentos+1; i++)
            {
                // Ver si el cliente recuerda el comercial
                rnd1 = Truncar(rndRecuerda.NextDouble());
                recuerda = experimento.Recuerda(rnd1);

                // Ver si el cliente quiere comprar el producto
                rnd2 = Truncar(rndComprara.NextDouble());
                comprara = experimento.Comprara(rnd2, recuerda);

                // Si quiere comprar: éxito
                if(comprara == CondExito) exitos++;
                
                // Creamos la línea de la iteración
                res = AsignarResLinea(i, rnd1, recuerda, rnd2, comprara, exitos);

                // Cargamos la línea al csv si cumple la condición
                if(i >= desde && i < desde + cant) 
                    csv.WriteToCsvFile(string.Join(", ", res));
            };
            
            // Cargamos la última línea (resultado de la simulación)
            csv.WriteToCsvFile("\n" + string.Join(", ", res));

            // Cerramos el archivo
            csv.CloseStream();
        }
    }
}
