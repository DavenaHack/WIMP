namespace MIMP.WIMP
{
    public class WIMPConfigurationServiceFactory
    {

        private IWIMPConfigurationService _service;


        public IWIMPConfigurationService Get()
        {
            if (_service == null)
                lock (this)
                    if (_service == null)
                        _service = new JSONWIMPConfigurationService(@"configuration.json");
            return _service;
        }

    }
}
