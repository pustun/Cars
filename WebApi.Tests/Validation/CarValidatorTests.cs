using FluentAssertions;
using Model;
using Moq;
using NUnit.Framework;
using WebApi.Validators;

namespace WebApi.Tests.Validation
{
    [TestFixture]
    public class CarValidatorTests
    {
        [Test]
        public void Should_loop_through_rules()
        {
            // arrange
            var ruleMock1 = new Mock<ICarValidationRule>();
            ruleMock1.Setup(x => x.IsValid(It.IsAny<Car>()))
                .Returns(true);

            var ruleMock2 = new Mock<ICarValidationRule>();
            ruleMock2.Setup(x => x.IsValid(It.IsAny<Car>()))
                .Returns(true);

            var sut = new CarValidator(new [] { ruleMock1.Object, ruleMock2.Object});

            var car = new Car();

            // act
            sut.IsValid(car);

            // assert
            ruleMock1.Verify(x => x.IsValid(car), Times.Once);
            ruleMock2.Verify(x => x.IsValid(car), Times.Once);
        }

        [Test]
        public void Should_stop_when_first_rule_fails()
        {
            // arrange
            var ruleMock1 = new Mock<ICarValidationRule>();
            ruleMock1.Setup(x => x.IsValid(It.IsAny<Car>()))
                .Returns(false);

            var ruleMock2 = new Mock<ICarValidationRule>();
            ruleMock2.Setup(x => x.IsValid(It.IsAny<Car>()))
                .Returns(true);

            var sut = new CarValidator(new[] { ruleMock1.Object, ruleMock2.Object });

            var car = new Car();

            // act
            sut.IsValid(car);

            // assert
            ruleMock1.Verify(x => x.IsValid(car), Times.Once);
            ruleMock2.Verify(x => x.IsValid(car), Times.Never);
        }

        [Test]
        public void Should_return_true_if_all_rules_return_true()
        {
            // arrange
            var ruleMock1 = new Mock<ICarValidationRule>();
            ruleMock1.Setup(x => x.IsValid(It.IsAny<Car>()))
                .Returns(true);

            var ruleMock2 = new Mock<ICarValidationRule>();
            ruleMock2.Setup(x => x.IsValid(It.IsAny<Car>()))
                .Returns(true);

            var sut = new CarValidator(new[] { ruleMock1.Object, ruleMock2.Object });

            var car = new Car();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeTrue();
        }

        [Test]
        public void Should_return_false_if_one_rule_returns_false()
        {
            // arrange
            var ruleMock1 = new Mock<ICarValidationRule>();
            ruleMock1.Setup(x => x.IsValid(It.IsAny<Car>()))
                .Returns(true);

            var ruleMock2 = new Mock<ICarValidationRule>();
            ruleMock2.Setup(x => x.IsValid(It.IsAny<Car>()))
                .Returns(false);

            var sut = new CarValidator(new[] { ruleMock1.Object, ruleMock2.Object });

            var car = new Car();

            // act
            var isValid = sut.IsValid(car);

            // assert
            isValid.Should().BeFalse();
        }
    }
}
