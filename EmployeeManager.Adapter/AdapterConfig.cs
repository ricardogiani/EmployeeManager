using Dapper.FluentMap;
using EmployeeManager.Adapter.ModelMaps;

public class AdapterConfig : IAdapterConfig
{

    public AdapterConfig()
    {
        
    }

    public void Config()
    {
        // Configuração do FluentMap
        FluentMapper.Initialize(config =>
        {
            // Adiciona o mapeamento específico para EmployeeModel
            config.AddMap(new EmployeeModelMap());
        });
    }
}

public interface IAdapterConfig
{
    void Config();
}