using System;
using NUnit.Framework;
using TinyRx;

namespace TinyRx.Tests.Observer {
    public class ObserverTests {
        [Test]
        public void OnNextが正しく呼び出されること() {
            // Arrange
            var value = 0;
            var observer = new Observer<int>(
                onNext: x => value = x,
                onError: null,
                onCompleted: null
            );

            // Act
            observer.OnNext(42);

            // Assert
            Assert.That(value, Is.EqualTo(42));
        }

        [Test]
        public void OnCompletedが正しく呼び出されること() {
            // Arrange
            var completed = false;
            var observer = new Observer<int>(
                onNext: null,
                onError: null,
                onCompleted: () => completed = true
            );

            // Act
            observer.OnCompleted();

            // Assert
            Assert.That(completed, Is.True);
        }

        [Test]
        public void OnErrorが正しく呼び出されること() {
            // Arrange
            Exception capturedException = null;
            var observer = new Observer<int>(
                onNext: null,
                onError: ex => capturedException = ex,
                onCompleted: null
            );
            var expectedException = new Exception("Test Exception");

            // Act
            observer.OnError(expectedException);

            // Assert
            Assert.That(capturedException, Is.SameAs(expectedException));
        }

        [Test]
        public void OnErrorハンドラが設定されていない場合_例外が伝播すること() {
            // Arrange
            var observer = new Observer<int>(
                onNext: null,
                onError: null,
                onCompleted: null
            );
            var expectedException = new Exception("Test Exception");

            // Act & Assert
            var actualException = Assert.Throws<Exception>(() => observer.OnError(expectedException));
            Assert.That(actualException, Is.SameAs(expectedException));
        }
    }
} 