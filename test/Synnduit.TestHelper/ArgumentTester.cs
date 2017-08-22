using System;
using FluentAssertions;

namespace Synnduit.TestHelper
{
    public static class ArgumentTester
    {
        public static void ThrowsArgumentException(
            Action invokeTestedMethod, string expectedParamName)
        {
            ThrowsArgumentException<ArgumentException>(
                invokeTestedMethod, expectedParamName);
        }

        public static void ThrowsArgumentNullException(
            Action invokeTestedMethod, string expectedParamName)
        {
            ThrowsArgumentException<ArgumentNullException>(
                invokeTestedMethod, expectedParamName);
        }

        private static void ThrowsArgumentException<TException>(
            Action invokeTestedMethod, string expectedParamName)
            where TException : ArgumentException
        {
            // arrange
            TException exceptionThrown = null;

            // act
            try
            {
                invokeTestedMethod();
            }
            catch(TException exception)
            {
                exceptionThrown = exception;
            }

            // assert
            exceptionThrown.Should().NotBeNull();
            exceptionThrown.ParamName.Should().Be(expectedParamName);
        }
    }
}
