namespace MIMP.WIMP
{
    public interface IWIMPConfigurationService
    {

        public WIMPConfiguration Load();

        public void Save(WIMPConfiguration configuration);

    }
}
