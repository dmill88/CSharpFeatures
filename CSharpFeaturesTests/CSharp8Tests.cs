using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharp8;
using System.Linq;
using System.Diagnostics;
using CSharp8NonNullRef;
using System.Threading.Tasks;

namespace CSharpFeaturesTests
{
    [TestClass]
    public class CSharp8Tests
    {
        [TestMethod]
        public void TestAsynchronousStreams()
        {
            Task.Run(async () =>
            {
                await foreach (var number in CoolNewCSharp8Stuff.GenerateSequence())
                {
                    Debug.WriteLine(number);
                }
            }).GetAwaiter().GetResult();
            Debug.WriteLine("Done");
        }

        [TestMethod]
        public void TestIndicesAndRanges()
        {
            string[] words = CoolNewCSharp8Stuff.GetMeSomeWordsWillYa();
            string dog = words[^1];
            Assert.AreEqual(dog, "dog");
            string[] quickBrownFox = words[1..4];
            Assert.AreEqual(quickBrownFox[2], "fox");
            Assert.AreEqual(quickBrownFox[^1], "fox");

            string[] allWords = words[..]; // contains "The" through "dog".
            Assert.AreEqual(allWords[^1], "dog");

            string[] firstPhrase = words[..4]; // contains "The" through "fox"
            Assert.AreEqual(firstPhrase[1], "quick");

            string[] lastPhrase = words[6..]; // contains "the", "lazy" and "dog"
            Assert.AreEqual(lastPhrase[^2], "lazy");

            Range phrase = 1..4;
            firstPhrase = words[phrase];
            Assert.AreEqual(firstPhrase[2], "fox");
        }

        [TestMethod]
        public void TestNullCoalescingAssignment()
        {
            List<int>? numbers = null;
            int? i = null;

            numbers ??= new List<int>();
            numbers.Add(i ??= 17);
            numbers.Add(i ??= 20);// Will add 17, since i is assigned to 17. 
            Assert.AreEqual(i, 17);
            Assert.AreEqual(numbers[0], 17);
            Assert.AreEqual(numbers[1], 17);
        }

        [TestMethod]
        public void TestNoNullRef()
        {
            var surveyRun = new SurveyRun();
            surveyRun.AddQuestion(QuestionType.YesNo, "Has your code ever thrown a NullReferenceException?");
            surveyRun.AddQuestion(new SurveyQuestion(QuestionType.Number, "How many times (to the nearest 100) has that happened?"));
            surveyRun.AddQuestion(QuestionType.Text, "What is your favorite color?");
            // surveyRun.AddQuestion(QuestionType.Text, default); // Generates warning CS8625: Cannot convert null literal to non-nullable reference type.
            
            surveyRun.PerformSurvey(8);

            foreach (var participant in surveyRun.AllParticipants)
            {
                Debug.WriteLine($"Participant: {participant.Id}:");
                if (participant.AnsweredSurvey)
                {
                    for (int i = 0; i < surveyRun.Questions.Count; i++)
                    {
                        var answer = participant.Answer(i);
                        Debug.WriteLine($"\t{surveyRun.GetQuestion(i).QuestionText} : {answer}");
                    }
                }
                else
                {
                    Debug.WriteLine("\tNo responses");
                }
            }
        }

        [TestMethod]
        public void TestPropertyPatternMatching()
        {
            RadioBand amRadio = new RadioBand(1, "AM", "The first radio transmission");
            decimal maxDistance = CoolNewCSharp8Stuff.MaxDistance(amRadio);
            Assert.AreEqual(maxDistance, 2000m);
        }

        [TestMethod]
        public void TestStaticLocalFunction()
        {
            CoolNewCSharp8Stuff cnStuff = new CoolNewCSharp8Stuff();
            int luckNum = cnStuff.AddNumbersUsingStaticLocalFunction(2, 2);
            Assert.AreEqual(luckNum, 4);
        }

        [TestMethod]
        public void TestTuplePatternMatching()
        {
            string result = CoolNewCSharp8Stuff.RockPaperScissors("paper", "scissors");
            Assert.AreEqual(result, "paper is cut by scissors. Scissors wins.");
            
            Quadrant theSecondSection = CoolNewCSharp8Stuff.GetQuadrant(new Point(-1, 1));
            Assert.AreEqual(theSecondSection, Quadrant.Two);
        }

        [TestMethod]
        public void TestUsingDeclaration()
        {
            bool goIntoScope = true;
            if (goIntoScope)
            {
                using MyDisposable myDisposableClass = new MyDisposable();
                myDisposableClass.HappyInteger = 168;
            }
        }

        [TestMethod]
        public void TestInterfaceDefaultImplementation()
        {
            Customer c = new Customer("customer one", new DateTime(2010, 5, 31))
            {
                Reminders =
                {
                    { new DateTime(2010, 08, 12), "childs's birthday" },
                    { new DateTime(1012, 11, 15), "anniversary" }
                }
            };

            SaleOrder o = new SaleOrder(new DateTime(2012, 6, 1), 5m);
            c.AddOrder(o);

            o = new SaleOrder(new DateTime(2103, 7, 4), 25m);
            c.AddOrder(o);

            Debug.WriteLine($"Current discount: {c.ComputeLoyaltyDiscount()}");

            // Add more orders to get the discount:
            DateTime recurring = new DateTime(2013, 3, 15);
            for (int i = 0; i < 15; i++)
            {
                o = new SaleOrder(recurring, 19.23m * i);
                c.AddOrder(o);

                recurring.AddMonths(2);
            }

            Debug.WriteLine($"Data about {c.Name}");
            Debug.WriteLine($"Joined on {c.DateJoined}. Made {c.PreviousOrders.Count()} orders, the last on {c.LastOrder}");
            Debug.WriteLine("Reminders:");
            foreach (var item in c.Reminders)
            {
                Debug.WriteLine($"\t{item.Value} on {item.Key}");
            }
            foreach (IOrder order in c.PreviousOrders)
                Debug.WriteLine($"Order on {order.Purchased} for {order.Cost}");

            decimal discountAmount = c.ComputeLoyaltyDiscount();
            Assert.AreEqual(discountAmount, 0.10m);

            // Set new calculation values in the interface.
            ICustomer.SetLoyaltyThresholds(new TimeSpan(30, 0, 0, 0), 1, 0.25m);
            discountAmount = c.ComputeLoyaltyDiscount();
            Assert.AreEqual(discountAmount, 0.25m);
        }
    }
}
