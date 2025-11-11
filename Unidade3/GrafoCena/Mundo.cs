namespace GrafoCena
{
  internal class Mundo
  {
    private readonly List<Objeto> objetosLista = [];
    private readonly char rotulo = '@';
    private readonly char rotuloAtual;

    public Mundo()
    {
      rotuloAtual = rotulo;

      Objeto objeto = new(ref rotuloAtual);
      objetosLista.Add(objeto);

      objeto = new Objeto(ref rotuloAtual);
      objetosLista.Add(objeto);
      objeto.ObjetoFilhoAdicionar(ref rotuloAtual);
      objeto.ObjetoFilhoAdicionar(ref rotuloAtual);

      objeto = new Objeto(ref rotuloAtual);
      objetosLista.Add(objeto);

      Console.WriteLine("------------------");
      Console.WriteLine("Mundo");
      foreach (var obj in objetosLista)
      {
        obj.GrafocenaImprimir("  ");
      }
      Console.WriteLine("------------------");

      foreach (var obj in objetosLista)
      {
        Console.WriteLine(" _Informe ID Grafo Cena: ");
        var objBuscar = obj.GrafocenaBusca('C');
        if (objBuscar != null)
        {
          objBuscar.ObjetoFilhoAdicionar(ref rotuloAtual);
          Console.WriteLine("Achou!!");
          break;
        }
      }
      Console.WriteLine("..");

      Console.WriteLine("------------------");
      Console.WriteLine("Mundo");
      foreach (var obj in objetosLista)
      {
        obj.GrafocenaImprimir("  ");
      }
      Console.WriteLine("------------------");

    }

  }

}