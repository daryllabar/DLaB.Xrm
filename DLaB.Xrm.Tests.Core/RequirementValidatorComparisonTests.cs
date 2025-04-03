using DLaB.Xrm.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Source.DLaB.Xrm.Plugin;
using System;
using System.Collections.Generic;
using MessageType = Source.DLaB.Xrm.Plugin.MessageType;

namespace DLaB.Xrm.Tests.Core
{
    [TestClass]
    public class RequirementValidatorComparisionTests
    {
        private static List<Contact> PossibleValues { get; } = new()
        {
            new () { AssistantName = null },
            new () { AssistantName = null, NickName = null },
            new () { AssistantName = null, NickName = string.Empty },
            new () { AssistantName = null, NickName = "NotNull" },
            new () { AssistantName = string.Empty },
            new () { AssistantName = string.Empty, NickName = null },
            new () { AssistantName = string.Empty, NickName = string.Empty},
            new () { AssistantName = string.Empty, NickName = "NotNull"},
            new () { AssistantName = "NotNull" },
            new () { AssistantName = "NotNull", NickName = null },
            new () { AssistantName = "NotNull", NickName = string.Empty },
            new () { AssistantName = "NotNull", NickName = "NotNull" },
            new (),
            new () { NickName = null },
            new () { NickName = string.Empty },
            new () { NickName = "NotNull" },
        };


        private static readonly Dictionary<string, Expected> IsValidByFunction = new () {
            
            //                                                               <******** A Null *********>      <******** A Empty ********>      <********** A ***********>       <********* No A **********> 
            //                                                               ANull  AnBn   AnBe   ANullB      AEmpty AeBn   AeBe   AeB         A      ABNull ABe    AB          None   BNull  BEmpty B
            { nameof(RequirementValidator.Changed),             new Expected(false, true,  true,  true,  /**/ false, true,  true,  true,  /**/ false, true,  true,  true,  /**/ false, false, false, false) },
            { nameof(RequirementValidator.ChangedAny),          new Expected(true,  true,  true,  true,  /**/ true,  true,  true,  true,  /**/ true,  true,  true,  true,  /**/ false, true,  true,  true ) },
            { nameof(RequirementValidator.Cleared),             new Expected(false, true,  true,  false, /**/ false, true,  true,  false, /**/ false, false, false, false, /**/ false, false, false, false) },
            { nameof(RequirementValidator.ClearedAny),          new Expected(true,  true,  true,  true,  /**/ true,  true,  true,  true,  /**/ false, true,  true,  false, /**/ false, true,  true,  false) },
            { nameof(RequirementValidator.Contains),            new Expected(false, false, false, false, /**/ false, false, false, false, /**/ false, false, false, true,  /**/ false, false, false, false) },
            { nameof(RequirementValidator.ContainsAny),         new Expected(false, false, false, true,  /**/ false, false, false, true,  /**/ true,  true,  true,  true,  /**/ false, false, false, true ) },
            { nameof(RequirementValidator.ContainsAnyNull),     new Expected(true,  true,  true,  true,  /**/ true,  true,  true,  true,  /**/ false, true,  true,  false, /**/ false, true,  true,  false) },
            { nameof(RequirementValidator.ContainsAnyNullable), new Expected(true,  true,  true,  true,  /**/ true,  true,  true,  true,  /**/ true,  true,  true,  true,  /**/ false, true,  true,  true ) },
            { nameof(RequirementValidator.ContainsNull),        new Expected(false, true,  true,  false, /**/ false, true,  true,  false, /**/ false, false, false, false, /**/ false, false, false, false) },
            { nameof(RequirementValidator.ContainsNullable),    new Expected(false, true,  true,  true,  /**/ false, true,  true,  true,  /**/ false, true,  true,  true,  /**/ false, false, false, false) },
            { nameof(RequirementValidator.Missing),             new Expected(false, false, false, false, /**/ false, false, false, false, /**/ false, false, false, false, /**/ true,  false, false, false) },
            { nameof(RequirementValidator.MissingAny),          new Expected(true,  false, false, false, /**/ true,  false, false, false, /**/ true,  false, false, false, /**/ true,  true,  true,  true ) },
            { nameof(RequirementValidator.MissingOrNull),       new Expected(true,  true,  true,  false, /**/ true,  true,  true,  false, /**/ false, false, false, false, /**/ true,  true,  true,  false) },
            { nameof(RequirementValidator.MissingOrNullAny),    new Expected(true,  true,  true,  true,  /**/ true,  true,  true,  true,  /**/ true,  true,  true,  false, /**/ true,  true,  true,  true ) },
            { nameof(RequirementValidator.Not),                 new Expected(true,  true,  true,  true,  /**/ true,  true,  true,  true,  /**/ true,  true,  true,  false, /**/ true,  true,  true,  true ) },
            { nameof(RequirementValidator.NotAny),              new Expected(true,  true,  true,  false, /**/ true,  true,  true,  false, /**/ false, false, false, false, /**/ true,  true,  true,  false) },
            { nameof(RequirementValidator.Updated),             new Expected(false, false, false, false, /**/ false, false, false, false, /**/ false, false, false, true,  /**/ false, false, false, false) },
            { nameof(RequirementValidator.UpdatedAny),          new Expected(false, false, false, true,  /**/ false, false, false, true,  /**/ true,  true,  true,  true,  /**/ false, false, false, true ) },
        };

