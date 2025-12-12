using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public class MatrixLed : Device
    {
        public Led[,] matrix;

        public void GenerateMatrix(int rows, int columns, Led led) 
        {    
            matrix = new Led[rows, columns];
            
            for(int i = 0;  i < rows; i++)
            {
                for(int j = 0;  j < columns; j++)
                {
                    matrix[i, j] = led;
                }
            }
            lastModifiedAtUtc = DateTime.Now;
        }

        public void SwitchOnAll()
        {
            for(int i = 0;i < matrix.GetLength(0); i++)
            {
                for(int j = 0;j < matrix.GetLength(1); j++)
                {
                    matrix[i,j].turnOn();
                }
            }
            lastModifiedAtUtc = DateTime.Now;
        }

        public void SwitchOffAll()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j].turnOff();
                }
            }
            lastModifiedAtUtc = DateTime.Now;
        }

        public void SetIntensityAll(int intensity)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j].adjustBrightness(intensity);
                }
            }
            lastModifiedAtUtc = DateTime.Now;
        }

        public void PatternCheckerBoard()
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if(i % 2 == 0)
                {
                    for (int j = 1; j < matrix.GetLength(1); j+=2)
                    {
                        matrix[i, j].turnOn();
                    }
                }
                else
                {
                    for (int j = 0; j < matrix.GetLength(1); j += 2)
                    {
                        matrix[i, j].turnOn();
                    }
                }         
            }
            lastModifiedAtUtc = DateTime.Now;
        }

        public Led GetLed(int rows, int columns)
        {
           return matrix[rows, columns];
        }
    }
}
