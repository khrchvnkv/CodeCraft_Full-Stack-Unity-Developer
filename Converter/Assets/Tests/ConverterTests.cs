using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Homework
{
    public sealed class ConverterTests
    {
        private const int DefaultConvertCapacity = 10;
        private const float DefaultConvertCycleTime = 5.0f;
        
        [TestCaseSource(nameof(CorrectConverterCases))]
        public void WhenInstantiateWithCorrectParamsThenNotNull(
            int loadingCapacity,
            int unloadingCapacity,
            int loadingZoneAmount,
            int unloadingZoneAmount,
            float conversionTime)
        {
            var converter = new Converter(loadingCapacity, unloadingCapacity, loadingZoneAmount,
                unloadingZoneAmount, conversionTime);
            
            Assert.IsNotNull(converter);
        }

        private static IEnumerable<TestCaseData> CorrectConverterCases()
        {
            yield return new TestCaseData(1, 1, 1, 1, 1.0f).SetName("Case 1");
            yield return new TestCaseData(1, 2, 3, 4, 5.0f).SetName("Case 2");
            yield return new TestCaseData(3, 5, 7, 9, 1.0f).SetName("Case 3");
            yield return new TestCaseData(2, 4, 6, 8, 1.0f).SetName("Case 4");
            yield return new TestCaseData(9, 8, 7, 6, 0.1f).SetName("Case 5");
        }

        [TestCaseSource(nameof(IncorrectConverterCases))]
        public void WhenInstantiateWithIncorrectParamsThenException(
            int loadingCapacity,
            int unloadingCapacity,
            int loadingZoneAmount,
            int unloadingZoneAmount,
            float conversionTime)
        {
            Assert.Catch<ArgumentOutOfRangeException>(() =>
            {
                var converter = new Converter(loadingCapacity, unloadingCapacity, loadingZoneAmount,
                    unloadingZoneAmount, conversionTime);
            });
        }

        private static IEnumerable<TestCaseData> IncorrectConverterCases()
        {
            yield return new TestCaseData(1, 0, 1, 1, 1.0f).SetName("Case 1");
            yield return new TestCaseData(1, 2, 3, -1, 5.0f).SetName("Case 2");
            yield return new TestCaseData(3, 5, 7, 9, 0.0f).SetName("Case 3");
            yield return new TestCaseData(-1, 4, 6, 8, -1.0f).SetName("Case 4");
            yield return new TestCaseData(9, 8, 7, 6, -0.1f).SetName("Case 5");
        }

        [Test]
        public void WhenAddExtraLoadingValueThenLimitItByCapacity()
        {
            var converter = CreateDefaultConverter();

            converter.AddLoadingValue(DefaultConvertCapacity * 1000);
            
            Assert.AreEqual(converter.LoadingValue, DefaultConvertCapacity);
        }
        
        [Test]
        public void WhenAddExtraUnloadingValueThenLimitItByCapacity()
        {
            var converter = CreateDefaultConverter();

            converter.AddUnloadingValue(DefaultConvertCapacity * 1000);
            
            Assert.AreEqual(converter.UnloadingValue, DefaultConvertCapacity);
        }
        
        [Test]
        public void WhenEnableConverterThenIsEnabledTrue()
        {
            var converter = CreateDefaultConverter();

            converter.Enable();
            
            Assert.IsTrue(converter.IsEnabled);
        }
        
        [Test]
        public void WhenDisableConverterThenIsEnabledFalse()
        {
            var converter = CreateDefaultConverter();

            converter.Disable();
            
            Assert.IsFalse(converter.IsEnabled);
        }

        [Test]
        public void WhenStartConvertCycleThenLoadingCountDecreasing()
        {
            var converter = CreateDefaultConverter();
            converter.AddLoadingValue(DefaultConvertCapacity);
            
            converter.Enable();

            Assert.AreEqual(converter.LoadingValue, DefaultConvertCapacity - 3);
        }
        
        [Test]
        public void WhenConvertCycleThenUnloadingCountIncreasing()
        {
            var converter = CreateDefaultConverter();
            converter.AddLoadingValue(DefaultConvertCapacity);
            
            converter.Enable();
            converter.Update(DefaultConvertCycleTime);

            Assert.AreEqual(converter.UnloadingValue, 2);
        }
        
        [Test]
        public void WhenBreakConvertCycleThenLoadingCountReset()
        {
            var converter = CreateDefaultConverter();
            converter.AddLoadingValue(DefaultConvertCapacity);
            
            converter.Enable();
            converter.Update(DefaultConvertCycleTime / 2);
            converter.Disable();

            Assert.AreEqual(converter.LoadingValue, DefaultConvertCapacity);
            Assert.AreEqual(converter.UnloadingValue, 0);
            Assert.AreEqual(converter.Converting, false);
            Assert.AreEqual(converter.IsEnabled, false);
        }

        [Test]
        public void WhenAddNegativeLoadingValueThenException()
        {
            var converter = CreateDefaultConverter();

            Assert.Catch<ArgumentOutOfRangeException>(() =>
            {
                converter.AddLoadingValue(-1);
            });
        }
        
        [Test]
        public void WhenAddNegativeUnloadingValueThenException()
        {
            var converter = CreateDefaultConverter();

            Assert.Catch<ArgumentOutOfRangeException>(() =>
            {
                converter.AddUnloadingValue(-1);
            });
        }
        
        [Test]
        public void WhenRemoveNegativeUnloadingValueThenException()
        {
            var converter = CreateDefaultConverter();

            Assert.Catch<ArgumentOutOfRangeException>(() =>
            {
                converter.RemoveUnloadingValue(-1);
            });
        }
        
        [Test]
        public void WhenNoLoadingSpaceThenNotConverting()
        {
            var converter = CreateDefaultConverter();

            converter.Enable();
            
            Assert.IsFalse(converter.Converting);
            Assert.IsFalse(converter.CanStartConverter());
        }
        
        [Test]
        public void WhenFullUnloadingSpaceThenNotConverting()
        {
            var converter = CreateFullConverter();

            converter.Enable();
            
            Assert.IsFalse(converter.Converting);
            Assert.IsFalse(converter.CanStartConverter());
        }
        
        [Test]
        public void WhenConvertCycleThenAllSpacesChanging()
        {
            var converter = CreateDefaultConverter();
            converter.AddLoadingValue(DefaultConvertCapacity);
            int loadingValue = 0;
            int unloadingValue = 0;
            converter.OnConverted += () =>
            {
                loadingValue = converter.LoadingValue;
                unloadingValue = converter.UnloadingValue;
            };

            converter.Enable();
            converter.Update(DefaultConvertCycleTime);
            
            Assert.AreEqual(loadingValue, DefaultConvertCapacity - 3);
            Assert.AreEqual(unloadingValue, 2);
        }
        
        [Test]
        public void WhenConvertAllAvailableLoadingValueThenPauseConverting()
        {
            var converter = CreateDefaultConverter();
            converter.AddLoadingValue(DefaultConvertCapacity);
            int convertCount = 0;
            converter.OnConverted += () => convertCount++;

            converter.Enable();
            for (int i = 0; i < 5; i++)
            {
                converter.Update(DefaultConvertCycleTime);
            }
            
            Assert.AreEqual(convertCount, 3);
            Assert.AreEqual(converter.LoadingValue, 1);
            Assert.AreEqual(converter.UnloadingValue, 6);
            Assert.IsFalse(converter.Converting);
            Assert.IsTrue(converter.IsEnabled);
        }

        private Converter CreateDefaultConverter() => 
            new(DefaultConvertCapacity, 
                DefaultConvertCapacity, 
                3, 
                2, 
                DefaultConvertCycleTime);
        
        private Converter CreateFullConverter()
        {
            var converter = new Converter(DefaultConvertCapacity,
                DefaultConvertCapacity,
                3,
                2,
                DefaultConvertCycleTime);
            converter.AddLoadingValue(DefaultConvertCapacity);
            converter.AddUnloadingValue(DefaultConvertCapacity);
            return converter;
        }
    }
}