using Xunit;
using System.Reflection;
using BlaisePascal.SmartHouse.Domain;

namespace BlaisePascal.SmartHouse.Domain.UnitTests.Test
{
    public class SmokeDetectorTest
    {
        [Fact]
        public void SmokeDetector_WhenNoSmoke_ShouldNotDetectSmoke()
        {
            // Arrange
            var detector = new SmokeDetector();

            // Act
            detector.smokeDetector();

            // Assert
            Assert.False(detector.smoke_detectet);
        }

        [Fact]
        public void SmokeDetector_WhenSmokeIsTrue_ShouldDetectSmoke()
        {
            // Arrange
            var detector = new SmokeDetector();
            detector.smoke = true;

            // Act
            detector.smokeDetector();

            // Assert
            Assert.True(detector.smoke_detectet);
        }

        [Fact]
        public void Alarm_ShouldResetSmokeDetectedToFalse()
        {
            // Arrange
            var detector = new SmokeDetector();
           

            // Act
            detector.alarm();

            // Assert
            Assert.False(detector.smoke_detectet);
        }
    }
}