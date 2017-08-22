using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Synnduit
{
    [TestClass]
    public class EntityIdentifierConverterTest
    {
        [TestMethod]
        public void FromValue_Returns_Null_If_Value_Null()
        {
            this.CreateConverter()
                .FromValue(null)
                .Should()
                .BeNull();
        }

        [TestMethod]
        public void FromValue_Converts_String_Value_To_Expected_Identifier()
        {
            this.CreateConverter()
                .FromValue("A built-in remedy")
                .Should()
                .Be(new EntityIdentifier("A built-in remedy"));
        }

        [TestMethod]
        public void FromValue_Converts_Guid_Value_To_Expected_Identifier()
        {
            this.CreateConverter()
                .FromValue(Guid.Parse("F189E51E-6307-45CC-A6D8-0B514BA2E9DF"))
                .Should()
                .Be(new EntityIdentifier(
                    new Guid("F189E51E-6307-45CC-A6D8-0B514BA2E9DF")));
        }

        [TestMethod]
        public void FromValue_Converts_Int32_Value_To_Expected_Identifier()
        {
            this.CreateConverter()
                .FromValue(88757)
                .Should()
                .Be(new EntityIdentifier(88757));
        }

        [TestMethod]
        public void FromValue_Converts_Int64_Value_To_Expected_Identifier()
        {
            this.CreateConverter()
                .FromValue(199358329843L)
                .Should()
                .Be(new EntityIdentifier(199358329843L));
        }

        [TestMethod]
        public void FromValue_Converts_UInt32_Value_To_Expected_Identifier()
        {
            this.CreateConverter()
                .FromValue(8937447U)
                .Should()
                .Be(new EntityIdentifier(8937447U));
        }

        [TestMethod]
        public void FromValue_Converts_UInt64_Value_To_Expected_Identifier()
        {
            this.CreateConverter()
                .FromValue(89437297323987UL)
                .Should()
                .Be(new EntityIdentifier(89437297323987UL));
        }

        [TestMethod]
        public void FromValue_Converts_Custom_Type_Instances_To_Expected_Identifiers()
        {
            var converter = this.CreateConverter();
            converter
                .FromValue(new CustomValueTypeIdentifier(27, 'c'))
                .Should()
                .Be(new EntityIdentifier("27,c"));
            converter
                .FromValue(new CustomReferenceTypeIdentifier("We Can Be Heroes", true))
                .Should()
                .Be(new EntityIdentifier("We Can Be Heroes-true"));
            converter
                .FromValue(new CustomValueTypeIdentifier(57, 't'))
                .Should()
                .Be(new EntityIdentifier("57,t"));
            converter
                .FromValue(new CustomReferenceTypeIdentifier("Just For One Day", false))
                .Should()
                .Be(new EntityIdentifier("Just For One Day-false"));
        }

        [TestMethod]
        public void FromValue_Wraps_Data_Conversion_Exception()
        {
            // arrange
            InvalidOperationException exceptionThrown = null;

            // act
            try
            {
                this.CreateConverter()
                    .FromValue(new string('k', EntityIdentifier.MaxLength + 1));
            }
            catch(InvalidOperationException exception)
            {
                exceptionThrown = exception;
            }

            // assert
            exceptionThrown
                .Should()
                .NotBeNull();
            exceptionThrown
                .InnerException
                .Should()
                .BeOfType<ArgumentException>();
        }

        [TestMethod]
        public void FromValue_Converts_EntityIdentifier_Value_To_Expected_Identifier()
        {
            this.CreateConverter()
                .FromValue(new EntityIdentifier("Bumblebee"))
                .Should()
                .Be(new EntityIdentifier("Bumblebee"));
        }

        [TestMethod]
        public void FromValue_Throws_InvalidOperationException_If_Value_Type_Not_Supported()
        {
            // arrange
            InvalidOperationException exceptionThrown = null;

            // act
            try
            {
                this.CreateConverter()
                    .FromValue(3.141592684d);
            }
            catch(InvalidOperationException exception)
            {
                exceptionThrown = exception;
            }

            // assert
            exceptionThrown.Should().NotBeNull();
        }

        [TestMethod]
        public void ToValue_Throws_ArgumentNullException_If_Type_Is_Null()
        {
            // arrange
            ArgumentNullException exceptionThrown = null;

            // act
            try
            {
                this.CreateConverter()
                    .ToValue("identifier", null);
            }
            catch(ArgumentNullException exception)
            {
                exceptionThrown = exception;
            }

            // assert
            exceptionThrown.Should().NotBeNull();
            exceptionThrown.ParamName.Should().Be("type");
        }

        [TestMethod]
        public void ToValue_Returns_Null_If_Identifier_Null()
        {
            // arrange
            EntityIdentifierConverter converter = this.CreateConverter();

            // act
            object valueOne = converter.ToValue(null, typeof(string));
            object valueTwo = converter.ToValue(null, typeof(Guid));
            object valueThree = converter.ToValue(null, typeof(double));
            object valueFour = converter.ToValue(null, typeof(CustomValueTypeIdentifier));
            object valueFive = converter.ToValue(
                null, typeof(CustomReferenceTypeIdentifier));
            object valueSix = converter.ToValue(null, typeof(TestMethodAttribute));
            object valueSeven = converter.ToValue(null, typeof(EntityTransactionOutcome));

            // assert
            valueOne.Should().BeNull();
            valueTwo.Should().BeNull();
            valueThree.Should().BeNull();
            valueFour.Should().BeNull();
            valueFive.Should().BeNull();
            valueSix.Should().BeNull();
            valueSeven.Should().BeNull();
        }

        [TestMethod]
        public void ToValue_Converts_Identifier_To_String()
        {
            this.CreateConverter()
                .ToValue("I'll light the fire ... ", typeof(string))
                .Should()
                .Be("I'll light the fire ... ");
        }

        [TestMethod]
        public void ToValue_Converts_Identifier_To_Guid()
        {
            this.CreateConverter()
                .ToValue("28219543-9607-459A-9536-86CF4EF0DF51", typeof(Guid))
                .Should()
                .Be(Guid.Parse("28219543-9607-459A-9536-86CF4EF0DF51"));
        }

        [TestMethod]
        public void ToValue_Converts_Identifier_To_Nullable_Guid()
        {
            this.CreateConverter()
                .ToValue("CBA4F6DF-D20E-4C28-99C7-00212AF2EA9D", typeof(Guid?))
                .Should()
                .Be(Guid.Parse("CBA4F6DF-D20E-4C28-99C7-00212AF2EA9D"));
        }

        [TestMethod]
        public void ToValue_Converts_Identifier_To_Int32()
        {
            this.CreateConverter()
                .ToValue("8875", typeof(int))
                .Should()
                .Be(8875);
        }

        [TestMethod]
        public void ToValue_Converts_Identifier_To_Nullable_Int32()
        {
            this.CreateConverter()
                .ToValue("12345", typeof(int?))
                .Should()
                .Be(12345);
        }

        [TestMethod]
        public void ToValue_Converts_Identifier_To_Int64()
        {
            this.CreateConverter()
                .ToValue("92837429387429387", typeof(long))
                .Should()
                .Be(92837429387429387L);
        }

        [TestMethod]
        public void ToValue_Converts_Identifier_To_Nullable_Int64()
        {
            this.CreateConverter()
                .ToValue("2893574928374777", typeof(long?))
                .Should()
                .Be(2893574928374777L);
        }

        [TestMethod]
        public void ToValue_Converts_Identifier_To_UInt32()
        {
            this.CreateConverter()
                .ToValue("934857955", typeof(uint))
                .Should()
                .Be(934857955U);
        }

        [TestMethod]
        public void ToValue_Converts_Identifier_To_Nullable_UInt32()
        {
            this.CreateConverter()
                .ToValue("34883488", typeof(uint?))
                .Should()
                .Be(34883488U);
        }

        [TestMethod]
        public void ToValue_Converts_Identifier_To_UInt64()
        {
            this.CreateConverter()
                .ToValue("5534854758455665", typeof(ulong))
                .Should()
                .Be(5534854758455665UL);
        }

        [TestMethod]
        public void ToValue_Converts_Identifier_To_Nullable_UInt64()
        {
            this.CreateConverter()
                .ToValue("33449358349582394", typeof(ulong?))
                .Should()
                .Be(33449358349582394UL);
        }

        [TestMethod]
        public void ToValue_Converts_Identifier_To_Custom_Type_Instances()
        {
            var converter = this.CreateConverter();
            converter
                .ToValue("877,z", typeof(CustomValueTypeIdentifier))
                .Should()
                .Be(new CustomValueTypeIdentifier(877, 'z'));
            converter
                .ToValue("Detroit Red Wings-true", typeof(CustomReferenceTypeIdentifier))
                .Should()
                .Be(new CustomReferenceTypeIdentifier("Detroit Red Wings", true));
            converter
                .ToValue("7,u", typeof(CustomValueTypeIdentifier?))
                .Should()
                .Be(new CustomValueTypeIdentifier(7, 'u'));
            converter
                .ToValue("99,p", typeof(CustomValueTypeIdentifier))
                .Should()
                .Be(new CustomValueTypeIdentifier(99, 'p'));
            converter
                .ToValue("New Jersey Devils-false", typeof(CustomReferenceTypeIdentifier))
                .Should()
                .Be(new CustomReferenceTypeIdentifier("New Jersey Devils", false));
            converter
                .ToValue("173,m", typeof(CustomValueTypeIdentifier?))
                .Should()
                .Be(new CustomValueTypeIdentifier(173, 'm'));
        }

        [TestMethod]
        public void ToValue_Converts_Identifier_To_EntityIdentifier()
        {
            this.CreateConverter()
                .ToValue("BCCB", typeof(EntityIdentifier))
                .Should()
                .Be(new EntityIdentifier("BCCB"));
        }

        [TestMethod]
        public void ToValue_Wraps_Data_Conversion_Exception()
        {
            // arrange
            InvalidOperationException exceptionThrown = null;

            // act
            try
            {
                this.CreateConverter()
                    .ToValue("definitely not a GUID", typeof(Guid));
            }
            catch(InvalidOperationException exception)
            {
                exceptionThrown = exception;
            }

            // assert
            exceptionThrown
                .Should()
                .NotBeNull();
            exceptionThrown
                .InnerException
                .Should()
                .BeOfType<FormatException>();
        }

        [TestMethod]
        public void ToValue_Throws_InvalidOperationException_If_Value_Type_Not_Supported()
        {
            // arrange
            InvalidOperationException exceptionThrown = null;

            // act
            try
            {
                this.CreateConverter()
                    .ToValue("2.718d", typeof(double));
            }
            catch(InvalidOperationException exception)
            {
                exceptionThrown = exception;
            }

            // assert
            exceptionThrown.Should().NotBeNull();
        }

        private EntityIdentifierConverter CreateConverter()
        {
            return new EntityIdentifierConverter();
        }

        private struct CustomValueTypeIdentifier
        {
            public static implicit operator
                EntityIdentifier(CustomValueTypeIdentifier value)
            {
                return new EntityIdentifier($"{value.Alpha},{value.Bravo}");
            }

            public static explicit operator
                CustomValueTypeIdentifier(EntityIdentifier identifier)
            {
                string[] values = ((string) identifier).Split(',');
                return new CustomValueTypeIdentifier(
                    int.Parse(values[0]),
                    values[1][0]);
            }

            public CustomValueTypeIdentifier(int alpha, char bravo)
            {
                this.Alpha = alpha;
                this.Bravo = bravo;
            }

            public int Alpha { get; private set; }

            public char Bravo { get; private set; }

            public override int GetHashCode()
            {
                return $"{this.Alpha},{this.Bravo}".GetHashCode();
            }

            public override bool Equals(object obj)
            {
                bool equals = false;
                if(obj is CustomValueTypeIdentifier)
                {
                    var other = (CustomValueTypeIdentifier) obj;
                    equals =
                        this.Alpha == other.Alpha &&
                        this.Bravo == other.Bravo;
                }
                return equals;
            }
        }

        public class CustomReferenceTypeIdentifier
        {
            public static explicit operator
                EntityIdentifier(CustomReferenceTypeIdentifier value)
            {
                return $"{value.Delta}-{value.Echo}";
            }

            public static implicit operator
                CustomReferenceTypeIdentifier(EntityIdentifier identifier)
            {
                string[] values = ((string) identifier).Split('-');
                return new CustomReferenceTypeIdentifier(
                    values[0],
                    bool.Parse(values[1]));
            }

            public CustomReferenceTypeIdentifier(string delta, bool echo)
            {
                this.Delta = delta;
                this.Echo = echo;
            }

            public string Delta { get; private set; }

            public bool Echo { get; private set; }

            public override int GetHashCode()
            {
                return $"{this.Delta}-{this.Echo}".GetHashCode();
            }

            public override bool Equals(object obj)
            {
                bool equals = false;
                var other = obj as CustomReferenceTypeIdentifier;
                if(other != null)
                {
                    equals =
                        this.Delta == other.Delta &&
                        this.Echo == other.Echo;
                }
                return equals;
            }
        }
    }
}
