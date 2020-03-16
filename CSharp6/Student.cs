using System;
using System.Collections.Generic;
using System.Linq;
using static System.String; // using static

namespace CSharp6
{
    public class Student
    {
        public Student(string firstName, string lastName)
        {
            // using static enhancement and the nameof expression
            if (IsNullOrEmpty(firstName))
                throw new ArgumentException(message: "Must have a value.", paramName: nameof(firstName));
            if (IsNullOrEmpty(lastName))
                throw new ArgumentException(message: "Must have a value.", paramName: nameof(lastName));

            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        /// <summary>
        /// Auto-property initializers
        /// </summary>
        public List<double> Grades { get; } = new List<double>();

        /// <summary>
        /// String interpolation sample
        /// </summary>
        /// <returns></returns>
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// String interpolation sample
        /// </summary>
        public string GetGradePointPercentage() => $"{Grades.Average():F1}";

        /// <summary>
        /// String interpolation
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{LastName}, {FirstName}";

        public string RaiseException(string expectionErrorMessage, string allowedErrorMessage)
        {
            try
            {
                throw new Exception(expectionErrorMessage);
            }
            catch (Exception e) when (e.Message.Contains(allowedErrorMessage))
            {
                return allowedErrorMessage;
            }
        }

        private Dictionary<int, string> _responses = new Dictionary<int, string>
        {
            [11] = "How are you?",
            [5] = "Can you guess my name?",
            [8] = "My name is Simon. I like to draw."
        };

        public string GetResponse(int index)
        {
            return _responses[index];
        }

    }
}