        private static readonly string[] Columns = { Contact.Fields.AssistantName, Contact.Fields.NickName };
        private static readonly HashSet<string> FunctionsWithPreImage = new HashSet<string>
        {
            nameof(RequirementValidator.Changed),
            nameof(RequirementValidator.ChangedAny),
            nameof(RequirementValidator.Cleared),
            nameof(RequirementValidator.ClearedAny),
            nameof(RequirementValidator.Updated),
            nameof(RequirementValidator.UpdatedAny),
        };

        [TestMethod]
        public void Changed_Test()
        {
            var sut = new RequirementValidator();
            sut.Changed(Columns);
            TestEachPossibleValue(sut, nameof(RequirementValidator.Changed));
        }

        [TestMethod]
        public void ChangedAny_Test()
        {
            var sut = new RequirementValidator();
            sut.ChangedAny(Columns);
            TestEachPossibleValue(sut, nameof(RequirementValidator.ChangedAny));
        }

        [TestMethod]
        public void Cleared_Test()
        {
            var sut = new RequirementValidator();
            sut.Cleared (Columns);
            TestEachPossibleValue(sut, nameof(RequirementValidator.Cleared));
        }

        [TestMethod]
        public void ClearedAny_Test()
        {
            var sut = new RequirementValidator();
            sut.ClearedAny(Columns);
            TestEachPossibleValue(sut, nameof(RequirementValidator.ClearedAny));
        }

        [TestMethod]
        public void Contains_Test()
        {
            var sut = new RequirementValidator();
            sut.Contains(ContextEntity.Target, Columns);
            TestEachPossibleValue(sut, nameof(RequirementValidator.Contains));
        }

        [TestMethod]
        public void ContainsAny_Test()
        {
            var sut = new RequirementValidator();
            sut.ContainsAny(ContextEntity.Target, Columns);
            TestEachPossibleValue(sut, nameof(RequirementValidator.ContainsAny));
        }

        [TestMethod]
        public void ContainsAnyNull_Test()
        {
            var sut = new RequirementValidator();
            sut.ContainsAnyNull(ContextEntity.Target, Columns);
            TestEachPossibleValue(sut, nameof(RequirementValidator.ContainsAnyNull));
        }

        [TestMethod]
        public void ContainsAnyNullable_Test()
        {
            var sut = new RequirementValidator();
            sut.ContainsAnyNullable(ContextEntity.Target, Columns);
            TestEachPossibleValue(sut, nameof(RequirementValidator.ContainsAnyNullable));
        }

        [TestMethod]
        public void ContainsNull_Test()
        {
            var sut = new RequirementValidator();
            sut.ContainsAnyNullable(ContextEntity.Target, Columns);
            TestEachPossibleValue(sut, nameof(RequirementValidator.ContainsAnyNullable));
        }

        [TestMethod]
        public void ContainsNullable_Test()
        {
            var sut = new RequirementValidator();
            sut.ContainsNullable(ContextEntity.Target, Columns);
            TestEachPossibleValue(sut, nameof(RequirementValidator.ContainsNullable));
        }

        [TestMethod]
        public void Missing_Test()
        {
            var sut = new RequirementValidator();
            sut.Missing(ContextEntity.Target, Columns);
            TestEachPossibleValue(sut, nameof(RequirementValidator.Missing));
        }

        [TestMethod]
        public void MissingAny_Test()
        {
            var sut = new RequirementValidator();
            sut.MissingAny(ContextEntity.Target, Columns);
            TestEachPossibleValue(sut, nameof(RequirementValidator.MissingAny));
        }

        [TestMethod]
        public void MissingOrNull_Test()
        {
            var sut = new RequirementValidator();
            sut.MissingOrNull(ContextEntity.Target, Columns);
            TestEachPossibleValue(sut, nameof(RequirementValidator.MissingOrNull));
        }

