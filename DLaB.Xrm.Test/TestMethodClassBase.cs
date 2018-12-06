namespace DLaB.Xrm.Test
{
    public abstract class TestMethodClassBase : TestMethodClassBaseDLaB
    {

        protected override void LoadConfigurationSettings()
        {
            TestInitializer.InitializeTestSettings();
        }

        public void Test()
        {
            Test(new DebugLogger());
        }
    }
}
