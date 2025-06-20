using System.Collections.Generic;
using NUnit.Framework;

namespace TinyRx.Tests.ObservableProperty {
    [TestFixture]
    public class ObservablePropertyTests {
        [Test]
        public void 正常系_値の変更が通知されること() {
            // Arrange
            var values = new List<int>();
            var property = new ObservableProperty<int>(0);
            property.Subscribe(new Observer<int>(
                onNext: value => values.Add(value),
                onError: _ => Assert.Fail("エラーが発生してはいけない"),
                onCompleted: () => { }
            ));

            // Act
            property.Value = 42;

            // Assert
            CollectionAssert.AreEqual(new[] { 0, 42 }, values);
        }

        [Test]
        public void 正常系_同じ値の場合は通知されないこと() {
            // Arrange
            var values = new List<int>();
            var property = new ObservableProperty<int>(42);
            property.Subscribe(new Observer<int>(
                onNext: value => values.Add(value),
                onError: _ => Assert.Fail("エラーが発生してはいけない"),
                onCompleted: () => { }
            ));

            // Act
            property.Value = 42;

            // Assert
            CollectionAssert.AreEqual(new[] { 42 }, values);
        }

        [Test]
        public void 正常系_ReadOnlyObservablePropertyで値の変更が通知されること() {
            // Arrange
            var values = new List<int>();
            var property = new ObservableProperty<int>(0);
            var readOnlyProperty = property.ToReadOnly();
            readOnlyProperty.Subscribe(new Observer<int>(
                onNext: value => values.Add(value),
                onError: _ => Assert.Fail("エラーが発生してはいけない"),
                onCompleted: () => { }
            ));

            // Act
            property.Value = 42;

            // Assert
            CollectionAssert.AreEqual(new[] { 0, 42 }, values);
        }

        [Test]
        public void 正常系_ReadOnlyObservablePropertyの値が取得できること() {
            // Arrange
            var property = new ObservableProperty<int>(42);
            var readOnlyProperty = property.ToReadOnly();

            // Act & Assert
            Assert.That(readOnlyProperty.Value, Is.EqualTo(42));
        }

        [Test]
        public void 正常系_複数の型のObservablePropertyを管理できること() {
            // Arrange
            var intValues = new List<int>();
            var stringValues = new List<string>();
            var boolValues = new List<bool>();

            var intProperty = new ObservableProperty<int>(0);
            var stringProperty = new ObservableProperty<string>("");
            var boolProperty = new ObservableProperty<bool>(false);

            intProperty.Subscribe(new Observer<int>(
                onNext: value => intValues.Add(value),
                onError: _ => Assert.Fail("エラーが発生してはいけない"),
                onCompleted: () => { }
            ));

            stringProperty.Subscribe(new Observer<string>(
                onNext: value => stringValues.Add(value),
                onError: _ => Assert.Fail("エラーが発生してはいけない"),
                onCompleted: () => { }
            ));

            boolProperty.Subscribe(new Observer<bool>(
                onNext: value => boolValues.Add(value),
                onError: _ => Assert.Fail("エラーが発生してはいけない"),
                onCompleted: () => { }
            ));

            // Act
            intProperty.Value = 42;
            stringProperty.Value = "test";
            boolProperty.Value = true;

            // Assert
            CollectionAssert.AreEqual(new[] { 0, 42 }, intValues);
            CollectionAssert.AreEqual(new[] { "", "test" }, stringValues);
            CollectionAssert.AreEqual(new[] { false, true }, boolValues);
        }
    }
} 
