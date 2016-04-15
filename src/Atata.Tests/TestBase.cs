﻿using NUnit.Framework;
using OpenQA.Selenium.Firefox;

namespace Atata.Tests
{
    public abstract class TestBase : UITest
    {
        [SetUp]
        public void SetUp()
        {
            Driver = new FirefoxDriver();
            Log = new SimpleLogManager(
                message =>
                {
                    TestContext.WriteLine(message);
                },
                Driver);

            AtataContext.Init(Driver, Log);

            Log.Info("Start test");
            Driver.Manage().Window.Maximize();
            OnSetUp();
        }

        protected virtual void OnSetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
            Log.Info("Finish test");
            Driver.Quit();
        }

        protected void SetAndVerifyValues<T, TPage>(EditableField<T, TPage> control, params T[] values)
            where TPage : PageObject<TPage>
        {
            control.VerifyExists();

            for (int i = 0; i < values.Length; i++)
            {
                control.Set(values[i]);
                control.VerifyEquals(values[i]);
                Assert.That(control.Get(), Is.EqualTo(values[i]));

                if (i > 0 && !object.Equals(values[i], values[i - 1]))
                    control.VerifyDoesNotEqual(values[i - 1]);
            }
        }

        protected void SetAndVerifyValue<T, TPage>(EditableField<T, TPage> control, T value)
            where TPage : PageObject<TPage>
        {
            control.Set(value);
            VerifyEquals(control, value);
        }

        protected void VerifyEquals<T, TPage>(EditableField<T, TPage> control, T value)
            where TPage : PageObject<TPage>
        {
            control.VerifyEquals(value);
            Assert.That(control.Get(), Is.EqualTo(value));
        }

        protected void VerifyDoesNotEqual<T, TPage>(EditableField<T, TPage> control, T value)
            where TPage : PageObject<TPage>
        {
            control.VerifyDoesNotEqual(value);

            Assert.Throws<AssertionException>(() =>
                control.VerifyEquals(value));
        }
    }
}
