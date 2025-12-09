using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlaisePascal.SmartHouse.Domain.Lamps
{
    public class MatrixLed : Device
    {
        Led[,] matrix;

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
        }

        public void SwitchOffAll()
        {
            for (int i = 0; ; i++)
            {
                for (int j = 0; j <= matrix.GetLength(2); j++)
                {
                    matrix[i,j].turnOff();
                }
            }
        }

        public void SetIntensityAll(int intensity)
        {
            for (int i = 0; ; i++)
            {
                for (int j = 0; j <= matrix.GetLength(2); j++)
                {
                    matrix[i, j].adjustBrightness(intensity);
                }
            }
        }

        public void PatternCheckerBoard()
        {
            for (int i = 0; ; i++)
            {
                for (int j = 0; j <= matrix.GetLength(2); j++)
                {
                    matrix[i, j].turnOn();
                }
            }

            if (matrix.GetLength(1) % 2 == 0)
            {
                for (int i = 0; ; i++)
                {
                    for (int j = 0; j <= matrix.GetLength(2); j++)
                    {
                        if (j % 2 == 0)
                            matrix[i, j].turnOff();
                    }
                }
            }
            else
            {
                for (int i = 0; ; i++)
                {
                    for (int j = 0; j <= matrix.GetLength(2); j++)
                    {
                        if ((i % 2) == (j % 2))
                        {
                            matrix[i, j].turnOn();
                        }
                         
                    }
                }
            }

        }

        public Led GetLed(int rows, int columns)
        {
           return matrix[rows, columns];
        }
    }
}
