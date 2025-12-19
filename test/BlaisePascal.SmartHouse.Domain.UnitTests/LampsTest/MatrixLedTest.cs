using BlaisePascal.SmartHouse.Domain.Lamps;
using Xunit;

namespace BlaisePascal.SmartHouse.Domain.UnitTests.LampsTest
{
    public class MatrixLedTest
    {
        [Fact]
        public void GenerateMatrix_ShouldCreateCorrectDimensions()
        {
            // Arrange
            MatrixLed matrixLed = new MatrixLed();
            Led led = new Led(5, "Test", 200);

            // Act
            matrixLed.GenerateMatrix(3, 4, led);

            // Assert
            Assert.Equal(3, matrixLed.matrix.GetLength(0));
            Assert.Equal(4, matrixLed.matrix.GetLength(1));

        }

        [Fact]
        public void SwitchOnAll_ShouldTurnOnEveryLed()
        {
            // Arrange
            MatrixLed matrixLed = new MatrixLed();
            Led led = new Led(5, "Test", 200);
            matrixLed.GenerateMatrix(2, 2, led);

            // Act
            matrixLed.turnOn();

            // Assert
            foreach (var cell in matrixLed.matrix)
            {
                Assert.True(cell.is_on);
            }
        }

        [Fact]
        public void SwitchOffAll_ShouldTurnOffEveryLed()
        {
            // Arrange
            MatrixLed matrixLed = new MatrixLed();
            Led led = new Led(5, "Test", 200);
            matrixLed.GenerateMatrix(2, 2, led);

            matrixLed.turnOn();

            // Act
            matrixLed.turnOff();

            // Assert
            foreach (var cell in matrixLed.matrix)
            {
                Assert.False(cell.is_on);
            }
        }

        [Fact]
        public void SetIntensityAll_ShouldSetBrightnessOnAllLeds()
        {
            // Arrange
            MatrixLed matrixLed = new MatrixLed();
            Led led = new Led(5, "Test", 200);
            matrixLed.GenerateMatrix(2, 3, led);

            // Act
            matrixLed.SetIntensityAll(70);

            // Assert
            foreach (var cell in matrixLed.matrix)
            {
                Assert.Equal(70, cell.brightness_Perc);
            }
        }

        [Fact]
        public void PatternCheckerBoard_ShouldLightOnlyCheckerboardPattern()
        {
            // Arrange
            MatrixLed matrixLed = new MatrixLed();
            Led led = new Led(5, "Test", 200);
            matrixLed.GenerateMatrix(3, 3, led);
            matrixLed.turnOff();
            // Act
            matrixLed.PatternCheckerBoard();

            // Assert
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    bool shouldOn;
                    if ((i+j )%2 == 1)
                    {
                        shouldOn = true;
                    }
                    else
                    {
                        shouldOn = false;
                    }

                    Assert.Equal(shouldOn, matrixLed.matrix[i, j].is_on);                  
                }
            }
        }
    }
}
