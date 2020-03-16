using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp7;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CSharpFeaturesTests
{
    [TestClass]
    public class CSharp7Tests
    {
        [TestMethod]
        public void TestImprovedOutVariable()
        {
            CoolStuff coolStuff = new CoolStuff();
            string parsedText = coolStuff.ParseStringAsInteger("88");
            Assert.AreEqual(parsedText, "88");
        }

        [TestMethod]
        public void SimpleTupleTests()
        {
            var p = new TuplePoint(3.14, 2.71);
            (double X, double Y) = p;
            Assert.AreEqual(X, 3.14);
            Assert.AreEqual(Y, 2.71);

            var unamed = ("one", "two");
            Assert.AreEqual(unamed.Item1, "one");
            var namedTuple = (First: "one", Second: "two");
            Assert.AreEqual(namedTuple.First, "one");
            Assert.AreEqual(namedTuple.Second, "two");

            var sum = 12.5;
            var count = 5;
            var accumulation = (count, sum);
            Assert.AreEqual(accumulation.sum, sum);

            var localVariableOne = 5;
            var localVariableTwo = "some text";

            var testTuple = (explicitFieldOne: localVariableOne, explicitFieldTwo: localVariableTwo);
            Assert.AreEqual(testTuple.explicitFieldOne, localVariableOne);

            var left = (a: 5, b: 10);
            var right = (a: 5, b: 10);
            Assert.AreEqual(left, right);

            // Lifted conversion
            (int a, int b)? nullableRightTuple = right;
            Assert.AreEqual(nullableRightTuple, right);

            // Nested tuples
            (int, (int, int)) nestedTuple = (1, (2, 3));
            Assert.AreEqual(nestedTuple,  (1, (2, 3)));

            // The 'arity' and 'shape' of all these tuples are compatible. 
            // The only difference is the field names being used.
            var unnamed = (42, "The meaning of life");
            var anonymous = (16, "a perfect square");
            var named = (Answer: 42, Message: "The meaning of life");
            var differentNamed = (SecretConstant: 42, Label: "The meaning of life");

            unnamed = named;
            named = unnamed;

            // 'named' still has fields that can be referred to as 'Answer', and 'Message':
            Assert.AreEqual(named.Answer, 42);
            Assert.AreEqual(named.Message, "The meaning of life");

            anonymous = unnamed;

            // named tuples.
            named = differentNamed;
            // The field names are not assigned. 'named' still has fields that can be referred to as 'Answer' and 'Message':
            Debug.WriteLine($"{named.Answer}, {named.Message}");

            // With implicit conversions:
            // int can be implicitly converted to long
            (long, string) conversion = named;
            Assert.AreEqual(conversion.Item1, 42L);

            double standardDeviation = CoolStuff.StandardDeviationWithTuple(new List<double>() { 11d, 22d, 33d, 44d, 55d, 66d, 77d, 88d, 99d, 111d });
            Assert.IsTrue(standardDeviation > 22d);

            // Inferred tuple element names
            int Id = 5;
            string LookupName = "CN0073893";
            var testInferredTuple = (Id, LookupName); // element names are "Id" and "LookupName"
            var sameAsTestTuple = (Id, LookupName);
            Assert.AreEqual(testInferredTuple.Id, Id);
            Assert.AreEqual(testInferredTuple.LookupName, LookupName);
            Assert.AreEqual(testInferredTuple, sameAsTestTuple);
            Assert.IsTrue(testInferredTuple == sameAsTestTuple);
        }

        [TestMethod]
        public void TupleDeconstructionTest()
        {
            var p = new Person("Althea", "Goodwin");
            var (first, last) = p;
            Assert.AreEqual(first, "Althea");
            Assert.AreEqual(last, "Goodwin");
            first = "Sally";    // Show that the p.FirstName is not updated. 
            Assert.AreEqual(p.FirstName, "Althea");
        }

        [TestMethod]
        public void TupleOutParameterTest()
        {
            Dictionary<int, (int, string)> dict = new Dictionary<int, (int, string)>();
            dict.Add(1, (234, "First!"));
            dict.Add(2, (345, "Second"));
            dict.Add(3, (456, "Last"));

            // TryGetValue already demonstrates using out parameters
            dict.TryGetValue(2, out (int num, string place) pair);

            Assert.AreEqual(pair.num, 345);
            Assert.AreEqual(pair.place, "Second");
        }

        [TestMethod]
        public void TestDiscards()
        {
            var (_, _, _, pop1, _, pop2) = CoolStuff.QueryCityDataForYears("New York City", 1960, 2010);
            Assert.AreEqual(pop1, 7781984);
            Assert.AreEqual(pop2, 8175133);
        }

        [TestMethod]
        public void TestPatternMatching()
        {
            List<object> listOfNumbers = new List<object>();
            listOfNumbers.Add(2);
            listOfNumbers.Add(new List<int>() { 1, 8, 1, 10 });
            listOfNumbers.Add(3);
            listOfNumbers.Add(-30);
            listOfNumbers.Add(0);
            int sumOfPositive = CoolStuff.SumPositiveNumbers(listOfNumbers);
            Assert.AreEqual(sumOfPositive, 25);
        }


        [TestMethod]
        public void TestRefLocalsAndReturns()
        {
            int[,] matrix = new int[,] { { 1, 321 }, { 3, 999 }, { 5, 634 }, { 7, 807 } };
            ref int itemInMatrix = ref CoolStuff.FindIntInMatrix(matrix, (val) => val == 999);
            itemInMatrix = 42;
            Assert.AreEqual(matrix[1, 1], 42);
        }

        [TestMethod]
        public void TestAlphabetSubset()
        {
            IEnumerable<char> charsList = CoolStuff.AlphabetSubset('c', 'u');
            Assert.AreEqual(charsList.ElementAt(3), 'f');
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestInvalidArgAlphabetSubset()
        {
            _ = CoolStuff.AlphabetSubset('-', 'u');
        }

        [TestMethod]
        public void TestLocalFunctionFactorial()
        {
            int factorialResult = CoolStuff.LocalFunctionFactorial(8);
            Assert.AreEqual(factorialResult, 40320);
            factorialResult = CoolStuff.LambdaFactorial(8);
            Assert.AreEqual(factorialResult, 40320);
        }

        [TestMethod]
        public void TestExpressionBodiedMembers()
        {
            Person p = new Person("Ben", "Fulfold");
            p.Title = "Journalist";
            Assert.AreEqual(p.Title, "Journalist");

            p.Title = null;
            Assert.AreEqual(p.Title, "Person");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestThrowExpress()
        {
            Person p = new Person("Ben", "Fulfold");
            p.Nickname = "Mr. Grumpy";
            Assert.AreEqual(p.Nickname, "Mr. Grumpy");
            p.Nickname = null;
        }

        [TestMethod]
        public void TestGeneralizedAsyncReturnType()
        {
            CoolStuff myCoolStuff = new CoolStuff();
            int luckNumber = myCoolStuff.GetMyLuckyNumber().GetAwaiter().GetResult();
            Assert.AreEqual(luckNumber, 88);
        }

        [TestMethod]
        public void TestLiteraSyntaxImprovements()
        {
            CoolStuff myCoolStuff = new CoolStuff();
            long more = myCoolStuff.AddSomeMorePlease(300_000_000_000);
            Assert.AreEqual(more, 400_000_000_000);
        }
    }
}
