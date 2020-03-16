using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp6;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeaturesTests
{
    [TestClass]
    public class CSharp6Tests
    {
        [TestMethod]
        public void TestReadOnlyAutoProp()
        {
            MyImmutableType myImmutableType = new MyImmutableType(88m, DateTime.UtcNow);
            string display = myImmutableType.ToString();
            System.Diagnostics.Debug.WriteLine(display);
            // myImmutableType.AmountDeposit = 80000m; // Get this error: Property or indexer 'property' cannot be assigned to — it is read only
        }

        [TestMethod]
        public void TestAutoInitAndStringInterpolation()
        {
            Student student = new Student("Maggie", "May");
            student.Grades.AddRange(new List<double> { 4.0, 3.0, 4.0, 3.0, 3.5 });
            string fullName = student.FullName;
            Assert.AreEqual(fullName, "Maggie May");
            string grades = student.GetGradePointPercentage();
            Assert.AreEqual(grades, "3.5");
            FormattableString str = $"{student.Grades.Average()}";
            var gradeStr = str.ToString(new System.Globalization.CultureInfo("de-DE"));
            Assert.AreEqual(gradeStr, "3,5");
            // Need to cast to FormattableString
            gradeStr = ((FormattableString)($"{student.Grades.Average()}")).ToString(new System.Globalization.CultureInfo("de-DE"));
            Assert.AreEqual(gradeStr, "3,5");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestNameof()
        {
            Student student = new Student(null, "May");
        }

        [TestMethod]
        public void FilterExceptionTest()
        {
            Student student = new Student("John", "Tester");
            string errorMsg = student.RaiseException("Test", "Test");
            Assert.AreEqual(errorMsg, "Test");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void FilterExceptionTestUnhandled()
        {
            Student student = new Student("John", "Tester");
            student.RaiseException("Test", "Hi");
        }

        [TestMethod]
        public void TestNullConditionalOp()
        {
            Student student = new Student("Maggie", "May");
            student.FirstName = null;
            string first = student?.FirstName ?? "Unspecified";
            Assert.AreEqual(first, "Unspecified");
        }

        [TestMethod]
        public void TestInitAssociativeCollectionWithIndex()
        {
            Student student = new Student("Maggie", "May");
            string response = student.GetResponse(11);
            Assert.AreEqual(response, "How are you?");
        }

    }
}
