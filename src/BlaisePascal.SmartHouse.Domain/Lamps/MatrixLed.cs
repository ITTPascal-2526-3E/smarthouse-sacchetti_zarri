using BlaisePascal.SmartHouse.Domain.AbstractInterfaces;
using BlaisePascal.SmartHouse.Domain.Abstraction.ValObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public sealed class MatrixLed : Device
    {
        public Led[,] matrix;

        public void GenerateMatrix(int rows, int columns, Led led) 
        {    
            matrix = new Led[rows, columns];
            
            for(int i = 0;  i < rows; i++)
            {
                for(int j = 0;  j < columns; j++)
                {
                    matrix[i, j] = new Led(led.power, led.brand, led.max_brightness); ;
                }
            }
            lastModifiedAtUtc = DateTime.Now;
        }
        public override void turnOn()
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

        public override void turnOff()
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

        public void SetIntensityAll(Brightness intensity)
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
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        matrix[i, j].turnOn();
                    }
                    else
                    {
                        matrix[i, j].turnOff();
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
