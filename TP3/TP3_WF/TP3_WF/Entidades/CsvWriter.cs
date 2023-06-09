﻿using System;
using System.IO;

namespace TP3_WF.Entidades
{
    public class CsvWriter
    {
        StreamWriter streamWriter;

        public CsvWriter(string filePath)
        {
            // Instancia el objeto que nos permite escribir en el archivo CSV
            streamWriter = new StreamWriter(filePath);
        }

        public void WriteToCsvFile(string res)
        {
            // Escribe un string en el archivo CSV
            streamWriter.WriteLine(res);
        }

        public void CloseStream()
        {
            // Cierra el archivo CSV
            streamWriter.Close();
        }
    }
}