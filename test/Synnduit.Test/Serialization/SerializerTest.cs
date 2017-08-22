using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Synnduit.Serialization
{
    [TestClass]
    public class SerializerTest
    {
        private static string[] EntityPropertyNames = new[]
        {
            "Bravo",            // has a nullable proxy
            "Delta",
            "Foxtrot",
            "Hotel",
            "India",
            "Juliet"            // has a nullable proxy
        };

        [TestMethod]
        public void Serialize_Serializes_All_Entity_Properties_Into_Json()
        {
            // arrange
            var serializableObject = new SerializableObject()
            {
                Alpha = "Baby you can drive my car!",
                ProxyBravo = 56.57m,
                Charlie = 24,
                Delta = 86.88d,
                Echo = new DateTime(1985, 3, 5, 12, 2, 5),
                Foxtrot = "I want to hold your hand!",
                Golf = 77559.22m,
                Hotel = 32,
                India = 3.14d,
                ProxyJuliet = new DateTime(2006, 3, 10, 9, 20, 46)
            };
            var serializer = new Serializer<SerializableObject>(new FakeMetadataParser());

            // act
            byte[] serializedObject = serializer.Serialize(serializableObject);

            // assert
            SerializableObject deserializedObject
                = this.DeserializeObject(serializedObject);
            deserializedObject.Alpha.Should().BeNull();
            deserializedObject.Bravo.Should().Be(56.57m);
            deserializedObject.ProxyBravo.Should().NotHaveValue();
            deserializedObject.Charlie.Should().NotHaveValue();
            deserializedObject.Delta.Should().Be(86.88d);
            deserializedObject.Echo.Should().NotHaveValue();
            deserializedObject.Foxtrot.Should().Be("I want to hold your hand!");
            deserializedObject.Golf.Should().NotHaveValue();
            deserializedObject.Hotel.Should().Be(32);
            deserializedObject.India.Should().NotHaveValue();
            deserializedObject.Juliet.Should().Be(new DateTime(2006, 3, 10, 9, 20, 46));
            deserializedObject.ProxyJuliet.Should().NotHaveValue();
        }

        [TestMethod]
        public void Serialize_Ignores_Null_Values()
        {
            // arrange
            var serializableObject = new SerializableObject();
            var serializer = new Serializer<SerializableObject>(new FakeMetadataParser());

            // act
            byte[] serializedObject = serializer.Serialize(serializableObject);

            // assert
            string serializedObjectAsString =
                Encoding.UTF8.GetString(serializedObject);
            serializedObjectAsString.Should().NotContain("Bravo");
            serializedObjectAsString.Should().Contain("Delta");
            serializedObjectAsString.Should().NotContain("Foxtrot");
            serializedObjectAsString.Should().NotContain("Hotel");
            serializedObjectAsString.Should().NotContain("Juliet");
        }

        [TestMethod]
        public void Deserialize_Deserializes_From_Json()
        {
            // arrange
            byte[] serializedObject = this.SerializeObject(
                new SerializableObject()
                {
                    Alpha = "Ville",
                    Bravo = 525600.00m,
                    Charlie = 56,
                    Delta = 3.141592654d,
                    Echo = new DateTime(1994, 8, 30, 12, 6, 26),
                    Foxtrot = "Forever Young",
                    Golf = 74.82m,
                    Hotel = 75,
                    India = 2.718d,
                    Juliet = new DateTime(2001, 12, 19, 16, 50, 34)
                });
            var serializer = new Serializer<SerializableObject>(new FakeMetadataParser());

            // act
            SerializableObject deserializedObject =
                serializer.Deserialize(serializedObject);

            // assert
            deserializedObject.Alpha.Should().BeNull();
            deserializedObject.Bravo.Should().Be(default(decimal));
            deserializedObject.ProxyBravo.Should().Be(525600.00m);
            deserializedObject.Charlie.Should().NotHaveValue();
            deserializedObject.Delta.Should().Be(3.141592654d);
            deserializedObject.Echo.Should().NotHaveValue();
            deserializedObject.Foxtrot.Should().Be("Forever Young");
            deserializedObject.Golf.Should().NotHaveValue();
            deserializedObject.Hotel.Should().Be(75);
            deserializedObject.India.Should().NotHaveValue();
            deserializedObject.Juliet.Should().Be(default(DateTime));
            deserializedObject.ProxyJuliet.Should().Be(
                new DateTime(2001, 12, 19, 16, 50, 34));
        }

        [TestMethod]
        public void Serializer_ContractResolver_Caches_Properties()
        {
            // arrange
            var serializableObject = new SerializableObject()
            {
                Alpha = "Bravo",
                Bravo = decimal.MaxValue
            };
            var metadataParser = new FakeMetadataParser();
            var serializer = new Serializer<SerializableObject>(metadataParser);

            // act
            serializer.Serialize(serializableObject);
            int metadataGetterCallCountAfterFirstUse
                = metadataParser.MetadataGetterCallCount;
            byte[] serializedObject = serializer.Serialize(serializableObject);
            serializer.Deserialize(serializedObject);
            serializer.Deserialize(serializedObject);

            // assert
            metadataParser
                .MetadataGetterCallCount
                .Should()
                .Be(metadataGetterCallCountAfterFirstUse);
        }

        private SerializableObject DeserializeObject(byte[] serializedObject)
        {
            using(var reader = new StreamReader(new MemoryStream(serializedObject)))
            {
                var serializer = new JsonSerializer();
                return (SerializableObject) serializer.Deserialize(
                    reader, typeof(SerializableObject));
            }
        }

        private byte[] SerializeObject(SerializableObject serializableObject)
        {
            var stream = new MemoryStream();
            var serializer = new JsonSerializer();
            using(var writer = new StreamWriter(stream))
            {
                serializer.Serialize(writer, serializableObject);
            }
            return stream.ToArray();
        }

        private class SerializableObject
        {
            public string Alpha { get; set; }

            public decimal Bravo { get; set; }

            public decimal? ProxyBravo { get; set; }

            public int? Charlie { get; set; }

            public double Delta { get; set; }

            public DateTime? Echo { get; set; }

            public string Foxtrot { get; set; }

            public decimal? Golf { get; set; }

            public int? Hotel { get; set; }

            [JsonIgnore]
            public double? India { get; set; }

            public DateTime Juliet { get; set; }

            public DateTime? ProxyJuliet { get; set; }
        }

        private class FakeMetadataParser : IMetadataParser<SerializableObject>
        {
            private readonly EntityTypeMetadata metadata;

            public FakeMetadataParser()
            {
                this.metadata = new EntityTypeMetadata(
                    null,
                    null,
                    typeof(SerializableObject)
                    .GetProperties()
                    .Where(property => EntityPropertyNames.Contains(property.Name))
                    .Select(property => new EntityProperty(
                        property, this.GetProxyProperty(property), null, true, false, -1))
                    .ToArray(),
                    null,
                    null);
                this.MetadataGetterCallCount = 0;
            }

            public EntityTypeMetadata Metadata
            {
                get
                {
                    this.MetadataGetterCallCount++;
                    return this.metadata;
                }
            }

            public int MetadataGetterCallCount { get; private set; }

            private PropertyInfo GetProxyProperty(PropertyInfo property)
            {
                return
                    typeof(SerializableObject)
                    .GetProperty("Proxy" + property.Name);
            }
        }
    }
}
