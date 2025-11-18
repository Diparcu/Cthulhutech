using System.Collections.Generic;

public class ResultadoTirada
{
    public bool Exito { get; set; }
    public string CombinacionNombre { get; set; }
    public int SumaCombinacion { get; set; }
    public List<int> DadosLanzados { get; set; }
    public List<int> DadosEnCombinacion { get; set; }

    public ResultadoTirada()
    {
        DadosLanzados = new List<int>();
        DadosEnCombinacion = new List<int>();
        CombinacionNombre = "Ninguna";
        SumaCombinacion = 0;
        Exito = false;
    }
}
