using Xunit;
using BlaisePascal.SmartHouse.Domain.Security;
using System.Reflection;

namespace BlaisePascal.SmartHouse.Domain.UnitTests.SecurityTest
{
    public class SecureDoorTest
    {
        private const string InitialPassword = "TestPass123";
        private const string InitialEmail = "test@example.com";

        [Fact]
        public void Constructor_ShouldInitializeLockedStateAndEmail()
        {
            // Arrange
            // Act
            var door = new SecureDoor(InitialPassword, InitialEmail);

            // Assert
            Assert.True(door.is_locked);
            Assert.Equal(InitialEmail, door.mail);
        }

        [Fact]
        public void LockDoor_ShouldSetIsLockedToTrue()
        {
            // Arrange
            var door = new SecureDoor(InitialPassword, InitialEmail);
            door.UnlockDoor(InitialPassword);

            // Act
            door.LockDoor();

            // Assert
            Assert.True(door.is_locked);
        }

        [Fact]
        public void UnlockDoor_WithCorrectPassword_ShouldSetIsLockedToFalse()
        {
            // Arrange
            var door = new SecureDoor(InitialPassword, InitialEmail);

            // Act
            door.UnlockDoor(InitialPassword);

            // Assert
            Assert.False(door.is_locked);
        }

        [Fact]
        public void UnlockDoor_WithIncorrectPassword_ShouldStayLocked()
        {
            // Arrange
            var door = new SecureDoor(InitialPassword, InitialEmail);

            // Act
            door.UnlockDoor("WrongPassword");

            // Assert
            Assert.True(door.is_locked);
        }

    }
}