        [TestMethod]
        public void MissingOrNullAny_Test()
        {
            var sut = new RequirementValidator();
            sut.MissingOrNullAny(ContextEntity.Target, Columns);
            TestEachPossibleValue(sut, nameof(RequirementValidator.MissingOrNullAny));
        }

        [TestMethod]
        public void Not_Test()
        {
            var sut = new RequirementValidator();
            sut.Not(ContextEntity.Target, new Contact { AssistantName = "NotNull", NickName = "NotNull" });
            TestEachPossibleValue(sut, nameof(RequirementValidator.Not));
        }


        [TestMethod]
        public void NotAny_Test()
        {
            var sut = new RequirementValidator();
            sut.NotAny(ContextEntity.Target, new Contact { AssistantName = "NotNull", NickName = "NotNull" });
            TestEachPossibleValue(sut, nameof(RequirementValidator.NotAny));
        }

        [TestMethod]
        public void Updated_Test()
        {
            var sut = new RequirementValidator();
            sut.Updated(Columns);
            TestEachPossibleValue(sut, nameof(RequirementValidator.Updated));
        }

        [TestMethod]
        public void UpdatedAny_Test()
        {
            var sut = new RequirementValidator();
            sut.UpdatedAny(Columns);
            TestEachPossibleValue(sut, nameof(RequirementValidator.UpdatedAny));
        }

        private static void TestEachPossibleValue(RequirementValidator sut, string functionName)
        {
            var expected = IsValidByFunction[functionName];
            var preImage = FunctionsWithPreImage.Contains(functionName)
                ? new Contact { AssistantName = "A", NickName = "B" }
                : null;
            for (var i = 0; i < PossibleValues.Count; i++)
            {
                var value = PossibleValues[i];
                var isValid = !sut.SkipExecution(GetContext(value, preImage));
                var expectedResult = expected.Get(i);
                Assert.AreEqual(expectedResult, isValid, $"Expected {expectedResult} for {GetDisplay(value)}");
            }
        }

        private static string GetDisplay(Contact contact)
        {
            var a = contact.AssistantName ?? "Null";
            var b = contact.NickName ?? "Null";
            a = string.IsNullOrEmpty(a) ? "Empty" : a;
            b = string.IsNullOrEmpty(b) ? "Empty" : b;

            return $"{a} and {b}";
        }

        private static FakeContext GetContext(Contact root, Contact? preImage)
        {
            var context = new FakeContext
            {
                MessageName = MessageType.Update,
                Target = root
            };

            if (preImage != null)
            {
                context.PreImage = preImage;
            }

            return context;
        }

        private class Expected
        {
            private bool ANull { get; }
            private bool ANullBNull { get; }
            private bool ANullBEmpty { get; }
            private bool ANullB { get; }
            private bool AEmpty { get; }
            private bool AEmptyBNull { get; }
            private bool AEmptyBEmpty { get; }
            private bool AEmptyB { get; }
            private bool A { get; }
            private bool ABNull { get; }
            private bool ABEmpty { get; }
            private bool AB { get; }
            private bool None { get; }
            private bool BNull { get; }
            private bool BEmpty { get; }
            private bool B { get; }

            public Expected(bool aNull, bool aNullBNull, bool aNullBEmpty, bool aNullB, bool aEmpty, bool aEmptyBNull, bool aEmptyBEmpty, bool aEmptyB, bool a, bool abNull, bool abEmpty, bool ab, bool none, bool bNull, bool bEmpty, bool b)
            {
                ANull = aNull;
                ANullBNull = aNullBNull;
                ANullBEmpty = aNullBEmpty;
                ANullB = aNullB;
                AEmpty = aEmpty;
                AEmptyBNull = aEmptyBNull;
                AEmptyBEmpty = aEmptyBEmpty;
                AEmptyB = aEmptyB;
                A = a;
                ABNull = abNull;
                ABEmpty = abEmpty;
                AB = ab;
                None = none;
                BNull = bNull;
                BEmpty = bEmpty;
                B = b;
            }

            public bool Get(int i)
            {
                switch (i)
                {
                    case 0:
                        return ANull;
                    case 1:
                        return ANullBNull;
                    case 2:
                        return ANullBEmpty;
                    case 3:
                        return ANullB;
                    case 4:
                        return AEmpty;
                    case 5:
                        return AEmptyBNull;
                    case 6:
                        return AEmptyBEmpty;
                    case 7:
                        return AEmptyB;
                    case 8:
                        return A;
                    case 9:
                        return ABNull;
                    case 10:
                        return ABEmpty;
                    case 11:
                        return AB;
                    case 12:
                        return None;
                    case 13:
                        return BNull;
                    case 14:
                        return BEmpty;
                    case 15:
                        return B;
                    default:
                        throw new IndexOutOfRangeException();
                }
            }
        }
    }
}
