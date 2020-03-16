using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp7
{
    public class Person
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Person(string first, string last)
        {
            FirstName = first;
            LastName = last;
            _nickname = $"{last}ator";
        }

        public void Deconstruct(out string firstName, out string lastName)
        {
            firstName = FirstName;
            lastName = LastName;
        }

        private string _title = "Person";

        // Expression-bodied get / set accessors.
        public string Title
        {
            get => _title;
            set => this._title = value ?? "Person";
        }

        private string _nickname;
        public string Nickname 
        { 
            get => _nickname; 
            set => _nickname = value ??
        throw new ArgumentNullException(paramName: nameof(value), message: "Nickname cannot be null");
        }

    }
}
