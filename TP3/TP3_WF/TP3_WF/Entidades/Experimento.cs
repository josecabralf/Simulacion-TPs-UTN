﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3_WF.Entidades
{
    internal class Experimento
    {
        // Esta clase contiene los métodos necesarios para llevar a cabo 1 sola corrida del experimento

        // Atributos:
        private string[] Comprar;
        private double[] _ProbAcRecuerda;
        private double[] _ProbAcNoRecuerda;
        private double _recuerda;

        public Experimento(double recueda, double[] probsRec, double[] probsNoRec)
        {
            // Comprar: tiene todos los posibles resultados finales del experimento
            Comprar = new string[] { "Definitivamente si", "Dudoso", "Definitivamente no" };

            // _ProbAcRecuerda: contiene las P() AC de los resultados en caso de que el cliente recuerde la publicidad
            _ProbAcRecuerda = new double[] { probsRec[0], probsRec[0] + probsRec[1], 1 };

            // _ProbAcNoRecuerda: contiene las P() AC de los resultados en caso de que el cliente no recuerde la publicidad
            _ProbAcNoRecuerda = new double[] { probsNoRec[0], probsNoRec[0] + probsNoRec[1], 1 };

            _recuerda = recueda;
        }

        public bool Recuerda(double rnd)
        // Este método se encarga de determinar si el cliente recuerda la publicidad
        {
            if (rnd < _recuerda) return true;

            return false;
        }

        private string CompraraRecuerda(double rnd)
        // Este método se encarga de determinar la rta de un cliente que recuerda la publicidad
        {
            for (int i = 0; i<2; i++)
            {
                if(rnd < _ProbAcRecuerda[i])
                    return Comprar[i];
            }
            return Comprar[2];
        }

        private string CompraraNoRecuerda(double rnd)
        // Este método se encarga de determinar la rta de un cliente que no recuerda la publicidad
        {
            for (int i = 0; i < 2; i++)
            {
                if (rnd < _ProbAcNoRecuerda[i])
                    return Comprar[i];
            }
            return Comprar[2];
        }

        public string Comprara(double rnd, bool recuerda)
        // Este método se encarga de determinar la rta del cliente
        {
            if(recuerda)
                return CompraraRecuerda(rnd);
            else
                return CompraraNoRecuerda(rnd);
        }
    }
}
