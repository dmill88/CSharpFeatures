using CSharp9;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpFeaturesTests
{
    [TestClass]
    public class CSharp9Tests
    {


        [TestMethod]
        public void RecordsValueEquilityTests()
        {
            var phoneNumbers = new string[2];
            Person person1 = new("Nancy", "Davolio", phoneNumbers);
            Person person2 = new("Nancy", "Davolio", phoneNumbers);
            Assert.AreEqual(person1, person2);

            person1.PhoneNumbers[0] = "555-1234";
            Assert.AreEqual(person1, person2);

            Assert.IsFalse(ReferenceEquals(person1, person2));
        }

        [TestMethod]
        public void RecordsNondestructiveMutationTests()
        {
            Person person1 = new("Nancy", "Davolio", new string[1]);

            Person person2 = person1 with { FirstName = "John" };
            Assert.AreNotEqual(person1, person2);

            person2 = person1 with { PhoneNumbers = new string[1] };
            Assert.AreNotEqual(person1, person2);

            person2 = person1 with { };
            Assert.AreEqual(person1, person2);
        }

        [TestMethod]
        public void RecordsInheritanceTests()
        {
            string[] phoneNumbers = new string[1];
            Person teacher = new Teacher("Nancy", "Davolio", phoneNumbers, 3);
            Person student = new Student("Nancy", "Davolio", phoneNumbers, 3);
            Assert.AreNotEqual(teacher, student);

            Student student2 = new Student("Nancy", "Davolio", phoneNumbers, 3);
            Assert.AreEqual(student2, student);
        }

        [TestMethod]
        public void InitOnlySettersTests()
        {
            var weatherObservation = new WeatherObservation
            {
                RecordedAt = DateTime.Now,
                TemperatureInCelsius = 20,
                PressureInMillibars = 998.0m
            };
            Assert.AreEqual(weatherObservation.TemperatureInCelsius, 20);
            //    weatherObservation.TemperatureInCelsius = 22; // Throws compiler error. 
        }

        [TestMethod]
        public void PatternMatchingTests()
        {
            Assert.IsFalse(PatterMatchers.IsLetter('8'));
            Assert.IsTrue(PatterMatchers.IsLetter('c'));
            Assert.IsFalse(PatterMatchers.IsLetterOrSeparator('8'));
            Assert.IsTrue(PatterMatchers.IsLetterOrSeparator('c'));
            string myNullString = null;
            Assert.IsTrue((myNullString is null));
            myNullString = "hi";
            Assert.IsTrue((myNullString is not null));
        }

    }
}


